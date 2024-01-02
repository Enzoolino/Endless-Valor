using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEye_IdleState : Enemy_IdleState
{
    private FlyingEye enemy;
    
    public FlyingEye_IdleState(Enemy enemy, EnemyStateMachine enemyStateMachine, string animationBoolName, D_Enemy_IdleState stateData, FlyingEye enemySpecific) : base(enemy, enemyStateMachine, animationBoolName, stateData)
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
            Debug.Log("Bat: Changing State to PlayerDetected");
            enemyStateMachine.ChangeState(enemy.PlayerDetectedState);
        }
        
        if (!isAtInitialPosition)
        {
            enemy.CalculatedFlip(enemy.InitialPosition.x);
            enemy.ReturnToInitialPosition(enemy.InitialPosition, 6.0f); //TODO: Wywalić to z idle i zrobić nowy stan
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    
}
