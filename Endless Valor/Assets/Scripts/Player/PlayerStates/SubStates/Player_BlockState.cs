using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_BlockState : Player_AbilityState
{
    public Player_BlockState(Player player, PlayerStateMachine playerStateMachine, D_Player playerData, string animationBoolName) : base(player, playerStateMachine, playerData, animationBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
        player.InputHandler.UseBlockInput();
        player.isBlocking = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
        player.SetVelocityZero();

        if (player.InputHandler.BlockInputStop)
            isAbilityDone = true;
        
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        player.isBlocking = true;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        isAbilityDone = true;
    }
}
