using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationToStateMachine : MonoBehaviour
{
    public Enemy_AttackState attackState;
    public Enemy enemy;

    private void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    private void TriggerAttack()
    {
        attackState.TriggerAttack();
    }

    private void FinishAttack()
    {
        attackState.FinishAttack();
    }

    private void TriggerAction()
    {
        enemy.AnimationTrigger();
    }

    private void FinishAction()
    {
        enemy.AnimationFinishTrigger();
    }
    
}
