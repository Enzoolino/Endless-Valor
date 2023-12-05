using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom_ChargeState : Enemy_ChargeState
{
    private Mushroom enemy;
    
    public Mushroom_ChargeState(Enemy enemy, EnemyStateMachine enemyStateMachine, string animationBoolName, D_Enemy_ChargeState stateData, Mushroom enemySpecific) : base(enemy, enemyStateMachine, animationBoolName, stateData)
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
        
        Debug.Log("Exiting charge state");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (performCloseRangeAction)
        {
            Debug.Log("Charge time not over and entering attack state");
            enemyStateMachine.ChangeState(enemy.MeleeAttackState);
        }
        else if (isDetectingLedge || isDetectingWall)
        {
            Debug.Log("Charge time not over and entering look state");
            enemyStateMachine.ChangeState(enemy.LookForPlayerState);
        }
        else if (isChargeTimeOver)
        {
            if (isPlayerInCloseAggroRange)
            {
                Debug.Log("Charge time over and entering detected state");
                enemyStateMachine.ChangeState(enemy.PlayerDetectedState);
            }
            else
            {
                Debug.Log("Charge time over and entering look state");
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
