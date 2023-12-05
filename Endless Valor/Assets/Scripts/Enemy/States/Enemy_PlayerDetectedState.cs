
using UnityEngine;

public class Enemy_PlayerDetectedState : EnemyState
{
    protected D_Enemy_PlayerDetectedState stateData;
    protected EnemyEmotesHandler emotesHandler;

    protected bool isPlayerInCloseAggroRange;
    protected bool isPlayerInFarAggroRange;
    protected bool performCloseRangeAction;
    protected bool performLongRangeAction;

    protected bool wasLookingForPlayer; // Instantly perform long range action if this is true
    
    public Enemy_PlayerDetectedState(Enemy enemy, EnemyStateMachine enemyStateMachine, string animationBoolName, D_Enemy_PlayerDetectedState stateData, EnemyEmotesHandler emotesHandler) : base(enemy, enemyStateMachine, animationBoolName)
    {
        this.stateData = stateData;
        this.emotesHandler = emotesHandler;
    }
    
    
    public override void EnterState()
    {
        base.EnterState();
        
        // Handle emote
        emotesHandler.SetEmoteVisibility(true);
        emotesHandler.PlayerDetectedEmoteHandler(); 
        
        performLongRangeAction = false;
        
        enemy.SetVelocity(0f);
    }

    public override void ExitState()
    {
        base.ExitState();
        emotesHandler.SetEmoteVisibility(false);
        emotesHandler.PlayerDetectedEmoteHandler(); //Handle emote
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        if (Time.time >= startTime + stateData.longRangeActionTime || wasLookingForPlayer)
        {
            Debug.Log("Performing long range action!");
            performLongRangeAction = true;
            wasLookingForPlayer = false;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();
        
        isPlayerInCloseAggroRange = enemy.CheckPlayerInCloseAggroRange();
        isPlayerInFarAggroRange = enemy.CheckPlayerInFarAggroRange();
        performCloseRangeAction = enemy.CheckPlayerInCloseRangeAction();
    }
}
