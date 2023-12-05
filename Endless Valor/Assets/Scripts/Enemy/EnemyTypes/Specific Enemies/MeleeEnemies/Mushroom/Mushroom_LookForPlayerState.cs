using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom_LookForPlayerState : Enemy_LookForPlayerState
{
    private Mushroom enemy;
    
    public Mushroom_LookForPlayerState(Enemy enemy, EnemyStateMachine enemyStateMachine, string animationBoolName, D_Enemy_LookForPlayerState stateData, Mushroom specificEnemy, EnemyEmotesHandler emotesHandler) : base(enemy, enemyStateMachine, animationBoolName, stateData, emotesHandler)
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
            enemyStateMachine.ChangeState(enemy.MoveState);
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }
}
