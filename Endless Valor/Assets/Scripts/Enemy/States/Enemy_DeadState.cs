using UnityEngine;

public class Enemy_DeadState : EnemyState
{
    private D_Enemy_DeadState stateData;
    
    public Enemy_DeadState(Enemy enemy, EnemyStateMachine enemyStateMachine, string animationBoolName, D_Enemy_DeadState stateData) : base(enemy, enemyStateMachine, animationBoolName)
    {
        this.stateData = stateData;
    }

    public override void EnterState()
    {
        base.EnterState();
        enemy.Rb.bodyType = RigidbodyType2D.Static;
        enemy.boxCollider2D.enabled = false;
        
        //TODO: Check performance with bodies not removing
        //enemy.DestroyEnemyObject(stateData.deathDelay);
        
        if (player != null)
        {
            StatSystem_Core transferredStatToGive = null;

            switch (stateData.statToGive)
            {
                case Player.AvailableStats.MovementSpeed:
                    transferredStatToGive = player.MovementSpeed;
                    break;
                case Player.AvailableStats.JumpHeight:
                    transferredStatToGive = player.JumpHeight;
                    break;
            }
            
            player.AcquireStat(transferredStatToGive, stateData.statValueToGive);
        }
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
    
}
