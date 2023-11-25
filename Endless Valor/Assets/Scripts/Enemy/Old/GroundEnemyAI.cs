using System;
using CustomBoolInspector;
using UnityEngine;

public class GroundEnemyAI : MonoBehaviour
{
    [Header("Movement & Attack")]
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private float attackDistance = 1.4f;
    [SerializeField] private float attackDamage = 10.0f;
    [SerializeField] private float attackCooldown = 3.0f;
    
    [Header("DetectionSystem")]
    [SerializeField] private float playerDetectionRange = 5.0f;
    public float timeToDetectPlayer = 2.0f;
    public float timeForSearch = 6.0f;
    public float timeOfSurprise = 1.5f;
    
    [SerializeField] private float timeToFlipAfterHit = 0.7f;
    
    [Header("Initializing")]
    [SerializeField] private Animator animator;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private LayerMask wallsLayerMask;
    [SerializeField] private LayerMask playerLayerMask;

    /*
    [SerializeField] private bool hasShield = false;
    [ShowIf("hasShield", true)][SerializeField] private float shieldCooldown = 10.0f;

    [SerializeField] private bool isNimble = false;
    [ShowIf("isNimble", true)][SerializeField] private float leapCooldown = 5.0f;*/

    
    //Privates
    private GameObject player;
    private PlayerStats playerStats;
    private EnemyStats enemyStats;
    private BoxCollider2D boxCollider2D;
    
    //Flags
    private int direction = 1;  //1 - Right -1 - Left
    private bool wasDetectionEmoteShown;
    private bool wasLostTargetEmoteShown = true;
    private bool isPlayerDetected = false;
    [HideInInspector] public bool isPlayerBeingSearched = false;
    private bool isSurprised = false;
    
    //Timers
    private float attackTimer;
    [HideInInspector] public float detectPlayerTimer;
    private float searchTimer;
    private float surpriseTimer;
    private float flipAfterHitTimer;
    
    //Events
    public event EventHandler OnPlayerDetected; //Shows Detection emote
    public event EventHandler OnPlayerLost; //Shows LostTarget emote
    

    private void Start()
    {
        //Initialize components
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = player.GetComponent<PlayerStats>();
        enemyStats = GetComponent<EnemyStats>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        
        //Set timers
        flipAfterHitTimer = timeToFlipAfterHit;
        //surpriseTimer = timeOfSurprise;
    }

    private void Update()
    {
        attackTimer -= Time.deltaTime;
        
        if (enemyStats.isDead)
        {
            return;
        }
        
        DetectionSystem();
    }

    private void DetectionSystem()
    {
        if (!IsPlayerInRange())
        {
            speed = 2.0f;
            
            wasDetectionEmoteShown = false;
            if (!wasLostTargetEmoteShown && isPlayerDetected)
            {
                wasLostTargetEmoteShown = true;
                OnPlayerLost?.Invoke(this, EventArgs.Empty);

                isPlayerDetected = false;
                isPlayerBeingSearched = true;
                surpriseTimer = timeOfSurprise;
                isSurprised = true;
            }
            else if (detectPlayerTimer <= 0)
            {
                detectPlayerTimer = timeToDetectPlayer;
            }
            

            if (isPlayerBeingSearched)
            {
                searchTimer -= Time.deltaTime;

                if (searchTimer <= 0)
                    isPlayerBeingSearched = false;
            }
            else
            {
                searchTimer = timeForSearch;
            }
            
            
            if (enemyStats.isTakingDamage)
            {
                flipAfterHitTimer -= Time.deltaTime;

                if (flipAfterHitTimer <= 0)
                {
                    Flip();
                    enemyStats.isTakingDamage = false;
                    flipAfterHitTimer = timeToFlipAfterHit;
                    isPlayerDetected = true;
                }
            }

            surpriseTimer -= Time.deltaTime;
            
            if (surpriseTimer <= 0)
            {
                isSurprised = false;
                
                if (!IsNextToWall() && !IsOnTheEdgeOfPlatform())
                {
                    animator.SetBool("isWalking", true);
                    transform.position += new Vector3(direction * speed * Time.deltaTime, 0, 0);
                }
                else
                {
                    Flip();
                }
            }
            else
            {
                animator.SetBool("isWalking", false);
            }

        }
        else
        {
            speed = 2.5f;
            
            wasLostTargetEmoteShown = false;
            if (!wasDetectionEmoteShown)
            {
                wasDetectionEmoteShown = true;
                OnPlayerDetected?.Invoke(this, EventArgs.Empty);
            }
            
            detectPlayerTimer -= Time.deltaTime;

            if (isPlayerBeingSearched)
                isPlayerDetected = true;
            else if (detectPlayerTimer <= 0)
                isPlayerDetected = true;
            
            
            //Action Elements
            if (!IsOnTheEdgeOfPlatform() && isPlayerDetected)
            {
                animator.SetBool("isWalking", true);
                Chase();
            }
            else
            {
                animator.SetBool("isWalking", false);
            }
        }
    }


    private void Chase()
    {
        if (Vector2.Distance(transform.position, player.transform.position) <= attackDistance)
        {
            if (attackTimer <= 0)
            {
                //TODO: Attack Animation
                //playerStats.TakeDamage(attackDamage);
                attackTimer = attackCooldown;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position,
                player.transform.position + player.transform.forward * attackDistance, speed * Time.deltaTime);
        }
    }

    public bool IsPlayerInRange()
    {
        Vector2 raycastDirection = (direction == 1) ? Vector2.right : Vector2.left;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 
           0f, raycastDirection,boxCollider2D.bounds.extents.x + playerDetectionRange, playerLayerMask);
        Color rayColor = (raycastHit.collider == null) ? Color.yellow : Color.red;

        Debug.DrawRay(boxCollider2D.bounds.center,
            raycastDirection * (boxCollider2D.bounds.extents.x + playerDetectionRange), rayColor);

        return raycastHit.collider != null;
    }
    
    private bool IsOnTheEdgeOfPlatform()
    {
        float groundDetectionRange = 0.1f;
        Vector2 raycastSide = (direction == 1) ? new Vector2(boxCollider2D.bounds.max.x, boxCollider2D.bounds.min.y) : boxCollider2D.bounds.min;
        RaycastHit2D raycastHit = Physics2D.Raycast(raycastSide, Vector2.down, groundDetectionRange, groundLayerMask);
        Color rayColor = (raycastHit.collider == null) ? Color.green : Color.red;
        
        Debug.DrawRay(raycastSide, Vector2.down * (groundDetectionRange), rayColor);
        return raycastHit.collider == null;
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
    
    private void Flip()
    {
        attackTimer = attackCooldown; //Mainly exists to not attack player right after flip
        
        if (direction == 1)
        {
            direction = -1;
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (direction == -1)
        {
            direction = 1;
            transform.localScale = new Vector3(1f, 1f, 1f) ;
        }
    }
}
