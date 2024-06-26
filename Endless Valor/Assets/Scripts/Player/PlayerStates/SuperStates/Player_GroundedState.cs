using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_GroundedState : PlayerState
{
    protected int xInput;
    protected int yInput;
    
    private bool jumpInput;
    private bool primaryAttackInput;
    private bool ladderClimbInput;
    private bool blockInput;
    
    
    private bool isGrounded;
    
    
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
        blockInput = player.InputHandler.BlockInput;
        ladderClimbInput = player.InputHandler.LadderClimbInput;

        if (jumpInput && player.JumpState.CanJump())
        {
            playerStateMachine.ChangeState(player.JumpState);
        }
        else if (primaryAttackInput && !player.CheckIfComboAvailable())
        {
            playerStateMachine.ChangeState(player.PrimaryAttackState);
        }
        else if (primaryAttackInput && player.CheckIfComboAvailable() && player.CheckComboState() == 2)
        {
            playerStateMachine.ChangeState(player.SecondaryAttackState);
        }
        else if (primaryAttackInput && player.CheckIfComboAvailable() && player.CheckComboState() == 3)
        {
            playerStateMachine.ChangeState(player.FinishingAttackState);
        }
        else if (blockInput)
        {
            playerStateMachine.ChangeState(player.BlockState);
        }
        else if (ladderClimbInput && player.isNearLadder)
        {
            playerStateMachine.ChangeState(player.ClimbLadderState);
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
