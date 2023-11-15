using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookForPlayerState : EnemyState
{
    protected D_LookForPlayerState stateData;

    protected bool flipImmediately;
    protected bool isPlayerInCloseAggroRange;
    protected bool isAllTurnsDone;
    protected bool isAllTurnsTimeDone;

    protected float lastTurnTime;
    protected int amountOfTurnsDone;
    
    public LookForPlayerState(Enemy enemy, EnemyStateMachine enemyStateMachine, string animationBoolName, D_LookForPlayerState stateData) : base(enemy, enemyStateMachine, animationBoolName)
    {
        this.stateData = stateData;
    }


    public override void EnterState()
    {
        base.EnterState();

        isAllTurnsDone = false;
        isAllTurnsTimeDone = false;
        lastTurnTime = startTime;
        amountOfTurnsDone = 0;
        
        enemy.SetVelocity(0f);
    }

    public override void ExitState()
    {
        base.ExitState();
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
