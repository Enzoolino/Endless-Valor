
public class Mushroom_DeadState : Enemy_DeadState
{
    private Mushroom enemy;
    
    public Mushroom_DeadState(Enemy enemy, EnemyStateMachine enemyStateMachine, string animationBoolName, D_Enemy_DeadState stateData, Mushroom enemySpecific) : base(enemy, enemyStateMachine, animationBoolName, stateData)
    {
        this.enemy = enemySpecific;
    }

    public override void EnterState()
    {
        base.EnterState();

        enemy.enemyAudio.clip = enemy.mushroomDeathSound;
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
