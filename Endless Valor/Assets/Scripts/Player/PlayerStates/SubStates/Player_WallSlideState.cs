using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_WallSlideState : Player_TouchingWallState
{
    public Player_WallSlideState(Player player, PlayerStateMachine playerStateMachine, D_Player playerData, string animationBoolName) : base(player, playerStateMachine, playerData, animationBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        if (!isExitingState)
        {
            player.RB.gravityScale = 0f;
            player.SetVelocityY(-playerData.slideSpeed);
        }
        
    }

    public override void ExitState()
    {
        base.ExitState();
        player.RB.gravityScale = 1f;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }
}
