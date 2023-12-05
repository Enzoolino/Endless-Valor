using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom_IdleState : Enemy_IdleState
{
    private Mushroom enemy;
    
    public Mushroom_IdleState(Enemy enemy, EnemyStateMachine enemyStateMachine, string animationBoolName, D_Enemy_IdleState stateData, Mushroom enemySpecific) : base(enemy, enemyStateMachine, animationBoolName, stateData)
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

        if (isPlayerInCloseAggroRange)
        {
            enemyStateMachine.ChangeState(enemy.PlayerDetectedState);
        }
        else if (isIdleTimeOver)
        {
            enemyStateMachine.ChangeState(enemy.MoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
