using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_DeadState : PlayerState
{
    private float deathScreenTimer;
    
    
    public Player_DeadState(Player player, PlayerStateMachine playerStateMachine, D_Player playerData, string animationBoolName) : base(player, playerStateMachine, playerData, animationBoolName)
    {
    }

    //TODO: Add some sounds when dying and add death screen

    
    //For now the state just blocks the StateMachine 
    public override void EnterState()
    {
        base.EnterState();
        //player.isDead = false;
        player.SetVelocityZero(); //Prevent sliding after death

        deathScreenTimer = playerData.deathScreenDelay;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        deathScreenTimer -= Time.deltaTime;

        Debug.Log($"czs do ekranu śmierci{deathScreenTimer}");
        if (deathScreenTimer <= 0)
        {
            player.InstaDead();
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

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }
}
