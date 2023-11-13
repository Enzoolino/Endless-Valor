using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyState
{
    private GameObject _player;
    
    public EnemyChaseState(Enemy enemy, EnemyStateMachine enemyStateMachine, string animationBoolName) : base(enemy, enemyStateMachine, animationBoolName)
    {
        _player = GameObject.FindGameObjectWithTag("Player");
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
    }
}
