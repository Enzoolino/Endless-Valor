using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom_HurtState : Enemy_HurtState
{
    private Mushroom enemy;
    
    public Mushroom_HurtState(Enemy enemy, EnemyStateMachine enemyStateMachine, string animationBoolName, D_Enemy_HurtState stateData, Mushroom enemySpecific) : base(enemy, enemyStateMachine, animationBoolName, stateData)
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
        
        if (performCloseRangeAction)
        {
            enemyStateMachine.ChangeState(enemy.MeleeAttackState);
        }
        else if (isPlayerInCloseAggroRange)
        {
            enemyStateMachine.ChangeState(enemy.ChargeState);
        }
        else
        {
            enemyStateMachine.ChangeState(enemy.LookForPlayerState);
        }
    }
}
