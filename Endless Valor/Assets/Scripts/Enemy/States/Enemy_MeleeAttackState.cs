using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_MeleeAttackState : Enemy_AttackState
{
    protected D_Enemy_MeleeAttackState stateData;
    protected AttackDetails attackDetails;

    public Enemy_MeleeAttackState(Enemy enemy, EnemyStateMachine enemyStateMachine, string animationBoolName, Transform attackPosition, D_Enemy_MeleeAttackState stateData) : base(enemy, enemyStateMachine, animationBoolName, attackPosition)
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

        //attackCooldownTimer -= Time.deltaTime; //TODO: To chyba nic tutaj nie robi ?
        /*if (attackCooldownTimer <= 0)
            isAttackOnCooldown = false;*/
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

        enemy.didAttackHit = detectedObjects.Length != 0;
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
        enemy.attackCooldownTimer = stateData.attackCooldown;
    }
}
