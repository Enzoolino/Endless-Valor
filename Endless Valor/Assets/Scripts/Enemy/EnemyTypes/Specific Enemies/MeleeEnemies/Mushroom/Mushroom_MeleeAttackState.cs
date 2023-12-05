using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom_MeleeAttackState : Enemy_MeleeAttackState
{
    protected Mushroom enemy;
    
    public Mushroom_MeleeAttackState(Enemy enemy, EnemyStateMachine enemyStateMachine, string animationBoolName, Transform attackPosition, D_Enemy_MeleeAttackState stateData, Mushroom enemySpecific) : base(enemy, enemyStateMachine, animationBoolName, attackPosition, stateData)
    {
        this.enemy = enemySpecific;
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Entering Attack State");
    }

    public override void ExitState()
    {
        base.ExitState();
        Debug.Log("Exiting Attack State");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            Debug.Log("Attack Anim Finished");
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
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }
}
