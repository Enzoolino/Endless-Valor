using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_LandState : Player_GroundedState
{
    public Player_LandState(Player player, PlayerStateMachine playerStateMachine, D_Player playerData, string animationBoolName) : base(player, playerStateMachine, playerData, animationBoolName)
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

        if (!isExitingState)
        {
            if (xInput != 0)
            {
                playerStateMachine.ChangeState(player.MoveState);
            }
            else if (isAnimationFinished)
            {
                playerStateMachine.ChangeState(player.IdleState);
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
}
