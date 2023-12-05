using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_StunState : EnemyState
{
    protected D_Enemy_StunState stateData;

    protected bool isStunTimeOver;
    protected bool isGrounded;
    protected bool isMovementStopped;
    protected bool performCloseRangeAction;
    protected bool isPlayerInCloseAggroRange;
    
    public Enemy_StunState(Enemy enemy, EnemyStateMachine enemyStateMachine, string animationBoolName, D_Enemy_StunState stateData) : base(enemy, enemyStateMachine, animationBoolName)
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
