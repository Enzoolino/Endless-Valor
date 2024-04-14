using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_FinishingAttackState : Player_AbilityState
{
    protected AttackDetails attackDetails;
    
    public Player_FinishingAttackState(Player player, PlayerStateMachine playerStateMachine, D_Player playerData, string animationBoolName) : base(player, playerStateMachine, playerData, animationBoolName)
    {
    }


    public override void EnterState()
    {
        base.EnterState();
        
        attackDetails.damageAmount = playerData.finishingAttackDamage;
        attackDetails.stunDamageAmount = playerData.finishingAttackStunDamage;
        attackDetails.position = player.finishingAttackArea.position;
        attackDetails.scale = player.finishingAttackArea.localScale;

        player.PlayerAudio.clip = player.attackClip;
        player.PlayerAudio.Play();
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
        player.SetVelocityZero();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        
        base.AnimationTrigger();

        int count = 0;

        Collider2D[] detectedEnemies = Physics2D.OverlapBoxAll(attackDetails.position,
            attackDetails.scale, 0f, playerData.enemyLayerMask);
            
        foreach (Collider2D collider in detectedEnemies)
        {
            count++;
            collider.transform.SendMessage("TakeDamage", attackDetails);
        }

        if (count > 0)
        {
            player.PlayerAudio.clip = player.attackHitClip;
            player.PlayerAudio.Play();
        }
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        player.InputHandler.UsePrimaryAttackInput();
        player.SetComboTimer(0);
        player.SetComboState(2);
        isAbilityDone = true;
    }
}
