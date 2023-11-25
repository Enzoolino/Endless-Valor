using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
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
    
    [SerializeField] private D_Player playerData;

    #endregion
    
    #region Components
    
    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public BoxCollider2D PlayerCollider { get; private set; }
    
    #endregion

    #region Other Variables

    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; private set; }
    
    private Vector2 velocityHolder;

    #endregion

    #region Unity Callback Functions
    
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();
        
        IdleState = new Player_IdleState(this, StateMachine, playerData, "isIdle");
        MoveState = new Player_MoveState(this, StateMachine, playerData, "isMoving");
        JumpState = new Player_JumpState(this, StateMachine, playerData, "isInAir");
        InAirState = new Player_InAirState(this, StateMachine, playerData, "isInAir");
        LandState = new Player_LandState(this, StateMachine, playerData, "isLanding");
        WallSlideState = new Player_WallSlideState(this, StateMachine, playerData, "isHanging");
        WallGrabState = new Player_WallGrabState(this, StateMachine, playerData, "isHanging");
        WallJumpState = new Player_WallJumpState(this, StateMachine, playerData, "isInAir");
        LedgeClimbState = new Player_LedgeClimbState(this, StateMachine, playerData, "isInLedgeClimbState");

    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();
        PlayerCollider = GetComponent<BoxCollider2D>();

        FacingDirection = 1;
        
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        CurrentVelocity = RB.velocity;
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        CheckIfGrounded();     //Does nothing, just to debugging
        CheckIfTouchingWall(); //Does nothing, just for debugging
        StateMachine.CurrentState.PhysicsUpdate();
    }
    
    #endregion

    #region Set Functions

    public void SetVelocityZero()
    {
        RB.velocity = Vector2.zero;
        CurrentVelocity = Vector2.zero;
    }
    
    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        velocityHolder.Set(angle.x * velocity * direction, angle.y * velocity);
        RB.velocity = velocityHolder;
        CurrentVelocity = velocityHolder;
    }
    
    public void SetVelocityX(float velocity)
    {
        velocityHolder.Set(velocity, CurrentVelocity.y);
        RB.velocity = velocityHolder;
        CurrentVelocity = velocityHolder;
    }

    public void SetVelocityY(float velocity)
    {
        velocityHolder.Set(CurrentVelocity.x, velocity);
        RB.velocity = velocityHolder;
        CurrentVelocity = velocityHolder;
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
            PlayerCollider.bounds.extents.x + playerData.wallCheckRange, playerData.wallLayerMask);
        Color rayColor = (raycastHit.collider != null) ? Color.green : Color.red;
        
        Debug.DrawRay(new Vector2(PlayerCollider.bounds.center.x, PlayerCollider.bounds.max.y), transform.right * (PlayerCollider.bounds.extents.x + playerData.wallCheckRange), rayColor);
        return raycastHit.collider != null;
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

    #endregion
    
}