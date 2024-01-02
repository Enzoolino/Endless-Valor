
public class FlyingEye_LookForPlayerState : Enemy_LookForPlayerState
{
    private FlyingEye enemy;
    
    public FlyingEye_LookForPlayerState(Enemy enemy, EnemyStateMachine enemyStateMachine, string animationBoolName, D_Enemy_LookForPlayerState stateData, FlyingEye specificEnemy,EnemyEmotesHandler emotesHandler) : base(enemy, enemyStateMachine, animationBoolName, stateData, emotesHandler)
    {
        this.enemy = specificEnemy;
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (isPlayerInCloseAggroRange)
        {
            enemyStateMachine.ChangeState(enemy.PlayerDetectedState);
        }
        else if (isAllTurnsTimeDone)
        {
            //TODO: Transition to back to place state - Change it - For now it changes by idle state
            
            enemyStateMachine.ChangeState(enemy.IdleState);
            
        }
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
