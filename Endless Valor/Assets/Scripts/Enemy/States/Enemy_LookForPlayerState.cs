using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_LookForPlayerState : EnemyState
{
    protected D_Enemy_LookForPlayerState stateData;
    protected EnemyEmotesHandler emotesHandler;

    protected bool flipImmediately;
    protected bool isPlayerInCloseAggroRange;
    protected bool isAllTurnsDone;
    protected bool isAllTurnsTimeDone;

    protected float lastTurnTime;
    protected int amountOfTurnsDone;

    
    public Enemy_LookForPlayerState(Enemy enemy, EnemyStateMachine enemyStateMachine, string animationBoolName, D_Enemy_LookForPlayerState stateData, EnemyEmotesHandler emotesHandler) : base(enemy, enemyStateMachine, animationBoolName)
    {
        this.stateData = stateData;
        this.emotesHandler = emotesHandler;
    }


    public override void EnterState()
    {
        base.EnterState();
        
        emotesHandler.SetEmoteVisibility(true);
        emotesHandler.LookForPlayerEmoteHandler(); // Handle emote
        
        isAllTurnsDone = false;
        isAllTurnsTimeDone = false;
        lastTurnTime = startTime;
        amountOfTurnsDone = 0;
        
        enemy.SetVelocity(0f);
        
        Debug.Log("Entering LookForPlayer State");
    }

    public override void ExitState()
    {
        base.ExitState();
        emotesHandler.SetEmoteVisibility(false);
        emotesHandler.LookForPlayerEmoteHandler(); // Handle emote
        
        Debug.Log("Exiting LookForPlayer State");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (flipImmediately)
        {
            enemy.Flip();
            lastTurnTime = Time.time;
            amountOfTurnsDone++;
            flipImmediately = false;
        }
        else if (Time.time >= lastTurnTime + stateData.timeBetweenTurns && !isAllTurnsDone)
        {
            enemy.Flip();
            lastTurnTime = Time.time;
            amountOfTurnsDone++;
        }

        if (amountOfTurnsDone >= stateData.amountOfTurns)
        {
            isAllTurnsDone = true;
        }

        if (Time.time >= lastTurnTime + stateData.timeBetweenTurns && isAllTurnsDone)
        {
            isAllTurnsTimeDone = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInCloseAggroRange = enemy.CheckPlayerInCloseAggroRange();
    }

    public void SetFlipImmediately(bool flip)
    {
        flipImmediately = flip;
    }
    
    
}
