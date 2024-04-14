using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEye_MeleeAttackState : Enemy_MeleeAttackState
{
    private FlyingEye enemy;
    
    public FlyingEye_MeleeAttackState(Enemy enemy, EnemyStateMachine enemyStateMachine, string animationBoolName, Transform attackPosition, D_Enemy_MeleeAttackState stateData, FlyingEye enemySpecific) : base(enemy, enemyStateMachine, animationBoolName, attackPosition, stateData)
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

        enemy.enemyAudio.clip =
            enemy.didAttackHit ? enemy.flyingEyeMeleeAttackHitSound : enemy.flyingEyeAttackMissSound;
        enemy.enemyAudio.Play();
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }
}
