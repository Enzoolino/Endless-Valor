using UnityEngine;

public class EnemyMoveState : EnemyState
{
    protected D_EnemyMoveState stateData;

    protected bool isDetectingWall;
    protected bool isDetectingLedge;
    
    public EnemyMoveState(Enemy enemy, EnemyStateMachine enemyStateMachine, string animationBoolName, D_EnemyMoveState stateData) : base(enemy, enemyStateMachine, animationBoolName)
    {
        this.stateData = stateData;
    }

    public override void EnterState()
    {
        base.EnterState();
        enemy.SetVelocity(stateData.speed);

        isDetectingLedge = enemy.CheckLedge();
        isDetectingWall = enemy.CheckWall();
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
        isDetectingLedge = enemy.CheckLedge();
        isDetectingWall = enemy.CheckWall();
    }
}
