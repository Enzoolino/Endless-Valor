
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class FlyingEye_ChargeState : Enemy_ChargeState
{
    private FlyingEye enemy;
    private Vector3 externalTargetPoint;
    
    public FlyingEye_ChargeState(Enemy enemy, EnemyStateMachine enemyStateMachine, string animationBoolName, D_Enemy_ChargeState stateData, FlyingEye specificEnemy) : base(enemy, enemyStateMachine, animationBoolName, stateData)
    {
        this.enemy = specificEnemy;
    }

    public override void EnterState()
    {
        base.EnterState();

        enemy.enemyAudio.clip = enemy.flyingEyeChargeSound;
        enemy.enemyAudio.Play();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Player.Instance != null && !isChargeTimeOver)
        {
            enemy.CalculatedFlip(Player.Instance.transform.position.x);
            externalTargetPoint = enemy.CalculateCloserAttackPoint(Player.Instance.enemyOrientedOffsetPositionFront,
                Player.Instance.enemyOrientedOffsetPositionBack);
            enemy.ChaseTheTarget(externalTargetPoint, stateData.chargeSpeed);
            
        }
        
        if (performCloseRangeAction)
        {
            enemyStateMachine.ChangeState(enemy.ChargeAttackState);
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
