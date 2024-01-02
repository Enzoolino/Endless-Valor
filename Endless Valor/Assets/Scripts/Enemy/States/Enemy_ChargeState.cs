
using UnityEngine;

public class Enemy_ChargeState : EnemyState
{
    protected D_Enemy_ChargeState stateData;

    protected bool isPlayerInCloseAggroRange;
    protected bool isDetectingLedge;
    protected bool isDetectingWall;
    protected bool isChargeTimeOver;
    protected bool performCloseRangeAction;
    
    public Enemy_ChargeState(Enemy enemy, EnemyStateMachine enemyStateMachine, string animationBoolName, D_Enemy_ChargeState stateData) : base(enemy, enemyStateMachine, animationBoolName)
    {
        this.stateData = stateData;
    }

    public override void EnterState()
    {
        base.EnterState();
        isChargeTimeOver = false;
        enemy.SetVelocity(stateData.chargeSpeed);
        Debug.Log("Entering Charge State");
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + stateData.chargeTime)
        {
            isChargeTimeOver = true;
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
        isDetectingLedge = enemy.CheckLedge();
        isDetectingWall = enemy.CheckWall();
        performCloseRangeAction = enemy.CheckPlayerInCloseRangeAction();
    }
}
