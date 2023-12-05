using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationToStateMachine : MonoBehaviour
{
    public Enemy_AttackState attackState;

    private void TriggerAttack()
    {
        attackState.TriggerAttack();
    }

    private void FinishAttack()
    {
        attackState.FinishAttack();
    }
}
