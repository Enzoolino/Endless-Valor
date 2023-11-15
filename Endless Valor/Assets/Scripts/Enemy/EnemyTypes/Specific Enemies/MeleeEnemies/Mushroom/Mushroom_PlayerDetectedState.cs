using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom_PlayerDetectedState : PlayerDetectedState
{
    private Mushroom enemy;
    
    public Mushroom_PlayerDetectedState(Enemy enemy, EnemyStateMachine enemyStateMachine, string animationBoolName, D_PlayerDetectedState stateData, Mushroom specificEnemy) : base(enemy, enemyStateMachine, animationBoolName, stateData)
    {
        this.enemy = specificEnemy;
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
        else if (performLongRangeAction)
        {
            enemyStateMachine.ChangeState(enemy.ChargeState);
        }
        else if (!isPlayerInFarAggroRange)
        {
            enemyStateMachine.ChangeState(enemy.LookForPlayerState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
