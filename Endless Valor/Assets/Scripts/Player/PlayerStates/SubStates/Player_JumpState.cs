using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_JumpState : Player_AbilityState
{
    private int amountOfJumpsLeft;
    
    public Player_JumpState(Player player, PlayerStateMachine playerStateMachine, D_Player playerData, string animationBoolName) : base(player, playerStateMachine, playerData, animationBoolName)
    {
        amountOfJumpsLeft = playerData.amountOfJumps;
    }

    public override void EnterState()
    {
        base.EnterState();
        
            Debug.Log("Ustawianie velocity skoku");
            player.InputHandler.UseJumpInput();
            player.SetVelocityY(playerData.jumpVelocity);
            isAbilityDone = true;
            amountOfJumpsLeft--;
            player.InAirState.SetIsJumping();
    }

    public bool CanJump()
    {
        return amountOfJumpsLeft > 0;
    }

    public void ResetAmountOfJumpsLeft() => amountOfJumpsLeft = playerData.amountOfJumps;

    public void DecreaseAmountOfJumpsLeft() => amountOfJumpsLeft--;

}
