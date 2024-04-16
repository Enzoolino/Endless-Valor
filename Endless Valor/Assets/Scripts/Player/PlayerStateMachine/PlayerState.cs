using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine playerStateMachine;
    protected D_Player playerData;

    protected bool isAnimationFinished;
    protected bool isExitingState;
    
    protected float startTime;

    private string animationBoolName;

    private bool alreadyDied; //To mark the trigger of death state to not trigger it every frame

    public PlayerState(Player player, PlayerStateMachine playerStateMachine, D_Player playerData, string animationBoolName)
    {
        this.player = player;
        this.playerStateMachine = playerStateMachine;
        this.playerData = playerData;
        this.animationBoolName = animationBoolName;
    }

    public virtual void EnterState()
    {
        DoChecks();
        player.Anim.SetBool(animationBoolName, true);
        startTime = Time.time;
        isAnimationFinished = false;
        isExitingState = false;
    }

    public virtual void ExitState()
    {
        player.Anim.SetBool(animationBoolName, false);
        isExitingState = true;
    }

    public virtual void LogicUpdate()
    {
        //Interrupt every action when player is hurt
        if (player.isDead && !alreadyDied)
        {
            Debug.Log("Entering Player Dead State");
            alreadyDied = true;
            playerStateMachine.ChangeState(player.DeadState);
        }
        else if (player.isHurt)
        {
            Debug.Log("Entering Player Hurt State");
            playerStateMachine.ChangeState(player.HurtState);
        }
    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks()
    {
        
    }

    public virtual void AnimationTrigger()
    {
        
    }

    public virtual void AnimationFinishTrigger()
    {
        isAnimationFinished = true;
    }
    

}
