using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationToStateMachine : MonoBehaviour
{
    public EnemyAttackState attackState;

    private void TriggerAttack()
    {
        attackState.TriggerAttack();
    }

    private void FinishAttack()
    {
        attackState.FinishAttack();
    }
}
