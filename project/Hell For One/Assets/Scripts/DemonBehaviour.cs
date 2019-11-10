﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class DemonBehaviour : MonoBehaviour
{
    [SerializeField]
    public GameObject groupBelongingTo;
    public bool groupFound = false;

    private Stats stats;

    private void Start()
    {
        //FindGroup();
        stats = GetComponent<Stats>();
    }

    private void Update()
    {
        if ( !groupFound )
            FindGroup();
        /*
           Testing death managment in Stats
        if ( stats.health <= 0) 
            Destroy(gameObject);
        */    
    }

    // Balances group entering too
    public void FindGroup()
    {
        GameObject[] groups = GameObject.FindGameObjectsWithTag( "Group" );
        GameObject bestGroup = null;
        int maxFreeSlots = 0;

        foreach ( GameObject group in groups )
        {
            int freeSlots = 0;

            GameObject[] demonsArray = group.GetComponent<GroupBehaviour>().demons;
            GroupBehaviour groupBehaviour = group.GetComponent<GroupBehaviour>();
            //for ( int i = 0; i < demonsArray.Length; i++ )
            //{
            //    if ( demonsArray[ i ] == null )
            //    {
            //        freeSlots++;
            //    }
            //}
            freeSlots = groupBehaviour.maxNumDemons - groupBehaviour.demonsInGroup;

            if ( freeSlots > maxFreeSlots )
            {
                maxFreeSlots = freeSlots;
                bestGroup = group;
            }
        }

        if ( bestGroup != null )
        {
            GameObject[] demonsArray = bestGroup.GetComponent<GroupBehaviour>().demons;

            // Returns -1 if not present
            int firstEmpty = System.Array.IndexOf( demonsArray, null );

            if (firstEmpty >= 0)
            {
                demonsArray[ firstEmpty ] = gameObject;
                groupFound = true;
                groupBelongingTo = bestGroup;
                GroupBehaviour groupBehaviour = bestGroup.GetComponent<GroupBehaviour>();
                groupBehaviour.demonsInGroup++;
            }

            //for ( int i = 0; i < demonsArray.Length; i++ )
            //{
            //    if ( demonsArray[ i ] == null )
            //    {
            //        demonsArray[ i ] = gameObject;
            //        groupFound = true;
            //        groupBelongingTo = bestGroup;
            //        break;
            //    }
            //}
        }
    }
}
