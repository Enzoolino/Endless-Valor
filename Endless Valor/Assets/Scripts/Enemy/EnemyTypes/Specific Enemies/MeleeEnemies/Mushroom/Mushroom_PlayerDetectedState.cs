using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom_PlayerDetectedState : Enemy_PlayerDetectedState
{
    private Mushroom enemy;
    
    public Mushroom_PlayerDetectedState(Enemy enemy, EnemyStateMachine enemyStateMachine, string animationBoolName, D_Enemy_PlayerDetectedState stateData, Mushroom specificEnemy, EnemyEmotesHandler emotesHandler) : base(enemy, enemyStateMachine, animationBoolName, stateData, emotesHandler)
    {
        this.enemy = specificEnemy;
    }

    public override void EnterState()
    {
        base.EnterState();

        if (enemyStateMachine.PreviousState == enemy.LookForPlayerState)
            wasLookingForPlayer = true;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        // Cooldown is here because the enemy transfers to this state when on cooldown
        if (performCloseRangeAction && !enemy.MeleeAttackState.isAttackOnCooldown) 
        {
            enemyStateMachine.ChangeState(enemy.MeleeAttackState);
        }
        else if (performLongRangeAction && !isDetectingLedge)
        {
            enemyStateMachine.ChangeState(enemy.ChargeState);
        }
        else if (!isPlayerInFarAggroRange)
        {
            enemyStateMachine.ChangeState(enemy.LookForPlayerState);
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
