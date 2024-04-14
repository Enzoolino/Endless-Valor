using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEye_PlayerDetectedState : Enemy_PlayerDetectedState
{
    private FlyingEye enemy;
    
    public FlyingEye_PlayerDetectedState(Enemy enemy, EnemyStateMachine enemyStateMachine, string animationBoolName, D_Enemy_PlayerDetectedState stateData, FlyingEye enemySpecific, EnemyEmotesHandler emotesHandler) : base(enemy, enemyStateMachine, animationBoolName, stateData, emotesHandler)
    {
        this.enemy = enemySpecific;
    }


    public override void EnterState()
    {
        base.EnterState();
        
        if (enemyStateMachine.PreviousState == enemy.LookForPlayerState)
            wasLookingForPlayer = true;
        else
        {
            while (enemy.enemyAudio.isPlaying)
            {
                return;
            }

            enemy.enemyAudio.clip = enemy.flyingEyeSurprisedSound;
            enemy.enemyAudio.Play();
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        enemy.attackCooldownTimer -= Time.deltaTime;  //TODO: Zmienić to na funkcje

        if (enemy.attackCooldownTimer <= 0)
        {
            enemy.MeleeAttackState.isAttackOnCooldown = false;
            enemy.ChargeAttackState.isAttackOnCooldown = false;
        }
        
        if (performCloseRangeAction && !enemy.MeleeAttackState.isAttackOnCooldown && !enemy.ChargeAttackState.isAttackOnCooldown)
        {
            enemyStateMachine.ChangeState(enemy.MeleeAttackState);
        }
        else if (performLongRangeAction && !performCloseRangeAction)
        {
            Debug.Log("Nietoper wchodzi w charge state");
            enemyStateMachine.ChangeState(enemy.ChargeState);
        }
        else if (!isPlayerInCloseAggroRange)
        {
            enemyStateMachine.ChangeState(enemy.LookForPlayerState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
