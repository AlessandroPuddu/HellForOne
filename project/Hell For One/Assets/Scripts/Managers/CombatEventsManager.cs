﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatEventsManager : MonoBehaviour
{
    #region CombatEvents fields

    public delegate void OnStartSingleAttack();
    public event OnStartSingleAttack onStartSingleAttack;

    public delegate void OnStopSingleAttack();
    public event OnStopSingleAttack onStopSingleAttack;

    public delegate void OnStartRangedAttack();
    public event OnStartRangedAttack onRangedAttack;

    public delegate void OnStopRangedAttack();
    public event OnStopRangedAttack onStopRangedAttack;

    public delegate void OnStartBlock();
    public event OnStartBlock onStartBlock;

    public delegate void OnStopBlock();
    public event OnStopBlock onStopBlock;

    public delegate void OnStartSupport();
    public event OnStartSupport onStartSupport;

    public delegate void OnStopSupport();
    public event OnStopSupport onStopSupport;

    public delegate void OnStartGroupAttack();
    public event OnStartGroupAttack onStartGroupAttack;

    public delegate void OnStartGlobalAttack();
    public event OnStartGlobalAttack onStartGlobalAttack;

    public delegate void OnSuccessfulHit();
    public event OnSuccessfulHit onSuccessfulHit;

    public delegate void OnBlockedHit();
    public event OnBlockedHit onBlockedHit;

    public delegate void OnDeath();
    public event OnDeath onDeath;

    public delegate void OnStartRunning();
    public event OnStartRunning onStartRunning;

    public delegate void OnStartIdle();
    public event OnStartIdle onStartIdle;

    public delegate void OnStopAnimation();
    public event OnStopAnimation onStopAnimation;

    #endregion

    #region CombatEventFields

    public void RaiseOnStartSingleAttack()
    {
        if (onStartSingleAttack != null)
        {
            onStartSingleAttack();
        }
    }

    public void RaiseOnStopSingleAttack()
    {
        if (onStopSingleAttack != null)
        {
            onStopSingleAttack();
        }
    }

    public void RaiseOnStartRangedAttack()
    {
        if (onRangedAttack != null)
        {
            onRangedAttack();
        }
    }

    public void RaiseOnStopRangedAttack()
    {
        if (onStopRangedAttack != null)
        {
            onStopRangedAttack();
        }
    }

    public void RaiseOnStartBlock()
    {
        if (onStartBlock != null)
        {
            onStartBlock();
        }
    }

    public void RaiseOnStopBlock()
    {
        if (onStopBlock != null)
        {
            onStopBlock();
        }
    }

    public void RaiseOnStartSupport()
    {
        if (onStartSupport != null)
        {
            onStartSupport();
        }
    }

    public void RaiseOnStopSupport()
    {
        if (onStopSupport != null)
        {
            onStopSupport();
        }
    }

    public void RaiseOnStartSweep()
    {
        if (onStartGroupAttack != null)
        {
            onStartGroupAttack();
        }
    }

    public void RaiseOnStartGlobalAttack()
    {
        if (onStartGlobalAttack != null)
        {
            onStartGlobalAttack();
        }
    }

    public void RaiseOnSuccessfulHit()
    {
        if (onSuccessfulHit != null)
        {
            onSuccessfulHit();
        }
    }

    public void RaiseOnBlockedHit()
    {
        if (onBlockedHit != null)
        {
            onBlockedHit();
        }
    }

    public void RaiseOnDeath()
    {
        if (onDeath != null)
        {
            onDeath();
        }
    }

    public void RaiseOnStartRunning() {
        if(onStartRunning != null) {
            onStartRunning();
        }
    }

    public void RaiseOnStartIdle() {
        if(onStartIdle != null) {
            onStartIdle();
        }
    }

    public void RaiseOnStopAnimation() {
        if(onStopAnimation != null) {
            onStopAnimation();
        }
    }

    #endregion
}
