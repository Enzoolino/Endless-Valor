using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEye : FlyingEnemy
{
    #region States Properties

    public FlyingEye_IdleState IdleState { get; private set; }
    public FlyingEye_PlayerDetectedState PlayerDetectedState { get; private set; }
    public FlyingEye_LookForPlayerState LookForPlayerState { get; private set; }
    public FlyingEye_ChargeState ChargeState { get; private set; }
    public FlyingEye_MeleeAttackState MeleeAttackState { get; private set; }
    public FlyingEye_HurtState HurtState { get; private set; }
    public FlyingEye_FallState FallState { get; private set; }
    public FlyingEye_DeadState DeadState { get; private set; }
    

#endregion
    
    #region Other Properties
    
    public EnemyEmotesHandler EmotesHandler { get; set; }
    
    #endregion
    
    #region Data Variables

    //Data
    [SerializeField] private D_Enemy_IdleState idleStateData;
    [SerializeField] private D_Enemy_PlayerDetectedState playerDetectedStateData;
    [SerializeField] private D_Enemy_LookForPlayerState lookForPlayerStateData;
    [SerializeField] private D_Enemy_ChargeState chargeStateData;
    [SerializeField] private D_Enemy_MeleeAttackState meleeAttackStateData;
    [SerializeField] private D_Enemy_HurtState hurtStateData;
    [SerializeField] private D_Enemy_FallState fallStateData;
    [SerializeField] private D_Enemy_DeadState deadStateData;

    //Transforms
    [SerializeField] private Transform meleeAttackPosition;
    
    //Audio
    public AudioSource enemyAudio;
    public AudioClip flyingEyeHurt;
    public AudioClip flyingEyeDeath;
    
    #endregion


    public override void Start()
    {
        base.Start();
        EmotesHandler = GetComponent<EnemyEmotesHandler>();

        IdleState = new FlyingEye_IdleState(this, StateMachine, "isIdle", idleStateData, this);
        PlayerDetectedState = new FlyingEye_PlayerDetectedState(this, StateMachine, "isPlayerDetected", playerDetectedStateData, this, EmotesHandler);
        LookForPlayerState = new FlyingEye_LookForPlayerState(this, StateMachine, "isLookingForPlayer", lookForPlayerStateData, this, EmotesHandler);
        ChargeState = new FlyingEye_ChargeState(this, StateMachine, "isCharging", chargeStateData, this);
        MeleeAttackState = new FlyingEye_MeleeAttackState(this, StateMachine, "isAttackingNormal", meleeAttackPosition, meleeAttackStateData, this);
        HurtState = new FlyingEye_HurtState(this, StateMachine, "isHurt", hurtStateData, this);
        FallState = new FlyingEye_FallState(this, StateMachine, "isFalling", fallStateData, this);
        DeadState = new FlyingEye_DeadState(this, StateMachine, "isDead", deadStateData, this);


        StateMachine.Initialize(IdleState);
    }


    public override void TakeDamage(AttackDetails attackDetails)
    {
        base.TakeDamage(attackDetails);

        if (isFalling) // When hp reaches 0 it transfers to fall state instead of dead state ( dead state transfer inside fall state)
        {
            StateMachine.ChangeState(FallState);
        }
        else if (isHurt)
        {
            StateMachine.ChangeState(HurtState);
        }
        
        //TODO: Dodać stun state ? Może nie
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }
}
