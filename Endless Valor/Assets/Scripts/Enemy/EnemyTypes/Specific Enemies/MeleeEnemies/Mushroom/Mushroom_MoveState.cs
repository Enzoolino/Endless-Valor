using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom_MoveState : EnemyMoveState
{
    private Mushroom enemy;
    
    public Mushroom_MoveState(Enemy enemy, EnemyStateMachine enemyStateMachine, string animationBoolName, D_EnemyMoveState stateData, Mushroom enemySpecific) : base(enemy, enemyStateMachine, animationBoolName, stateData)
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

        if (isDetectingWall || isDetectingLedge)
        {
            enemy.IdleState.SetFlipAfterIdle(true);
            enemyStateMachine.ChangeState(enemy.IdleState);
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
