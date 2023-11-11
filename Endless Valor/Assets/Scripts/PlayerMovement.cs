using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float playerSpeed = 3.0f;
    [SerializeField] private float jumpHeight = 6.0f;
    
    [Header("Attacking")]
    [SerializeField] private float lightAttackCooldown = 1.0f;
    [SerializeField] private float heavyAttackCooldown = 1.5f;
    [SerializeField] private float lightAttackDamage = 25.0f;
    [SerializeField] private float heavyAttackDamage = 50.0f;
    [SerializeField] private Transform lightAttackBox;
    [SerializeField] private Transform heavyAttackBox;
    
    [Header("Wall Jumping")]
    [SerializeField] private float wallJumpTime = 0.2f;
    [SerializeField] private float slideFromWallForce = 0.5f;

    [Header("References")]
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private LayerMask wallsLayerMask;
    [SerializeField] private LayerMask enemyLayerMask;
    [SerializeField] private Animator animator;

    //Privates
    private Rigidbody2D character;
    private PlayerInputActions playerInputActions;
    //private PolygonCollider2D polygonCollider2D;
    private BoxCollider2D boxCollider2D;

    //Flags
    private bool isWalking;
    private bool isJumping;
    private bool isFalling;
    private bool canMove = true;
    private bool isAttacking;
    private int direction = 1;
    private bool isWallSticking;

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
        //polygonCollider2D = GetComponent<PolygonCollider2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        playerInputActions = new PlayerInputActions();
    }

    private void FixedUpdate()
    {
        //Simple Movement Handling
        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();

        if (canMove)
        {
            character.velocity = new Vector2(inputVector.x * playerSpeed, character.velocity.y);

            if (playerInputActions.Player.Movement.inProgress)
            {
                if (IsGrounded())
                {
                    isWalking = true;
                    animator.SetBool("isRunning", true);
                }

                if (Input.GetKey(KeyCode.A))
                {
                    direction = -1;
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    direction = 1;
                    transform.localScale = new Vector3(1, 1, 1);
                }
            }
            else
            {
                isWalking = false;
                animator.SetBool("isRunning", false);
            }
        }

        //Jump & Fall Animations Handling
        if (character.velocity.y > 0 && !IsGrounded() && !isWallSticking)
        {
            isJumping = true;
            animator.SetBool("isJumping", true);
        }
        
        if(character.velocity.y < 0 && !IsGrounded() && !isWallSticking)
        {
            isJumping = false;
            isFalling = true;
            animator.SetBool("isFalling", true);
        }
        
        if ((IsGrounded() || character.velocity.y == 0))
        {
            isJumping = false;
            isFalling = false;
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
        }


        //Attacks Handling
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0)
        {
            isAttacking = false;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("AttackLight") || animator.GetCurrentAnimatorStateInfo(0).IsName("AttackHeavy"))
        {
            canMove = false;
            character.velocity = Vector3.zero;
        }
        else
        {
            canMove = true;
        }

        //Wall Jumping Handling
        wallJumpTimer -= Time.deltaTime;

        if (wallJumpTimer <= 0 && !isAttacking)
        {
            canMove = true;
        }

        if (IsNextToWall() && !IsGrounded())
        {
            if((direction == -1 && Input.GetKey(KeyCode.A)) || (direction == 1 && Input.GetKey(KeyCode.D)))
            {
                isWallSticking = true;
            }
        }
        

        if (isWallSticking)
        {
            animator.SetBool("isHanging", true);
            character.gravityScale = 0f;
            character.velocity = new Vector3(0, -slideFromWallForce, 0);
        }
        else
        {
            animator.SetBool("isHanging", false);
            character.gravityScale = 1.0f;
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
            else if (isWallSticking && wallJumpTimer <= 0)
            {
                wallJumpTimer = wallJumpTime;
                canMove = false;
                character.velocity = new Vector2(-transform.localScale.x * playerSpeed, jumpHeight);
                character.gravityScale = 1.0f;
                isWallSticking = false;
            }
        } 
    }

    private void LightAttack(InputAction.CallbackContext context)
    {
        if (context.performed && !isAttacking)
        {
            isAttacking = true;
            attackTimer = lightAttackCooldown;
            animator.SetBool("isRunning", false);
            animator.SetTrigger("AttackingLight");

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
            attackTimer = heavyAttackCooldown;
            animator.SetBool("isRunning", false);
            animator.SetTrigger("AttackingHeavy");

            float animationLength = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
            float startTime = animationLength / 2f;

            StartCoroutine(DealHeavyDamageDelayed(startTime));

        }
    }

    //Function exists to time the heavy attack with monster hit animation
    private IEnumerator DealHeavyDamageDelayed(float startTime)
    {
        yield return new WaitForSeconds(startTime);

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
    
    private void ResetAllAnimations()
    {
        animator.SetBool("isJumping", false);
        animator.SetBool("isRunning", false);
        animator.SetBool("isFalling", false);
    }
}
