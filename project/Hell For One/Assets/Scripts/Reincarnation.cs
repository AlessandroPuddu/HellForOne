﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reincarnation : MonoBehaviour
{
    private Stats playerStats;
    private GameObject player;

    public void Reincarnate()
    {   
        // TODO - do we really need this? we are destroying this GamaObject
        player.GetComponent<Controller>().enabled = false;
        player.GetComponent<TacticsManager>().enabled = false;
        player.GetComponent<Dash>().enabled = false;
        player.GetComponent<Combat>().enabled = false;
        player.tag = "DeadPlayer";
        CameraManager cameraManager = Camera.main.GetComponent<CameraManager>();
        cameraManager.player = null;
        
        player = GameObject.FindGameObjectWithTag( "Demon" );

        // If the player is null then the last ally demon is dying
        if(player != null) {
            player.GetComponent<Reincarnation>().enabled = true;

            player.tag = "Player";
            player.GetComponent<DemonBehaviour>().enabled = false;
            player.GetComponent<DemonMovement>().enabled = false;
            player.GetComponent<ObjectsPooler>().enabled = false;
            player.GetComponent<Lancer>().enabled = false;
            player.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
            player.GetComponent<Stats>().type = Stats.Type.Player;
            player.GetComponent<Controller>().enabled = true;
            player.GetComponent<TacticsManager>().enabled = true;
            player.GetComponent<Dash>().enabled = true;

            
            // Reset Combat
            Combat playerCombat =  player.GetComponent<Combat>();
            if (!player.GetComponent<Stats>().IsIdle) { 
                playerCombat.StopAll();    
            }
            
            // Removing the new player from the group belonging to
            int playerIndex = System.Array.IndexOf(player.GetComponent<DemonBehaviour>().groupBelongingTo.GetComponent<GroupBehaviour>().demons, player);
            GroupBehaviour gb = player.GetComponent<DemonBehaviour>().groupBelongingTo.GetComponent<GroupBehaviour>();
            gb.demons[playerIndex] = null;
            gb.SetDemonsNumber(gb.GetDemonsNumber() - 1);
        }
    }

    void Start()
    {
        player = gameObject;
        playerStats = gameObject.GetComponent<Stats>();
    }
}
