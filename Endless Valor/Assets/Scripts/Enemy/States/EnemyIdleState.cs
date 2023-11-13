
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    protected D_EnemyIdleState stateData;

    protected float idleTime;

    protected bool flipAfterIdle;
    protected bool isIdleTimeOver;
    
    public EnemyIdleState(Enemy enemy, EnemyStateMachine enemyStateMachine, string animationBoolName, D_EnemyIdleState stateData) : base(enemy, enemyStateMachine, animationBoolName)
    {
        this.stateData = stateData;
    }

    public override void EnterState()
    {
        base.EnterState();
        
        enemy.SetVelocity(0f);
        isIdleTimeOver = false;
        idleTime = Random.Range(stateData.minIdleTime, stateData.maxIdleTime);
    }

    public override void ExitState()
    {
        base.ExitState();

        if (flipAfterIdle)
        {
            enemy.Flip();
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + idleTime)
        {
            isIdleTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void SetFlipAfterIdle(bool flip)
    {
        flipAfterIdle = flip;
    }
}
