using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEye_ChargeAttackState : Enemy_ChargeAttackState
{
    private FlyingEye enemy;
    
    public FlyingEye_ChargeAttackState(Enemy enemy, EnemyStateMachine enemyStateMachine, string animationBoolName, Transform attackPosition, D_Enemy_ChargeAttackState stateData, FlyingEye enemySpecific) : base(enemy, enemyStateMachine, animationBoolName, attackPosition, stateData)
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

        if (isAnimationFinished)
        {
            if (isPlayerInCloseAggroRange)
            {
                enemyStateMachine.ChangeState(enemy.PlayerDetectedState);
            }
            else
            {
                enemyStateMachine.ChangeState(enemy.LookForPlayerState);
            }
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

    public override void TriggerAttack()
    {
        base.TriggerAttack();
        
        if (enemy.didAttackHit && !enemy.wasAttackBlocked)
        {
            enemy.enemyAudio.clip = enemy.flyingEyeChargeAttackHitSound;
        }
        else if (!enemy.didAttackHit)
        {
            enemy.enemyAudio.clip = enemy.flyingEyeAttackMissSound;
        }
        
        enemy.enemyAudio.Play();
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
        
    }
}
