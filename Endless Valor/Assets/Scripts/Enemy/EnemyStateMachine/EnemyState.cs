using UnityEngine;

public class EnemyState
{
    protected Enemy enemy;
    protected EnemyStateMachine enemyStateMachine;

    protected bool isAnimationFinished;
    
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
        DoChecks();
        enemy.animator.SetBool(animationBoolName, true);
        startTime = Time.time;
        isAnimationFinished = false;
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
        DoChecks();
    }

    public virtual void DoChecks()
    {
        
    }

    public virtual void AnimationTrigger()
    {
        
    }

    public virtual void AnimationFinishTrigger()
    {
        isAnimationFinished = true;
    }

    public virtual void OnDestroy()
    {
        
    }
}
