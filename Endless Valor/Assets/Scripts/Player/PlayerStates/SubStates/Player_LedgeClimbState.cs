using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_LedgeClimbState : PlayerState
{
    private Vector2 detectedAtPos;
    private Vector2 cornerPos;
    private Vector2 startPos;
    private Vector2 stopPos;

    private bool isHanging;
    private bool isClimbing;
    private bool isTouchingWall;

    private int xInput;
    private int yInput;
    private bool jumpInput;
    
    
    public Player_LedgeClimbState(Player player, PlayerStateMachine playerStateMachine, D_Player playerData, string animationBoolName) : base(player, playerStateMachine, playerData, animationBoolName)
    {
    }

    public void SetDetectedPosition(Vector2 pos) => detectedAtPos = pos;

    public override void EnterState()
    {
        base.EnterState();
        
        player.SetVelocityZero();
        player.transform.position = detectedAtPos;
        cornerPos = player.DetermineCornerPosition();
        
        startPos.Set(cornerPos.x - (player.FacingDirection * playerData.startOffset.x), cornerPos.y - playerData.startOffset.y);
        stopPos.Set(cornerPos.x + (player.FacingDirection * playerData.stopOffset.x), cornerPos.y + playerData.stopOffset.y);

        player.transform.position = startPos;
    }

    public override void ExitState()
    {
        base.ExitState();
        
        isHanging = false;
        isClimbing = false;
        
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            player.SetVelocityZero();
            player.Rb.AddForce(new Vector2(0f, playerData.ledgeJumpForce), ForceMode2D.Impulse);
            player.InAirState.disableMovingAfterLedgeJumpTime = Time.time;
            player.InAirState.grabLedgeStopTime = Time.time;
            playerStateMachine.ChangeState(player.InAirState);
        }
        else
        {
            xInput = player.InputHandler.NormalizedInputX;
            yInput = player.InputHandler.NormalizedInputY;
            jumpInput = player.InputHandler.JumpInput;
        
            //player.SetVelocityZero();
            player.transform.position = startPos;

            if (xInput == player.FacingDirection && isHanging && !isClimbing)
            {
                isClimbing = true;
                player.Anim.SetBool("isClimbingLedge", true);
            }
            else if (yInput == -1 && isHanging && !isClimbing)
            {
                player.InAirState.grabLedgeStopTime = Time.time;
                playerStateMachine.ChangeState(player.WallSlideState);
            }
            else if (jumpInput && isHanging && !isClimbing)
            {
                playerStateMachine.ChangeState(player.WallJumpState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();
        
        isTouchingWall = player.CheckIfTouchingWall();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        
        isHanging = true;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        
        player.Anim.SetBool("isClimbingLedge", false);
    }
}
