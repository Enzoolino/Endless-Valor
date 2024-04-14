using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEye_HurtState : Enemy_HurtState
{
    private FlyingEye enemy;
    
    public FlyingEye_HurtState(Enemy enemy, EnemyStateMachine enemyStateMachine, string animationBoolName, D_Enemy_HurtState stateData, FlyingEye enemySpecific) : base(enemy, enemyStateMachine, animationBoolName, stateData)
    {
        this.enemy = enemySpecific;
    }

    public override void EnterState()
    {
        base.EnterState();
        enemy.enemyAudio.clip = enemy.flyingEyeHurtSound;
        enemy.enemyAudio.Play();
        
        enemy.ResetBasicAttackCooldowns(stateData.resetCooldownValue);
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
            enemyStateMachine.ChangeState(enemy.PlayerDetectedState);
        }
        else if (isPlayerInCloseAggroRange && !performCloseRangeAction)
        {
            enemyStateMachine.ChangeState(enemy.ChargeState);
        }
        else
        {
            enemyStateMachine.ChangeState(enemy.LookForPlayerState);
        }
    }
}
