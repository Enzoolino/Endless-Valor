using UnityEngine;

public class Enemy : MonoBehaviour
{
    public D_Enemy enemyData;
    [field: SerializeField] public float MaxHealth { get; set; } = 100f;
    public float CurrentHealth { get; set; }
    public int FacingDirection { get; private set; }
    public GameObject EnemyVisual { get; set; }
    public Rigidbody2D rb { get; private set; }
    public Animator animator { get; private set; }
    public BoxCollider2D boxCollider2D { get; private set; }
    public EnemyStateMachine StateMachine { get; set; }


    private Vector2 _velocityHolder;
    
    public virtual void Start()
    {
        CurrentHealth = MaxHealth;
        
        rb = transform.GetComponent<Rigidbody2D>();
        boxCollider2D = transform.GetComponent<BoxCollider2D>();
        animator = transform.GetComponentInChildren<Animator>();
        EnemyVisual = GameObject.Find(gameObject.name + " - Visual");
        
        StateMachine = new EnemyStateMachine();
    }

    public virtual void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }

    public virtual void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    public virtual void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        Mathf.Clamp(CurrentHealth, 0, MaxHealth);
        

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        gameObject.layer = 9;
        EnemyVisual.layer = 9;
        Debug.Log("Current layer: " + gameObject.layer);
        
    }

    public virtual void SetVelocity(float velocity)
    {
        _velocityHolder.Set(FacingDirection * velocity * Time.deltaTime, rb.velocity.y);
        rb.velocity = _velocityHolder;
    }

    public virtual bool CheckWall()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(boxCollider2D.bounds.center, EnemyVisual.transform.right,
            boxCollider2D.bounds.extents.x + enemyData.wallCheckDistance, enemyData.wallsLayerMask);

        Color rayColor = (raycastHit.collider != null) ? Color.green : Color.red;
        Debug.DrawRay(boxCollider2D.bounds.center, EnemyVisual.transform.right * (boxCollider2D.bounds.extents.x + enemyData.wallCheckDistance), rayColor);

        return raycastHit.collider != null;
    }

    public virtual bool CheckLedge()
    {
        Vector2 raycastSide = (FacingDirection == 1) ? new Vector2(boxCollider2D.bounds.max.x, boxCollider2D.bounds.min.y) : boxCollider2D.bounds.min;
        RaycastHit2D raycastHit = Physics2D.Raycast(raycastSide, Vector2.down, enemyData.ledgeCheckDistance,
            enemyData.groundLayerMask);
        
        Color rayColor = (raycastHit.collider == null) ? Color.green : Color.red;
        Debug.DrawRay(raycastSide, Vector2.down * (enemyData.ledgeCheckDistance), rayColor);

        return raycastHit.collider == null;
    }
    
    public virtual void Flip()
    {
        FacingDirection *= -1;
        EnemyVisual.transform.Rotate(0f, 180f, 0f);
    }
}
