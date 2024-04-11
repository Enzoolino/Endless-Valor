using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_ChargeAttackState : Enemy_AttackState//It's deriving from EnemyState because the attack is triggered when the charge hits and not by animation events.
{
    protected D_Enemy_ChargeAttackState stateData;
    protected AttackDetails attackDetails;
    
    
    public Enemy_ChargeAttackState(Enemy enemy, EnemyStateMachine enemyStateMachine, string animationBoolName, Transform attackPosition, D_Enemy_ChargeAttackState stateData) : base(enemy, enemyStateMachine, animationBoolName, attackPosition)
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
