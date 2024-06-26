using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    public static Player Instance; //Singleton instance

    [SerializeField] private AudioSource playerAudio;
    
    #region State Variables

    public PlayerStateMachine StateMachine { get; private set; }
    public Player_IdleState IdleState { get; private set; }
    public Player_MoveState MoveState { get; private set; }
    public Player_JumpState JumpState { get; private set; }
    public Player_InAirState InAirState { get; private set; }
    public Player_LandState LandState { get; private set; }
    public Player_WallSlideState WallSlideState { get; private set; }
    public Player_WallGrabState WallGrabState { get; private set; }
    public Player_WallJumpState WallJumpState { get; private set; }
    public Player_LedgeClimbState LedgeClimbState { get; private set; }
    public Player_PrimaryAttackState PrimaryAttackState { get; private set; }
    public Player_SecondaryAttackState SecondaryAttackState { get; private set; }
    public Player_FinishingAttackState FinishingAttackState { get; private set; }
    public Player_BlockState BlockState { get; private set; }
    public Player_ClimbLadder ClimbLadderState { get; private set; }
    public Player_HurtState HurtState { get; private set; }
    public Player_DeadState DeadState { get; private set; }
    
    [SerializeField] private D_Player playerData;

    #endregion

    #region Stats Variables

    public enum AvailableStats
    {
        MovementSpeed,
        JumpHeight,
    };
    
    //Movement Speed
    public Stat_MovementSpeed MovementSpeed { get; private set; }
    public float currentMovementSpeed => MovementSpeed.CurrentValue; //TODO: Change the naming as it's not current speed but current max acceleration speed
    
    [HideInInspector] public float movementSpeedWorkspace; //It's pretty much current speed of the player, acceleration is also calculated here 
    
    //Jump Height
    public Stat_JumpHeight JumpHeight { get; private set; }
    public float currentJumpHeight => JumpHeight.CurrentValue;
    
    [HideInInspector] public float jumpHeightWorkspace; //Unused for now - Will be used with effect
    
    #endregion
    
    #region Components
    
    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    public BoxCollider2D PlayerCollider { get; private set; }
    public AudioSource PlayerAudio { get; private set; }
    
    #endregion

    #region Other Variables
    
    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; private set; }
    
    private Vector2 velocityHolder;

    
    public float currentHealth;
    [HideInInspector] public bool isHurt; //Trigger Hurt State
    [HideInInspector] public bool isDead; //Trigger Dead State
    [HideInInspector] public bool isNearLadder; //Check if ladder is near
    [HideInInspector] public bool isBlocking;
    
    
    
    private float comboTimer;
    private int comboState = 2;
    
    public AudioClip attackClip;
    public AudioClip attackHitClip;
    public AudioClip shieldBlockClip;
    
    #endregion
    
    #region Transforms

    public Transform primaryAttackArea;
    public Transform secondaryAttackArea;
    public Transform finishingAttackArea;
    public Transform enemyOrientedOffsetPositionFront;
    public Transform enemyOrientedOffsetPositionBack;
    
    #endregion

    #region Unity Callback Functions
    
    private void Awake()
    {
        Instance = this;
        
        //STATES
        //Initialize StateMachine
        StateMachine = new PlayerStateMachine();
        
        //Initialize the states
        IdleState = new Player_IdleState(this, StateMachine, playerData, "isIdle");
        MoveState = new Player_MoveState(this, StateMachine, playerData, "isMoving");
        JumpState = new Player_JumpState(this, StateMachine, playerData, "isInAir");
        InAirState = new Player_InAirState(this, StateMachine, playerData, "isInAir");
        LandState = new Player_LandState(this, StateMachine, playerData, "isLanding");
        WallSlideState = new Player_WallSlideState(this, StateMachine, playerData, "isHanging");
        WallGrabState = new Player_WallGrabState(this, StateMachine, playerData, "isHanging");
        WallJumpState = new Player_WallJumpState(this, StateMachine, playerData, "isInAir");
        LedgeClimbState = new Player_LedgeClimbState(this, StateMachine, playerData, "isInLedgeClimbState");
        PrimaryAttackState = new Player_PrimaryAttackState(this, StateMachine, playerData, "isAttackingPrimary");
        SecondaryAttackState = new Player_SecondaryAttackState(this, StateMachine, playerData, "isAttackingSecondary");
        FinishingAttackState = new Player_FinishingAttackState(this, StateMachine, playerData, "isAttackingFinishing");
        BlockState = new Player_BlockState(this, StateMachine, playerData, "isBlocking");
        ClimbLadderState = new Player_ClimbLadder(this, StateMachine, playerData, "isClimbingLadder");
        HurtState = new Player_HurtState(this, StateMachine, playerData, "isHurt");
        DeadState = new Player_DeadState(this, StateMachine, playerData, "isDead");
        
        //STATS
        //Initialize the statistics
        MovementSpeed = new Stat_MovementSpeed(playerData.baseMovementSpeed, playerData.maximumMovementSpeed, 
            playerData.triggerEffectMovementSpeed);
        JumpHeight = new Stat_JumpHeight(playerData.baseJumpHeight, playerData.maximumJumpHeight,
            playerData.triggerEffectJumpHeight);

    }

    private void Start()
    {
        currentHealth = playerData.maxHealth;
        FacingDirection = 1;
        
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        Rb = GetComponent<Rigidbody2D>();
        PlayerCollider = GetComponent<BoxCollider2D>();
        PlayerAudio = playerAudio;
        
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        CurrentVelocity = Rb.velocity;
        StateMachine.CurrentState.LogicUpdate();

        if (CheckIfComboAvailable())
            comboTimer -= Time.deltaTime;

        if (!CheckIfComboAvailable())
            comboState = 2;
    }

    private void FixedUpdate()
    {
        CheckIfGrounded();     //Does nothing, just for debugging
        CheckIfTouchingWall(); //Does nothing, just for debugging
        StateMachine.CurrentState.PhysicsUpdate();
    }
    
    #endregion

    #region Set Functions

    public void SetVelocityZero()
    {
        Rb.velocity = Vector2.zero;
        CurrentVelocity = Vector2.zero;
    }
    
    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        velocityHolder.Set(angle.x * velocity * direction, angle.y * velocity);
        Rb.velocity = velocityHolder;
        CurrentVelocity = velocityHolder;
    }
    
    public void SetVelocityX(float velocity)
    {
        velocityHolder.Set(velocity, CurrentVelocity.y);
        Rb.velocity = velocityHolder;
        CurrentVelocity = velocityHolder;
    }

    public void SetVelocityY(float velocity)
    {
        velocityHolder.Set(CurrentVelocity.x, velocity);
        Rb.velocity = velocityHolder;
        CurrentVelocity = velocityHolder;
    }

    public void SetComboTimer(float time)
    {
        comboTimer = time;
    }

    public void SetComboState(int state)
    {
        comboState = state;
    }
    
    #endregion

    #region Check Functions

    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }
    
    public bool CheckIfGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(PlayerCollider.bounds.center, PlayerCollider.bounds.size, 0f,
            Vector2.down, playerData.groundCheckRange, playerData.groundLayerMask);
        Color rayColor = (raycastHit.collider != null) ? Color.green : Color.red;

        Debug.DrawRay(PlayerCollider.bounds.center, Vector2.down * (PlayerCollider.bounds.extents.y + playerData.groundCheckRange), rayColor);
        return raycastHit.collider != null;
    }

    public bool CheckIfTouchingWall()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(PlayerCollider.bounds.center, transform.right,
            PlayerCollider.bounds.extents.x + playerData.wallCheckRange, playerData.wallLayerMask);
        Color rayColor = (raycastHit.collider != null) ? Color.green : Color.red;
        
        Debug.DrawRay(PlayerCollider.bounds.center, transform.right * (PlayerCollider.bounds.extents.x + playerData.wallCheckRange), rayColor);
        return raycastHit.collider != null;
    }

    public bool CheckIfTouchingLedge()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(new Vector2(PlayerCollider.bounds.center.x, PlayerCollider.bounds.max.y), transform.right,
            PlayerCollider.bounds.extents.x + playerData.ledgeCheckRange, playerData.wallLayerMask);
        Color rayColor = (raycastHit.collider != null) ? Color.green : Color.red;
        
        Debug.DrawRay(new Vector2(PlayerCollider.bounds.center.x, PlayerCollider.bounds.max.y), transform.right * (PlayerCollider.bounds.extents.x + playerData.ledgeCheckRange), rayColor);
        return raycastHit.collider != null;
    }

    public bool CheckIfComboAvailable() => comboTimer > 0;
    public int CheckComboState() => comboState;

    public bool CheckIfBlocking()
    {
        return isBlocking;
    }
    

    #endregion

    #region Other Functions

    public Vector2 DetermineCornerPosition()
    {
        Vector2 ledgeCheck = new Vector2(PlayerCollider.bounds.center.x, PlayerCollider.bounds.max.y);
        Vector2 wallCheck = PlayerCollider.bounds.center;


        RaycastHit2D xHit = Physics2D.Raycast(wallCheck, transform.right,
            PlayerCollider.bounds.extents.x + playerData.wallCheckRange, playerData.groundLayerMask);
        float xDistance = xHit.distance;
        velocityHolder.Set(xDistance * FacingDirection, 0f);

        RaycastHit2D yHit = Physics2D.Raycast(ledgeCheck + (velocityHolder), Vector2.down, ledgeCheck.y - wallCheck.y,
            playerData.groundLayerMask);
        float yDistance = yHit.distance;
        velocityHolder.Set(wallCheck.x + (xDistance * FacingDirection), ledgeCheck.y - yDistance);

        return velocityHolder;
    }

    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
    
    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    
    public void TakeDamage(AttackDetails attackDetails)
    {
        currentHealth -= attackDetails.damageAmount;
        Mathf.Clamp(currentHealth, 0, playerData.maxHealth);

        if (currentHealth > 0)
        {
            Debug.Log("Player is hurt bool set to true !");
            isHurt = true;
        }
        else if (currentHealth <= 0 && !isDead)
        {
            Debug.Log("Player is dead bool set to true !");
            isDead = true;
        }
        
    }

    //Used to operate with enum system
    public void AcquireStat(StatSystem_Core stat, float value)
    {
        stat.Add(value);
    }

    public void LoseStat(StatSystem_Core stat, float value)
    {
        stat.Subtract(value);
    }


    public void InstaDead()
    {
        SceneManager.LoadScene(2);
    }
    
    
    #endregion
    
}
