using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_AbilityState : PlayerState
{
    protected bool isAbilityDone;
    private bool isGrounded;
    
    public Player_AbilityState(Player player, PlayerStateMachine playerStateMachine, D_Player playerData, string animationBoolName) : base(player, playerStateMachine, playerData, animationBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        isAbilityDone = false;
        Debug.Log("Wchodzenie do abilitystate a więc " + isAbilityDone);
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        if (isAbilityDone)
        {
            if (isGrounded && player.CurrentVelocity.y < 0.01f)
            {
                Debug.Log("Entering Idle State");
                playerStateMachine.ChangeState(player.IdleState);
            }
            else
            {
                Debug.Log("Entering Air State");
                playerStateMachine.ChangeState(player.InAirState);
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
        isGrounded = player.CheckIfGrounded();
    }
}
