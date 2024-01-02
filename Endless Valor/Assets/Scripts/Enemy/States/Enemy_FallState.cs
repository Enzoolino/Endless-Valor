using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_FallState : EnemyState
{
    private D_Enemy_FallState stateData;
    protected bool isGrounded;
    
    public Enemy_FallState(Enemy enemy, EnemyStateMachine enemyStateMachine, string animationBoolName, D_Enemy_FallState stateData) : base(enemy, enemyStateMachine, animationBoolName)
    {
        this.stateData = stateData;
    }


    public override void EnterState()
    {
        base.EnterState();
        
        enemy.Rb.gravityScale = 1;
        enemy.Rb.mass = 0.1f;
    }

    public override void ExitState()
    {
        base.ExitState();

        
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isGrounded)
        {
            enemy.Rb.bodyType = RigidbodyType2D.Static;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = enemy.CheckGround();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }
}
