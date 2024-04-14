using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEye_DeadState : Enemy_DeadState
{
    private FlyingEye enemy;
    
    public FlyingEye_DeadState(Enemy enemy, EnemyStateMachine enemyStateMachine, string animationBoolName, D_Enemy_DeadState stateData, FlyingEye enemySpecific) : base(enemy, enemyStateMachine, animationBoolName, stateData)
    {
        this.enemy = enemySpecific;
    }
    
    public override void EnterState()
    {
        base.EnterState();

        enemy.enemyAudio.clip = enemy.flyingEyeDeathSound;
        enemy.enemyAudio.Play();
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
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }
}
