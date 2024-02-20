using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    //Serialized
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float deathDelay = 2f;
    [SerializeField] private bool isFlying = false;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject enemyVisual;

    //Privates
    private Rigidbody2D rb;
    private float currentHealth;

    //Flags
    public bool isDead = false;
    public bool isTakingDamage = false;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (isDead)
        {
            if (isFlying)
            {
                if (rb.velocity.y == 0)
                {
                    animator.SetBool("isDying", true);
                    Object.Destroy(gameObject, deathDelay);
                }
            }
            else
            {
                animator.SetBool("isDying", true);
                Object.Destroy(gameObject, deathDelay);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        isTakingDamage = true;
        Mathf.Clamp(currentHealth, 0, maxHealth);
        animator.SetTrigger("Hit");

        
        if (currentHealth <= 0)
        {
            isDead = true;
            gameObject.layer = 9;
            enemyVisual.layer = 9;
            
            Debug.Log("Current layer: " + gameObject.layer);

            if (isFlying)
            {
                animator.SetBool("isFalling", true);
                rb.gravityScale = 1;
                rb.mass = 0.1f;
            }
        }
    }
}
