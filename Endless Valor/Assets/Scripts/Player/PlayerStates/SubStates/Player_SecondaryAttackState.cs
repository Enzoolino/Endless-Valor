using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_SecondaryAttackState : Player_AbilityState
{
    protected AttackDetails attackDetails;
    
    public Player_SecondaryAttackState(Player player, PlayerStateMachine playerStateMachine, D_Player playerData, string animationBoolName) : base(player, playerStateMachine, playerData, animationBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        //TODO: Change to secondary
        attackDetails.damageAmount = playerData.secondaryAttackDamage;
        attackDetails.stunDamageAmount = playerData.secondaryAttackStunDamage;
        attackDetails.position = player.secondaryAttackArea.position;
        attackDetails.scale = player.secondaryAttackArea.localScale;
        
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
        player.SetComboTimer(playerData.comboTimer);
        player.SetComboState(3);
        isAbilityDone = true;
    }
}
