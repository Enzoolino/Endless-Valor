using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_HurtState : EnemyState
{
    protected D_Enemy_HurtState stateData;

    protected bool performCloseRangeAction;
    protected bool isPlayerInCloseAggroRange;
    
    public Enemy_HurtState(Enemy enemy, EnemyStateMachine enemyStateMachine, string animationBoolName, D_Enemy_HurtState stateData) : base(enemy, enemyStateMachine, animationBoolName)
    {
        this.stateData = stateData;
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
    }

    public override void DoChecks()
    {
        base.DoChecks();

        performCloseRangeAction = enemy.CheckPlayerInCloseRangeAction();
        isPlayerInCloseAggroRange = enemy.CheckPlayerInCloseAggroRange();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        enemy.isHurt = false;
    }
}
