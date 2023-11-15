

public class Mushroom_StunState : EnemyStunState
{
    private Mushroom enemy;
    
    public Mushroom_StunState(Enemy enemy, EnemyStateMachine enemyStateMachine, string animationBoolName, D_EnemyStunState stateData, Mushroom enemySpecific) : base(enemy, enemyStateMachine, animationBoolName, stateData)
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

        if (isStunTimeOver)
        {
            if (performCloseRangeAction)
            {
                enemyStateMachine.ChangeState(enemy.MeleeAttackState);
            }
            else if (isPlayerInCloseAggroRange)
            {
                enemyStateMachine.ChangeState(enemy.ChargeState);
            }
            else
            {
                enemy.LookForPlayerState.SetFlipImmediately(true);
                enemyStateMachine.ChangeState(enemy.LookForPlayerState);
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
    }
}
