using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AttackState : EnemyState
{
    protected Transform attackPosition;
    protected bool isAnimationFinished;
    protected bool isPlayerInCloseAggroRange;
    [HideInInspector] public bool isAttackOnCooldown;
    
    
    public Enemy_AttackState(Enemy enemy, EnemyStateMachine enemyStateMachine, string animationBoolName, Transform attackPosition) : base(enemy, enemyStateMachine, animationBoolName)
    {
        this.attackPosition = attackPosition;
    }

    public override void EnterState()
    {
        base.EnterState();

        enemy.eatsm.attackState = this;
        isAnimationFinished = false;
        enemy.SetVelocity(0f);
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

        isPlayerInCloseAggroRange = enemy.CheckPlayerInCloseAggroRange();
    }

    public virtual void TriggerAttack()
    {
        
    }

    public virtual void FinishAttack()
    {
        isAnimationFinished = true;
        isAttackOnCooldown = true;
    }
}
