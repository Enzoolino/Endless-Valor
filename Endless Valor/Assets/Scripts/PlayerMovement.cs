using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float playerSpeed = 3.0f;
    [SerializeField] private float jumpHeight = 6.0f;
    
    [Header("Attacking")]
    [SerializeField] private float lightAttackCooldown = 0.5f;
    [SerializeField] private float heavyAttackCooldown = 1.0f;
    [SerializeField] private float lightAttackDamage = 25.0f;
    [SerializeField] private float heavyAttackDamage = 50.0f;
    [SerializeField] private Transform lightAttackBox;
    [SerializeField] private Transform heavyAttackBox;
    
    [Header("Wall Jumping")]
    [SerializeField] private float wallJumpTime = 0.2f;
    [SerializeField] private float wallSlideSpeed = 0.5f;
    [SerializeField] private Vector2 wallJumpForce;

    [Header("References")]
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private LayerMask wallsLayerMask;
    [SerializeField] private LayerMask enemyLayerMask;
    [SerializeField] private Animator animator;

    //Privates
    private Rigidbody2D character;
    private PlayerInputActions playerInputActions;
    private BoxCollider2D boxCollider2D;

    //Flags
    private int direction = 1; //1 - right -1 - left
    
    private bool canMove = true;
    
    private bool isRunning;
    private bool isJumping;
    private bool isFalling;
    
    private bool isAttacking;
    private bool isAttackingLight;
    private bool isAttackingHeavy;
    public bool isHurt;
    
    private bool isWallGrabbing;
    private bool isWallJumping;
    private bool isStartingWallJump;

    //Timers
    private float attackTimer;
    private float wallJumpTimer;
    

    #region Input Enable/Disable
    private void OnEnable()
    {
        playerInputActions.Player.Enable();
        playerInputActions.Player.Jump.performed += Jump;
        playerInputActions.Player.AttackLight.performed += LightAttack;
        playerInputActions.Player.AttackHeavy.performed += HeavyAttack;
    }

    private void OnDisable()
    {
        playerInputActions.Player.Disable();
        playerInputActions.Player.Jump.performed -= Jump;
        playerInputActions.Player.AttackLight.performed -= LightAttack;
        playerInputActions.Player.AttackHeavy.performed -= HeavyAttack;
    }
    #endregion

    private void Awake()
    {
        character = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        playerInputActions = new PlayerInputActions();
    }

    private void FixedUpdate()
    {
        MovementHandler(); // Simple A, D movement handling
        SetDirection(); // Player visuals are following direction
        AnimationHandler(); // Handles all of the animations 
        StateHandler(); // Handles all of the player states
    }

    private void MovementHandler()
    {
        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();

        if (canMove)
        {
            character.velocity = new Vector2(inputVector.x * playerSpeed, character.velocity.y);
            
            if (playerInputActions.Player.Movement.inProgress)
            {
                if (IsGrounded())
                {
                    isRunning = true;
                    animator.SetBool("isRunning", true);
                }
                
                direction = (int)inputVector.x;
            }
            else
            {
                isRunning = false;
                animator.SetBool("isRunning", false);
            }
        }
    }

    private void StateHandler()
    {
        //Movement Checker
        if (!isAttacking && wallJumpTimer <= 0)
            canMove = true;
        else
            canMove = false;
        
        //Jump Handler
        if (character.velocity.y > 0 && !IsGrounded() && !isWallGrabbing)
        {
            isJumping = true;
        }
        
        //Falling Handler
        if(character.velocity.y < 0 && !IsGrounded() && !isWallGrabbing)
        {
            isJumping = false;
            isFalling = true;
        }
        
        //Landing Handler
        if ((IsGrounded() || character.velocity.y == 0))
        {
            isJumping = false;
            isFalling = false;
            //Landing
        }
        
        //Attacks Handling
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0)
        {
            isAttacking = false;
        }

        if (isAttacking)
        {
            canMove = false;
            character.velocity = Vector3.zero;
        }
        
        //Wall Jumping Handling
        wallJumpTimer -= Time.deltaTime;

        if (wallJumpTimer <= 0)
        {
            isWallJumping = false;
        }

        if (IsNextToWall() && !IsGrounded())
        {
            if((direction == -1 && Input.GetKey(KeyCode.A)) || (direction == 1 && Input.GetKey(KeyCode.D)))
            {
                isWallGrabbing = true;
                character.gravityScale = 0f;
                character.velocity = new Vector3(0, -wallSlideSpeed, 0);
            }
        }
        else
        {
            isWallGrabbing = false;
            character.gravityScale = 1.0f;
        }

        if (isWallJumping)
        {
            character.gravityScale = 1.0f;
            character.velocity = new Vector2(-direction * wallJumpForce.x, wallJumpForce.y);
        }
        
    }

    private void SetDirection()
    {
        if (direction == 1)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (direction == -1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    
    private void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && canMove) 
        {
            if (IsGrounded())
            {
                character.velocity += new Vector2(0, jumpHeight);
            }
            else if (isWallGrabbing)
            {
                Debug.Log("Attempting walljump");
                wallJumpTimer = wallJumpTime;
                isStartingWallJump = true;
                isWallJumping = true;
            }
        } 
    }
    
    private void LightAttack(InputAction.CallbackContext context)
    {
        if (context.performed && !isAttacking)
        {
            isAttacking = true;
            isAttackingLight = true;
            isRunning = false;
            attackTimer = lightAttackCooldown;

            Collider2D[] hits = Physics2D.OverlapBoxAll(lightAttackBox.position, lightAttackBox.localScale, 0f, enemyLayerMask);

            foreach (Collider2D hit in hits)
            {
                if (!hit.isTrigger)
                {
                    EnemyStats enemyStats = hit.GetComponentInParent<EnemyStats>();

                    if (enemyStats != null)
                    {
                        enemyStats.TakeDamage(lightAttackDamage);
                    }
                }
            }

            if (hits.Length <= 0)
            {
                Debug.Log("No enemy in range, no hit detected");
            }
        }
    }

    private void HeavyAttack(InputAction.CallbackContext context)
    {
        if (context.performed && !isAttacking)
        {
            isAttacking = true;
            isAttackingHeavy = true;
            isRunning = false;
            attackTimer = heavyAttackCooldown;
            
            Collider2D[] hits = Physics2D.OverlapBoxAll(heavyAttackBox.position, heavyAttackBox.localScale, 0f, enemyLayerMask);

            foreach (Collider2D hit in hits)
            {
                if (!hit.isTrigger)
                {
                    EnemyStats enemyStats = hit.GetComponentInParent<EnemyStats>();

                    if (enemyStats != null)
                    {
                        Debug.Log("Enemy hit!" + hit.name);
                        enemyStats.TakeDamage(heavyAttackDamage);
                    }
                }
            }
            
            if (hits.Length <= 0)
            {
                Debug.Log("No enemy in range, no hit detected");
            }
        }
    }
    
    private void AnimationHandler()
    {
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isJumping", isJumping);
        animator.SetBool("isFalling", isFalling);
        animator.SetBool("isHanging", isWallGrabbing);

        if (isHurt)
        {
            animator.SetTrigger("Hit");
            isHurt = false;
        }

        if (isAttackingLight)
        {
            animator.SetTrigger("AttackingLight");
            isAttackingLight = false;
        }

        if (isAttackingHeavy)
        {
            animator.SetTrigger("AttackingHeavy");
            isAttackingHeavy = false;
        }

        if (isStartingWallJump)
        {
            animator.SetTrigger("WallJumping");
            isStartingWallJump = false;
        }
        
    }
    
    private void ResetAllAnimations()
    {
        animator.SetBool("isJumping", false);
        animator.SetBool("isRunning", false);
        animator.SetBool("isFalling", false);
    }
    
    private bool IsGrounded()
    {
        float extraHeightText = 0.05f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, extraHeightText, groundLayerMask);
        Color rayColor = (raycastHit.collider != null) ? Color.green : Color.red;

        Debug.DrawRay(boxCollider2D.bounds.center, Vector2.down * (boxCollider2D.bounds.extents.y + extraHeightText), rayColor);
        return raycastHit.collider != null;
    }

    private bool IsNextToWall()
    {
        float wallDetectionRange = 0.05f;
        Vector2 raycastDirection = (direction == 1) ? Vector2.right : Vector2.left;
        RaycastHit2D raycastHit = Physics2D.Raycast(boxCollider2D.bounds.center, raycastDirection, boxCollider2D.bounds.extents.x + wallDetectionRange, wallsLayerMask);
        Color rayColor = (raycastHit.collider != null) ? Color.green : Color.red;
        
        Debug.DrawRay(boxCollider2D.bounds.center, raycastDirection * (boxCollider2D.bounds.extents.x + wallDetectionRange), rayColor);
        return raycastHit.collider != null;
    }
    
    
}
