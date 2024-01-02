
public class FlyingEye_ChargeState : Enemy_ChargeState
{
    private FlyingEye enemy;
    
    public FlyingEye_ChargeState(Enemy enemy, EnemyStateMachine enemyStateMachine, string animationBoolName, D_Enemy_ChargeState stateData, FlyingEye specificEnemy) : base(enemy, enemyStateMachine, animationBoolName, stateData)
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

        if (Player.Instance != null)
        {
            enemy.CalculatedFlip(Player.Instance.transform.position.x);
            enemy.ChaseTheTarget(Player.Instance.enemyOrientedOffsetPosition, stateData.chargeSpeed);
        }
        
        if (performCloseRangeAction)
        {
            enemyStateMachine.ChangeState(enemy.MeleeAttackState);
        }
        else if (isChargeTimeOver)
        {
            if (isPlayerInCloseAggroRange)
            {
                enemyStateMachine.ChangeState(enemy.PlayerDetectedState);
            }
            else
            {
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

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }
}
