using UnityEngine;

public class Enemy_MoveState : EnemyState
{
    protected D_Enemy_MoveState stateData;

    protected bool isDetectingWall;
    protected bool isDetectingLedge;
    protected bool isPlayerInCloseAggroRange;
    
    public Enemy_MoveState(Enemy enemy, EnemyStateMachine enemyStateMachine, string animationBoolName, D_Enemy_MoveState stateData) : base(enemy, enemyStateMachine, animationBoolName)
    {
        this.stateData = stateData;
    }

    public override void EnterState()
    {
        base.EnterState();
        enemy.SetVelocity(stateData.speed);

        
    }

    public override void ExitState()
    {
        base.ExitState();
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
        
        isDetectingLedge = enemy.CheckLedge();
        isDetectingWall = enemy.CheckWall();
        isPlayerInCloseAggroRange = enemy.CheckPlayerInCloseAggroRange();
    }
}
