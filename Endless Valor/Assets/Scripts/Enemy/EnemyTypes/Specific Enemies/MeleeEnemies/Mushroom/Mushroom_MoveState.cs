
public class Mushroom_MoveState : Enemy_MoveState
{
    private Mushroom enemy;
    
    public Mushroom_MoveState(Enemy enemy, EnemyStateMachine enemyStateMachine, string animationBoolName, D_Enemy_MoveState stateData, Mushroom enemySpecific) : base(enemy, enemyStateMachine, animationBoolName, stateData)
    {
        this.enemy = enemySpecific;
    }


    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isPlayerInCloseAggroRange)
        {
            enemyStateMachine.ChangeState(enemy.PlayerDetectedState);
        }
        else if (isDetectingWall || isDetectingLedge)
        {
            enemy.IdleState.SetFlipAfterIdle(true);
            enemyStateMachine.ChangeState(enemy.IdleState);
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
