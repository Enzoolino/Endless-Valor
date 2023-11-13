using UnityEngine;

public class EnemyState
{
    protected Enemy enemy;
    protected EnemyStateMachine enemyStateMachine;

    protected float startTime;

    protected string animationBoolName;

    public EnemyState(Enemy enemy, EnemyStateMachine enemyStateMachine, string animationBoolName)
    {
        this.enemy = enemy;
        this.enemyStateMachine = enemyStateMachine;
        this.animationBoolName = animationBoolName;
    }


    public virtual void EnterState()
    {
        startTime = Time.time;
        enemy.animator.SetBool(animationBoolName, true);
    }
    
    public virtual void ExitState()
    {
        enemy.animator.SetBool(animationBoolName, false);
    }

    public virtual void LogicUpdate()
    {
        
    }

    public virtual void PhysicsUpdate()
    {
        
    }
}
