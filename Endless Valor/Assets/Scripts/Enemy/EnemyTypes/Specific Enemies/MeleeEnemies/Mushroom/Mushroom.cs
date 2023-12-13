using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Component = System.ComponentModel.Component;

public class Mushroom : MeleeEnemy
{
    public Mushroom_IdleState IdleState { get; private set; }
    public Mushroom_MoveState MoveState { get; private set; }
    public Mushroom_PlayerDetectedState PlayerDetectedState { get; private set; }
    public Mushroom_ChargeState ChargeState { get; private set; }
    public Mushroom_LookForPlayerState LookForPlayerState { get; private set; }
    public Mushroom_MeleeAttackState MeleeAttackState { get; private set; }
    public Mushroom_StunState StunState { get; private set; }
    public Mushroom_HurtState HurtState { get; private set; }
    public Mushroom_DeadState DeadState { get; private set; }
    
    
    
    public EnemyEmotesHandler EmotesHandler { get; set; }


    [SerializeField] private D_Enemy_IdleState idleStateData;
    [SerializeField] private D_Enemy_MoveState moveStateData;
    [SerializeField] private D_Enemy_PlayerDetectedState playerDetectedStateData;
    [SerializeField] private D_Enemy_ChargeState chargeStateData;
    [SerializeField] private D_Enemy_LookForPlayerState lookForPlayerStateData;
    [SerializeField] private D_Enemy_MeleeAttackState meleeAttackStateData;
    [SerializeField] private D_Enemy_StunState stunStateData;
    [SerializeField] private D_Enemy_HurtState hurtStateData;
    [SerializeField] private D_Enemy_DeadState deadStateData;

    [SerializeField] private Transform meleeAttackPosition;

    public override void Start()
    {
        base.Start();
        EmotesHandler = GetComponent<EnemyEmotesHandler>();
        
        IdleState = new Mushroom_IdleState(this, StateMachine, "isIdle", idleStateData, this);
        MoveState = new Mushroom_MoveState(this, StateMachine, "isWalking", moveStateData, this); //TODO: Change the animation name
        PlayerDetectedState = new Mushroom_PlayerDetectedState(this, StateMachine, "isPlayerDetected", playerDetectedStateData, this, EmotesHandler);
        ChargeState = new Mushroom_ChargeState(this, StateMachine, "isCharging", chargeStateData, this);
        LookForPlayerState = new Mushroom_LookForPlayerState(this, StateMachine, "isLookingForPlayer", lookForPlayerStateData, this, EmotesHandler);
        MeleeAttackState = new Mushroom_MeleeAttackState(this, StateMachine, "isAttackingNormal", meleeAttackPosition, meleeAttackStateData, this);
        StunState = new Mushroom_StunState(this, StateMachine, "isStunned", stunStateData, this, EmotesHandler);
        HurtState = new Mushroom_HurtState(this, StateMachine, "isHurt", hurtStateData, this);
        DeadState = new Mushroom_DeadState(this, StateMachine, "isDead", deadStateData, this);
        
        StateMachine.Initialize(MoveState);
    }

    public override void TakeDamage(AttackDetails attackDetails)
    {
        base.TakeDamage(attackDetails);

        if (isDead)
        {
            StateMachine.ChangeState(DeadState);
        }
        else if (isHurt)
        {
            StateMachine.ChangeState(HurtState);
        }

        if (isStunned && StateMachine.CurrentState != StunState)
        {
            StateMachine.ChangeState(StunState);
        }
    }
    
    
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }
}
