using UnityEngine;

//For now state is configured for only one weapon : sword
public class Player_PrimaryAttackState : Player_AbilityState
{
    protected AttackDetails attackDetails;
    
    public Player_PrimaryAttackState(Player player, PlayerStateMachine playerStateMachine, D_Player playerData, string animationBoolName) : base(player, playerStateMachine, playerData, animationBoolName)
    {
    }
    
    public override void EnterState()
    {
        base.EnterState();
        
        attackDetails.damageAmount = playerData.primaryAttackDamage;
        attackDetails.stunDamageAmount = playerData.primaryAttackStunDamage;
        attackDetails.position = player.primaryAttackArea.position;
        attackDetails.scale = player.primaryAttackArea.localScale;
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

        Collider2D[] detectedEnemies = Physics2D.OverlapBoxAll(attackDetails.position,
            attackDetails.scale, 0f, playerData.enemyLayerMask);
            
        foreach (Collider2D collider in detectedEnemies)
        {
            collider.transform.SendMessage("TakeDamage", attackDetails);
        }
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        player.InputHandler.UsePrimaryAttackInput();
        isAbilityDone = true;
    }
    
}
