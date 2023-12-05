using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_GroundedState : PlayerState
{
    protected int xInput;
    protected int yInput;
    
    private bool jumpInput;
    private bool isGrounded;

    private bool primaryAttackInput;

    
    public Player_GroundedState(Player player, PlayerStateMachine playerStateMachine, D_Player playerData, string animationBoolName) : base(player, playerStateMachine, playerData, animationBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        
        player.WallGrabState.ResetAmountOfGrabsLeft();
        player.JumpState.ResetAmountOfJumpsLeft();
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
        primaryAttackInput = player.InputHandler.PrimaryAttackInput;

        if (jumpInput && player.JumpState.CanJump())
        {
            playerStateMachine.ChangeState(player.JumpState);
        }
        else if (primaryAttackInput)
        {
            playerStateMachine.ChangeState(player.PrimaryAttackState);
        }
        else if (!isGrounded)
        {
            player.JumpState.DecreaseAmountOfJumpsLeft();
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
    }
}
