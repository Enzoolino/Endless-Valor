using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttackState : EnemyAttackState
{
    protected D_EnemyMeleeAttackState stateData;

    protected AttackDetails attackDetails;

    public EnemyMeleeAttackState(Enemy enemy, EnemyStateMachine enemyStateMachine, string animationBoolName, Transform attackPosition, D_EnemyMeleeAttackState stateData) : base(enemy, enemyStateMachine, animationBoolName, attackPosition)
    {
        this.stateData = stateData;
    }

    public override void EnterState()
    {
        base.EnterState();

        attackDetails.damageAmount = stateData.attackDamage;
        attackDetails.position = enemy.EnemyVisual.transform.position;
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

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        Collider2D[] detectedObjects =
            Physics2D.OverlapCircleAll(attackPosition.position, stateData.attackRadius, stateData.playerLayerMask);

        foreach (Collider2D collider in detectedObjects)
        {
            collider.transform.SendMessage("TakeDamage", attackDetails);
        }
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }
}
