using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_DeadState : EnemyState
{
    private D_Enemy_DeadState stateData;
    
    public Enemy_DeadState(Enemy enemy, EnemyStateMachine enemyStateMachine, string animationBoolName, D_Enemy_DeadState stateData) : base(enemy, enemyStateMachine, animationBoolName)
    {
        this.stateData = stateData;
    }

    public override void EnterState()
    {
        base.EnterState();
        enemy.rb.bodyType = RigidbodyType2D.Static;
        enemy.boxCollider2D.enabled = false;
        enemy.DestroyEnemyObject(stateData.deathDelay);
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
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }
}
