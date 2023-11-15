
using UnityEngine;

public class PlayerDetectedState : EnemyState
{
    protected D_PlayerDetectedState stateData;

    protected bool isPlayerInCloseAggroRange;
    protected bool isPlayerInFarAggroRange;
    protected bool performCloseRangeAction;
    protected bool performLongRangeAction;
    
    public PlayerDetectedState(Enemy enemy, EnemyStateMachine enemyStateMachine, string animationBoolName, D_PlayerDetectedState stateData) : base(enemy, enemyStateMachine, animationBoolName)
    {
        this.stateData = stateData;
    }


    public override void EnterState()
    {
        base.EnterState();
        performLongRangeAction = false;

        enemy.SetVelocity(0f);
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + stateData.longRangeActionTime)
        {
            performLongRangeAction = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();
        
        isPlayerInCloseAggroRange = enemy.CheckPlayerInCloseAggroRange();
        isPlayerInFarAggroRange = enemy.CheckPlayerInFarAggroRange();
        performCloseRangeAction = enemy.CheckPlayerInCloseRangeAction();
    }
}
