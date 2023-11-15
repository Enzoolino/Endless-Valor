using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Mushroom : MeleeEnemy
{
    public Mushroom_IdleState IdleState { get; private set; }
    public Mushroom_MoveState MoveState { get; private set; }
    public Mushroom_PlayerDetectedState PlayerDetectedState { get; private set; }
    public Mushroom_ChargeState ChargeState { get; private set; }
    public Mushroom_LookForPlayerState LookForPlayerState { get; private set; }
    public Mushroom_MeleeAttackState MeleeAttackState { get; private set; }
    public Mushroom_StunState StunState { get; private set; }


    [SerializeField] private D_EnemyIdleState idleStateData;
    [SerializeField] private D_EnemyMoveState moveStateData;
    [SerializeField] private D_PlayerDetectedState playerDetectedStateData;
    [SerializeField] private D_EnemyChargeState chargeStateData;
    [SerializeField] private D_LookForPlayerState lookForPlayerStateData;
    [SerializeField] private D_EnemyMeleeAttackState meleeAttackStateData;
    [SerializeField] private D_EnemyStunState stunStateData;

    [SerializeField] private Transform meleeAttackPosition;

    public override void Start()
    {
        base.Start();
        IdleState = new Mushroom_IdleState(this, StateMachine, "isIdle", idleStateData, this);
        MoveState = new Mushroom_MoveState(this, StateMachine, "isWalking", moveStateData, this); //TODO: Change the animation name
        PlayerDetectedState = new Mushroom_PlayerDetectedState(this, StateMachine, "isPlayerDetected", playerDetectedStateData, this);
        ChargeState = new Mushroom_ChargeState(this, StateMachine, "isCharging", chargeStateData, this);
        LookForPlayerState = new Mushroom_LookForPlayerState(this, StateMachine, "isLookingForPlayer", lookForPlayerStateData, this);
        MeleeAttackState = new Mushroom_MeleeAttackState(this, StateMachine, "isAttackingNormal", meleeAttackPosition, meleeAttackStateData, this);
        StunState = new Mushroom_StunState(this, StateMachine, "isStunned", stunStateData, this);

        StateMachine.Initialize(MoveState);
    }

    public override void TakeDamage(AttackDetails attackDetails)
    {
        base.TakeDamage(attackDetails);

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
