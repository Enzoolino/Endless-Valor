using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom_ChargeState : EnemyChargeState
{
    private Mushroom enemy;
    
    public Mushroom_ChargeState(Enemy enemy, EnemyStateMachine enemyStateMachine, string animationBoolName, D_EnemyChargeState stateData, Mushroom enemySpecific) : base(enemy, enemyStateMachine, animationBoolName, stateData)
    {
        this.enemy = enemySpecific;
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (performCloseRangeAction)
        {
            enemyStateMachine.ChangeState(enemy.MeleeAttackState);
        }
        else if (isDetectingLedge || isDetectingWall)
        {
            enemyStateMachine.ChangeState(enemy.LookForPlayerState);
        }
        else if (isChargeTimeOver)
        {
            if (isPlayerInCloseAggroRange)
            {
                enemyStateMachine.ChangeState(enemy.PlayerDetectedState);
            }
            else
            {
                enemyStateMachine.ChangeState(enemy.LookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }
}
