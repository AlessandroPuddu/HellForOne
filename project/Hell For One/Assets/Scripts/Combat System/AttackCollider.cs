﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    private enum AttackColliderType{ 
        Ranged,
        Melee,
        None
    }

    [SerializeField]
    AttackColliderType type = AttackColliderType.None;

    public bool isSweeping = false;
    public bool isGlobalAttacking = false;

    private Stats stats;

    private Combat combat;

    private DemonBehaviour demonBehaviour;

    private void Start()
    {
        stats = this.transform.root.gameObject.GetComponent<Stats>();
        combat = this.transform.root.gameObject.GetComponent<Combat>();
    }
    
    private void OnEnable()
    {
        if(stats == null) { 
            stats = this.transform.root.gameObject.GetComponent<Stats>();    
        }
        if(combat == null) {
            combat = this.transform.root.gameObject.GetComponent<Combat>();
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        ManageCollisionUsingType(other);    
    }

    private void ManageCollisionUsingType(Collider other) {
        Stats targetRootStats = other.transform.root.gameObject.GetComponent<Stats>();
        if(targetRootStats != null) {
            if (other.tag == "IdleCollider")
            {
                switch (stats.type)
                {
                    case Stats.Type.Player:
                        if (targetRootStats.type == Stats.Type.Enemy || targetRootStats.type == Stats.Type.Boss)
                        {
                            ManagePlayerCollisions(targetRootStats, other);
                        }
                        break;
                    case Stats.Type.Ally:
                        if (targetRootStats.type == Stats.Type.Enemy || targetRootStats.type == Stats.Type.Boss)
                        {
                            ManageSimpleDemonCollisions(targetRootStats, other);
                        }
                        break;
                    case Stats.Type.Enemy:
                        if (targetRootStats.type == Stats.Type.Player || targetRootStats.type == Stats.Type.Ally)
                        {
                            ManageSimpleDemonCollisions(targetRootStats, other);
                        }
                        break;
                    case Stats.Type.Boss:
                        if (targetRootStats.type == Stats.Type.Player || targetRootStats.type == Stats.Type.Ally)
                        {
                            ManageBossCollisions(targetRootStats, other);
                        }
                        break;
                }
            }
        }
        else { 
            Debug.Log(this.transform.root.gameObject.name + " is trying to hit a target without stats. target is: " + other.transform.root.gameObject.name);    
        }
    }

    private void StopAttack() {
        switch (this.type)
        {
            case AttackColliderType.Melee:
                if(!isGlobalAttacking && !isSweeping)
                    combat.StopAttack();
                break;
            case AttackColliderType.Ranged:
                this.gameObject.SetActive(false);
                break;
            case AttackColliderType.None:
                Debug.Log(this.name + "AttackCollider.tyoe is set to None");
                break;
        }
    }

    private void ManageAggro() {
        if(this.type != AttackColliderType.None) { 
            
            int aggroModifier = 0;
            
            if(type == AttackColliderType.Melee) { 
                aggroModifier = stats.MeleeDamage;    
            }
            if(type == AttackColliderType.Ranged) { 
                aggroModifier = stats.RangedDamage;    
            }
            
            stats.RaiseAggro(aggroModifier);
            
            // We update Group aggro only for Ally Imps
            if(stats.type == Stats.Type.Ally) { 
                if(demonBehaviour == null ) { 
                    demonBehaviour = this.transform.root.gameObject.GetComponent<DemonBehaviour>();   
                }
                if(demonBehaviour != null) {
                    demonBehaviour.groupBelongingTo.GetComponent<GroupAggro>().RaiseGroupAggro(aggroModifier);
                }
            }      
        }
        else {
            Debug.Log(this.name + "AttackCollider.type is set to None");
        }
    }

    private bool CheckAngle(Transform other) { 
        return Vector3.Angle(this.transform.root.transform.forward, other.forward) < 91;
    }

    // TODO - Check if it is needed to call CheckAngle when a target is blocking
    private void ManageKnockBack(Stats targetRootStats) {
        // Calculate knockback chance
        if (Random.Range(1f, 101f) <= stats.KnockBackChance && !targetRootStats.IsBlocking)
        {
            targetRootStats.TakeKnockBack(stats.KnockBackUnits, this.transform.root, stats.KnockBackSpeed);
        }
        else { 
            Debug.Log("No KnockBack, probably the target is blocking");    
        }
    }

    private void ManagePlayerCollisions(Stats targetRootStats, Collider other) {
        if (targetRootStats.IsBlocking)
        {
            if (CheckAngle(other.gameObject.transform.root))
            {
                DealDamage(targetRootStats);
                ManageAggro();

                StopAttack();
            }
            else
            {
                ManageAggro();
                
                StopAttack();
            }
        }
        if (!targetRootStats.IsBlocking)
        {
            DealDamage(targetRootStats);

            ManageAggro();

            StopAttack();
        }
    }

    private void ManageSimpleDemonCollisions(Stats targetRootStats, Collider other) {
        if (targetRootStats.IsBlocking)
        {
            if (CheckAngle(other.gameObject.transform.root))
            {
                if (targetRootStats.CalculateBeenHitChance(false))
                {
                    DealDamage(targetRootStats);
                }
                ManageAggro();

                StopAttack();
            }
            else
            {
                if (targetRootStats.CalculateBeenHitChance(true))
                {
                    DealDamage(targetRootStats);
                }
                ManageAggro();

                StopAttack();
            }
        }
        if (!targetRootStats.IsBlocking)
        {
            if (targetRootStats.CalculateBeenHitChance(false))
            {
                DealDamage(targetRootStats);
            }
            ManageAggro();

            StopAttack();
        }
    }

    private void ManageBossCollisions(Stats targetRootStats, Collider other) {
        if (targetRootStats.IsBlocking)
        {
            // if target is blocking but is not looking towards the boss
            if (CheckAngle(other.gameObject.transform.root))
            {
                // calculate been hit chance without counting block bonus
                if (targetRootStats.CalculateBeenHitChance(false))
                {
                    DealDamage(targetRootStats);

                    ManageKnockBack(targetRootStats);
                }

                ManageAggro();

                // We stop the attack only if is a simple attack
                StopAttack();
            }
            // if target is blocking and is looking towards the boss
            else
            {
                // calculate been hit chance counting block bonus
                if (targetRootStats.CalculateBeenHitChance(true))
                {
                    DealDamage(targetRootStats);

                    ManageKnockBack(targetRootStats);
                }

                ManageAggro();

                StopAttack();
            }
        }
        if (!targetRootStats.IsBlocking)
        {   
            // Calculate been hit chance without counting block bonus
            if (targetRootStats.CalculateBeenHitChance(false))
            {
                DealDamage(targetRootStats);

                ManageKnockBack(targetRootStats);
            }

            ManageAggro();

            // We stop the attack only if is a simple attack
            StopAttack();
        }
    }

    private void DealDamage(Stats targetRootStats) { 
        if(type == AttackColliderType.Melee) { 
            targetRootStats.TakeHit(stats.MeleeDamage);
        }
        if(type == AttackColliderType.Ranged) { 
            targetRootStats.TakeHit(stats.RangedDamage);
        }
        if(type == AttackColliderType.None){
            Debug.Log(this.transform.root.gameObject.name + " attack collide type not set");
        }
    }
}
