using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public D_Enemy enemyData;
    public int FacingDirection { get; private set; } = 1;
    public int LastDamageDirection { get; private set; }
    public GameObject EnemyVisual { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    public Animator Anim { get; private set; }
    public BoxCollider2D boxCollider2D { get; private set; }
    public EnemyStateMachine StateMachine { get; private set; }
    public EnemyAnimationToStateMachine Eatsm { get; private set; }


    private Vector2 velocityHolder;
    
    private float currentHealth;
    private float currentStunResistance;
    private float lastDamageTime;

    protected bool isStunned;
    [HideInInspector] public bool isHurt; //Trigger hurt state
    [HideInInspector] public bool isDead; //Trigger dead state
    
    public virtual void Start()
    {
        currentHealth = enemyData.maxHealth;
        currentStunResistance = enemyData.stunResistance;
        FacingDirection = 1;
        
        EnemyVisual = GameObject.Find(gameObject.name + " - Visual");
        Rb = transform.GetComponent<Rigidbody2D>();
        boxCollider2D = transform.GetComponent<BoxCollider2D>();
        Anim = EnemyVisual.transform.GetComponent<Animator>();
        Eatsm = EnemyVisual.transform.GetComponent<EnemyAnimationToStateMachine>();
        
        StateMachine = new EnemyStateMachine();
    }

    public virtual void Update()
    {
        StateMachine.CurrentState.LogicUpdate();

        if (Time.time >= lastDamageTime + enemyData.stunRecoveryTime)
        {
            ResetStunResistance();
        }
    }

    public virtual void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    public virtual void OnDestroy()
    {
        StateMachine.CurrentState.OnDestroy();
    }

    public virtual void SetVelocity(float velocity)
    {
        velocityHolder.Set(FacingDirection * velocity, Rb.velocity.y);
        Rb.velocity = velocityHolder;
    }

    public virtual void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        velocityHolder.Set(angle.x * velocity * direction, angle.y * velocity);
        Rb.velocity = velocityHolder;
    }

    public virtual bool CheckWall()
    {
        return RaycastingHandler(boxCollider2D.bounds.center, EnemyVisual.transform.right,
            boxCollider2D.bounds.extents.x + enemyData.wallCheckDistance, enemyData.wallsLayerMask, Color.green,
            Color.red, false, false);
    }

    public virtual bool CheckGround()
    {
        float inGroundDistance = 0.05f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f,
            Vector2.down, inGroundDistance, enemyData.groundLayerMask);
        
        Color rayColor = (raycastHit.collider != null) ? Color.green : Color.red;

        Debug.DrawRay(boxCollider2D.bounds.center, Vector2.down * (boxCollider2D.bounds.extents.y + inGroundDistance), rayColor);
        return raycastHit.collider != null;
    }

    public virtual bool CheckLedge()
    {
        Vector2 raycastOrigin = (FacingDirection == 1)
            ? new Vector2(boxCollider2D.bounds.max.x + enemyData.ledgeCheckDistance, boxCollider2D.bounds.min.y)
            : new Vector2(boxCollider2D.bounds.min.x - enemyData.ledgeCheckDistance, boxCollider2D.bounds.min.y);
        
        return RaycastingHandler(raycastOrigin, Vector2.down, enemyData.ledgeCheckDepth, enemyData.groundLayerMask,
            Color.green, Color.red, true, true);
    }

    public virtual bool CheckPlayerInCloseAggroRange()
    {
        return RaycastingHandler(boxCollider2D.bounds.center, EnemyVisual.transform.right,
            boxCollider2D.bounds.extents.x + enemyData.closeAggroDistance, enemyData.playerLayerMask, Color.green,
            Color.red, false, true);
    }

    public virtual bool CheckPlayerInFarAggroRange()
    {
        return RaycastingHandler(boxCollider2D.bounds.center, EnemyVisual.transform.right,
            boxCollider2D.bounds.extents.x + enemyData.farAggroDistance, enemyData.playerLayerMask, Color.green,
            Color.red, false, true);
    }

    public virtual bool CheckPlayerInCloseRangeAction()
    {
        return RaycastingHandler(boxCollider2D.bounds.center, EnemyVisual.transform.right,
            boxCollider2D.bounds.extents.x + enemyData.closeRangeActionDistance, enemyData.playerLayerMask, Color.cyan,
            Color.red, false, true);
    }
    
    public virtual void ResetStunResistance()
    {
        isStunned = false;
        currentStunResistance = enemyData.stunResistance;
    }
    
    public virtual void TakeDamage(AttackDetails attackDetails)
    {
        lastDamageTime = Time.time;
        currentHealth -= attackDetails.damageAmount;
        Mathf.Clamp(currentHealth, 0, enemyData.maxHealth);
        currentStunResistance -= attackDetails.stunDamageAmount;

        if (attackDetails.position.x > EnemyVisual.transform.position.x)
        {
            LastDamageDirection = -1;
        }
        else
        {
            LastDamageDirection = 1;
        }
        
        if (currentHealth > 0)
        {
            Debug.Log("Enemy is hurt !");
            isHurt = true;
        }
        else if (currentHealth <= 0)
        {
            Debug.Log("Enemy is dead !");
            isDead = true;
        }
        
        if (currentStunResistance <= 0)
        {
            isStunned = true;
        }
    }

    public void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger(); // Must be public for eatsm

    public void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger(); // Must be public for eatsm
    
    
    public virtual void Flip()
    {
        FacingDirection *= -1;
        EnemyVisual.transform.Rotate(0f, 180f, 0f);
        
    }

    private bool RaycastingHandler(Vector2 origin, Vector2 direction, float range, int layerMaskToDetect, Color positiveDetectionRayColor, Color negativeDetectionRayColor, bool isReturnConditionEqualNull, bool raycastVisible)
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(origin, direction, range, layerMaskToDetect);
        
        bool compareOption = (isReturnConditionEqualNull) ? (raycastHit.collider == null) : (raycastHit.collider != null);
        
        
        if (raycastVisible)
        {
            Color rayColor = (compareOption) ? positiveDetectionRayColor : negativeDetectionRayColor;
            Debug.DrawRay(origin, direction * range, rayColor);
        }
        return compareOption;
    }

    public void DestroyEnemyObject(float delay)
    {
        Destroy(gameObject, delay);
    }
}
