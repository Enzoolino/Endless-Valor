using UnityEngine;

public class Player_WallJumpState : Player_AbilityState
{
    private int wallJumpDirection;
    
    public Player_WallJumpState(Player player, PlayerStateMachine playerStateMachine, D_Player playerData, string animationBoolName) : base(player, playerStateMachine, playerData, animationBoolName)
    {
    }


    public override void EnterState()
    {
        base.EnterState();
        
        // Debugging: Check the initial velocity
        Debug.Log("Initial Velocity: " + player.CurrentVelocity);
        
        player.InputHandler.UseJumpInput();
        player.JumpState.ResetAmountOfJumpsLeft();
        
        DetermineWallJumpDirection();
        
        player.SetVelocity(playerData.wallJumpVelocity, playerData.wallJumpAngle, wallJumpDirection);
        player.CheckIfShouldFlip(wallJumpDirection);
        player.JumpState.DecreaseAmountOfJumpsLeft();
        
        // Debugging: Check the velocity after setting it
        Debug.Log("Velocity after setting: " + player.CurrentVelocity);
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        player.Anim.SetFloat("yVelocity", player.CurrentVelocity.y);
        player.Anim.SetFloat("xVelocity", player.CurrentVelocity.x);

        if (Time.time >= startTime + playerData.wallJumpTime)
        {
            isAbilityDone = true;
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

    public void DetermineWallJumpDirection()
    {
        wallJumpDirection = -player.FacingDirection;
    }
}
