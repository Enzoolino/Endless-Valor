using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_InAirState : PlayerState
{
    private int xInput;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool jumpInput;
    private bool jumpInputStop;
    private bool isJumping;
    private bool isTouchingLedge;

    public float grabLedgeStopTime;
    public float disableMovingAfterLedgeJumpTime;

    
    
    public Player_InAirState(Player player, PlayerStateMachine playerStateMachine, D_Player playerData, string animationBoolName) : base(player, playerStateMachine, playerData, animationBoolName)
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
        jumpInputStop = player.InputHandler.JumpInputStop;
        
        CheckJumpMultiplier();
        
        if (isGrounded && player.CurrentVelocity.y < 0.01f)
        {
            playerStateMachine.ChangeState(player.LandState);
        }
        else if (isTouchingWall && !isTouchingLedge && Time.time >= grabLedgeStopTime + playerData.grabLedgeDelayTime)
        {
            playerStateMachine.ChangeState(player.LedgeClimbState);
        }
        else if (jumpInput && player.JumpState.CanJump())
        {
            playerStateMachine.ChangeState(player.JumpState);
        }
        else if (isTouchingWall && xInput == player.FacingDirection && Time.time >= disableMovingAfterLedgeJumpTime + playerData.timeToEnableMovingAfterLedgeJump)
        {
            if (player.WallGrabState.CanGrab())
            {
                Debug.Log("Entering Wall Grab State");
                playerStateMachine.ChangeState(player.WallGrabState);
            }
            else
            {
                playerStateMachine.ChangeState(player.WallSlideState);
            }
        }
        else
        {
            if (Time.time >= disableMovingAfterLedgeJumpTime + playerData.timeToEnableMovingAfterLedgeJump)
            {
                player.CheckIfShouldFlip(xInput);
                player.SetVelocityX(player.currentMovementSpeed * xInput);
            }
            
            player.Anim.SetFloat("yVelocity", player.CurrentVelocity.y);
            player.Anim.SetFloat("xVelocity", Mathf.Abs(player.CurrentVelocity.x));
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
        isTouchingLedge = player.CheckIfTouchingLedge();

        if (isTouchingWall && !isTouchingLedge)
        {
            player.LedgeClimbState.SetDetectedPosition(player.transform.position);
        }
    }

    private void CheckJumpMultiplier()
    {
        if (isJumping)
        {
            if (jumpInputStop)
            {
                player.SetVelocityY(player.CurrentVelocity.y * playerData.jumpHeightMultiplier);
                isJumping = false;
            }
            else if (player.CurrentVelocity.y <= 0f)
            {
                isJumping = false;
            }
        }
    }

    public void SetIsJumping() => isJumping = true;
}
