using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEye_FallState : Enemy_FallState
{
    private FlyingEye specificEnemy;
    
    public FlyingEye_FallState(Enemy enemy, EnemyStateMachine enemyStateMachine, string animationBoolName, D_Enemy_FallState stateData, FlyingEye specificEnemy) : base(enemy, enemyStateMachine, animationBoolName, stateData)
    {
        this.specificEnemy = specificEnemy;
    }


    public override void EnterState()
    {
        base.EnterState();
        
        enemy.boxCollider2D.size = new Vector2((float)1.751972, (float)0.4453514);
        enemy.boxCollider2D.offset = new Vector2((float)0.08396828, (float)-0.5935351);
        enemy.gameObject.layer = 13;
    }

    public override void ExitState()
    {
        base.ExitState();

        
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (specificEnemy.Rb.velocity.y == 0 && isGrounded)
        {
            specificEnemy.isFalling = false; //TODO: Make it universal
            enemyStateMachine.ChangeState(specificEnemy.DeadState);
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

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }
}
