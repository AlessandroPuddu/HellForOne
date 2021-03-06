﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEventsHandler : MonoBehaviour
{
    private GameObject spawner;

    private static bool isInBossBattle = false;
    private static bool isInRegularBattle = false;

    private static BattleEventsHandler _instance;
    public static BattleEventsHandler Instance { get { return _instance; } }

    public static bool IsInBossBattle { get => isInBossBattle; private set => isInBossBattle = value; }
    // TODO - there are no differences anymore between battles
    public static bool IsInRegularBattle { get => isInRegularBattle; private set => isInRegularBattle = value; }

    private void Awake()
    {
        spawner = GameObject.FindGameObjectWithTag("Spawner");

        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        // This is here to start in an out of combat situation
        //BattleEventsManager.RaiseOnBattleExit();
        //BattleEventsManager.RaiseOnBossBattleExit();

        // TODO - Testing this to remove mob battle
        // find better solution
        BattleEventsManager.RaiseOnBattleExit();
    }

    private void OnEnable()
    {
        BattleEventsManager.onBattleEnter += EnterRegularBattle;
        BattleEventsManager.onBossBattleEnter += EnterBossBattle;

        BattleEventsManager.onBattleExit += ExitRegularBattle;
        BattleEventsManager.onBossBattleExit += ExitBossBattle;
    }

    private void OnDisable()
    {
        BattleEventsManager.onBattleEnter -= EnterRegularBattle;
        BattleEventsManager.onBossBattleEnter -= EnterBossBattle;

        BattleEventsManager.onBattleExit -= ExitRegularBattle;
        BattleEventsManager.onBossBattleExit -= ExitBossBattle;
    }

    private void Update()
    {
        if (IsInRegularBattle)
        {
            if (EnemiesManager.Instance.LittleEnemiesList.Count == 0)
            {
                BattleEventsManager.RaiseOnBattleExit();
            }
        }


        if (IsInBossBattle)
        {
            if (EnemiesManager.Instance.Boss == null)
            {
                BattleEventsManager.RaiseOnBossBattleExit();
            }
        }
    }

    private void EnterRegularBattle()
    {   
        if(!isInRegularBattle)
            isInRegularBattle = true;
        /*
        if (!IsInRegularBattle)
        {
            IsInRegularBattle = true;
            if (spawner != null)
            {
                spawner.GetComponent<AllyDemonSpawnerTest>().enabled = true;
            }
        }
        */
    }

    private void EnterBossBattle()
    {   
        if (!isInBossBattle)
        {
            IsInBossBattle = true;
            /*
            // TODO - Manage this in alliesManager
            if (spawner != null)
            {
                AllyDemonSpawnerTest adst = spawner.GetComponent<AllyDemonSpawnerTest>();

                if (adst != null)
                {
                    adst.enabled = true;
                }
            }
            /*
            GameObject boss = GameObject.FindGameObjectWithTag("Boss");

            if(boss != null) { 
                BossBehavior bossBehavior = boss.GetComponent<BossBehavior>();
                if(bossBehavior != null) { 
                    bossBehavior.enabled = true;    
                }
            }
            */
        }
    }

    private void ExitRegularBattle()
    {
        /*
        if (IsInRegularBattle)
        {
            IsInRegularBattle = false;
            if (spawner != null)
            {
                StopCoroutine(spawner.GetComponent<AllyDemonSpawnerTest>().SpawnAllyCR);
                spawner.GetComponent<AllyDemonSpawnerTest>().enabled = false;
            }
        }
        */
        if (isInRegularBattle) {
            isInRegularBattle = false;
        }
    }

    private void ExitBossBattle()
    {   /*
        if (isInBossBattle)
        {
            IsInBossBattle = false;
            if (spawner != null)
            {
                StopCoroutine(spawner.GetComponent<AllyDemonSpawnerTest>().SpawnAllyCR);
                spawner.GetComponent<AllyDemonSpawnerTest>().enabled = false;
            }
        }
        */
        if (isInBossBattle) {
            isInBossBattle = false;
        }
    }

}
