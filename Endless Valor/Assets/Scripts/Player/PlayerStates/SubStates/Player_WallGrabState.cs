using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_WallGrabState : Player_TouchingWallState
{
    private int amountOfGrabsLeft;
    
    
    public Player_WallGrabState(Player player, PlayerStateMachine playerStateMachine, D_Player playerData, string animationBoolName) : base(player, playerStateMachine, playerData, animationBoolName)
    {
        amountOfGrabsLeft = playerData.amountOfGrabs;
    }


    public override void EnterState()
    {
        base.EnterState();
        player.RB.gravityScale = 0f;
        player.SetVelocityZero();
        DecreaseAmountOfGrabsLeft();
    }

    public override void ExitState()
    {
        base.ExitState();
        player.RB.gravityScale = 1f;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            if (Time.time >= startTime + playerData.timeToStartSliding)
            {
                Debug.Log("Entering Wall Slide State");
                playerStateMachine.ChangeState(player.WallSlideState);
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
    }

    public bool CanGrab()
    {
        return amountOfGrabsLeft > 0;
    }

    public void ResetAmountOfGrabsLeft() => amountOfGrabsLeft = playerData.amountOfJumps;

    public void DecreaseAmountOfGrabsLeft() => amountOfGrabsLeft--;

}
