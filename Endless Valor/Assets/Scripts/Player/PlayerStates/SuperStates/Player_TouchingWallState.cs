using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_TouchingWallState : PlayerState
{
    private bool isGrounded;
    private bool isTouchingOldWall;
    private bool isTouchingWall;
    
    private int xInput;
    private bool jumpInput;
    
    public Player_TouchingWallState(Player player, PlayerStateMachine playerStateMachine, D_Player playerData, string animationBoolName) : base(player, playerStateMachine, playerData, animationBoolName)
    {
    }
    
    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        xInput = player.InputHandler.NormalizedInputX;
        jumpInput = player.InputHandler.JumpInput;

        if (isGrounded)
        {
            playerStateMachine.ChangeState(player.IdleState);
        }
        else if (jumpInput && isTouchingWall)
        {
            playerStateMachine.ChangeState(player.WallJumpState);
        }
        else if (!isTouchingWall || xInput != player.FacingDirection)
        {
            playerStateMachine.ChangeState(player.InAirState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();
        
        isGrounded = player.CheckIfGrounded();
        isTouchingWall = player.CheckIfTouchingWall();
    }
    
    //TODO: Add Coyote time for wall jumping
    
    /*private void CheckWallJumpCoyoteTime()
    {
        if (wallJumpCoyoteTime && Time.time > startWallJumpCoyoteTime + playerData.coyoteTime)
        {
            wallJumpCoyoteTime = false;
        }
    }

    public void StartWallJumpCoyoteTime()
    {
        wallJumpCoyoteTime = true;
        startWallJumpCoyoteTime = Time.time;
    }
    public void StopWallJumpCoyoteTime() => wallJumpCoyoteTime = false;*/
    
}
