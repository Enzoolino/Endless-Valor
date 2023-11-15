using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom_LookForPlayerState : LookForPlayerState
{
    private Mushroom enemy;
    
    public Mushroom_LookForPlayerState(Enemy enemy, EnemyStateMachine enemyStateMachine, string animationBoolName, D_LookForPlayerState stateData, Mushroom specificEnemy) : base(enemy, enemyStateMachine, animationBoolName, stateData)
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (isPlayerInCloseAggroRange)
        {
            enemyStateMachine.ChangeState(enemy.PlayerDetectedState);
        }
        else if (isAllTurnsTimeDone)
        {
            enemyStateMachine.ChangeState(enemy.MoveState);
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }
}
