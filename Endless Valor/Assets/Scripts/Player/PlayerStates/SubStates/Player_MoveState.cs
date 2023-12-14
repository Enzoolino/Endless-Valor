using UnityEngine;

public class Player_MoveState : Player_GroundedState
{
   
    private bool effectTriggerCheck;
    
    public Player_MoveState(Player player, PlayerStateMachine playerStateMachine, D_Player playerData, string animationBoolName) : base(player, playerStateMachine, playerData, animationBoolName)
    {
        
    }

    public override void EnterState()
    {
        base.EnterState();

        player.movementSpeedWorkspace = playerData.baseMovementSpeed;
    }

    public override void ExitState()
    {
        base.ExitState();
        
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (effectTriggerCheck)
        {
            player.MovementSpeed.EnableTriggerEffect();
        }
        
        player.CheckIfShouldFlip(xInput);

        if (player.movementSpeedWorkspace < player.currentMovementSpeed)
        {
            player.movementSpeedWorkspace += playerData.acceleration * Time.deltaTime;
        }
        
        player.SetVelocityX(player.movementSpeedWorkspace * xInput);

        
        if (xInput == 0 && !isExitingState)
        {
            playerStateMachine.ChangeState(player.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();
        effectTriggerCheck = player.MovementSpeed.CheckIfTriggerEffect();
    }
}
