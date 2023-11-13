using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom_IdleState : EnemyIdleState
{
    private Mushroom enemy;
    
    public Mushroom_IdleState(Enemy enemy, EnemyStateMachine enemyStateMachine, string animationBoolName, D_EnemyIdleState stateData, Mushroom enemySpecific) : base(enemy, enemyStateMachine, animationBoolName, stateData)
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

        if (isIdleTimeOver)
        {
            enemyStateMachine.ChangeState(enemy.MoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
