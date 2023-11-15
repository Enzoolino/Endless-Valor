using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStunState : EnemyState
{
    protected D_EnemyStunState stateData;

    protected bool isStunTimeOver;
    protected bool isGrounded;
    protected bool isMovementStopped;
    protected bool performCloseRangeAction;
    protected bool isPlayerInCloseAggroRange;
    
    public EnemyStunState(Enemy enemy, EnemyStateMachine enemyStateMachine, string animationBoolName, D_EnemyStunState stateData) : base(enemy, enemyStateMachine, animationBoolName)
    {
        this.stateData = stateData;
    }


    public override void EnterState()
    {
        base.EnterState();

        isStunTimeOver = false;
        isMovementStopped = false;
        enemy.SetVelocity(stateData.stunKnockbackSpeed, stateData.stunKnockbackAngle, enemy.LastDamageDirection);
    }

    public override void ExitState()
    {
        base.ExitState();
        
        enemy.ResetStunResistance();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + stateData.stunTime)
        {
            isStunTimeOver = true;
        }

        if (isGrounded && Time.time >= startTime + stateData.stunKnockbackTime && !isMovementStopped)
        {
            isMovementStopped = true;
            enemy.SetVelocity(0f);
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
        performCloseRangeAction = enemy.CheckPlayerInCloseRangeAction();
        isPlayerInCloseAggroRange = enemy.CheckPlayerInCloseAggroRange();
    }
}
