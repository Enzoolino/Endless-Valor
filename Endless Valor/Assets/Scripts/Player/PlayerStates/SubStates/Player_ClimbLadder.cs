
using UnityEngine;

public class Player_ClimbLadder : Player_AbilityState
{
    
    
    public Player_ClimbLadder(Player player, PlayerStateMachine playerStateMachine, D_Player playerData, string animationBoolName) : base(player, playerStateMachine, playerData, animationBoolName)
    {
    }


    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Entered Ladder State");
        
        player.Rb.gravityScale = 0f;
    }

    public override void ExitState()
    {
        base.ExitState();

        player.Rb.gravityScale = 1f;
        player.InputHandler.UseLadderClimbInput();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        
        player.SetVelocityY(playerData.climbLadderSpeed * 1); //Modifier - for now (1) is hardcoded value


        if (player.InputHandler.LadderClimbInputStop)
            isAbilityDone = true;

        if (!player.isNearLadder)
            isAbilityDone = true;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }
}
