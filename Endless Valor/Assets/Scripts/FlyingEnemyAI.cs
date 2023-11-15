using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyAI : MonoBehaviour
{
    //Serialized
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private float attackDistance = 5f;
    [SerializeField] private float attackDamage = 10.0f;
    [SerializeField] private float attackCooldown = 3.0f;
    [SerializeField] private Animator animator;

    //Privates
    private GameObject player;
    private EnemyStats enemyStats;
    private PlayerStats playerStats;
    private Vector2 startingPosition;

    //Flags
    private bool isChasing = false;

    //Timers
    private float attackTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyStats = GetComponent<EnemyStats>();
        playerStats = player.GetComponent<PlayerStats>();
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer -= Time.deltaTime;

        if (enemyStats.isDead)
        {
            return;
        }
        else if (isChasing)
        {
            Chase();
            Flip(player.transform.position.x);
        }
        else
        {
            ReturnToStartPoint();
            Flip(startingPosition.x);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isChasing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isChasing = false;
    }

    private void Chase()
    {
        if (Vector2.Distance(transform.position, player.transform.position) <= attackDistance)
        {
            if (attackTimer <= 0)
            {
                animator.SetTrigger("Attack1");
                //playerStats.TakeDamage(attackDamage);
                attackTimer = attackCooldown;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position + player.transform.forward * attackDistance, speed * Time.deltaTime);
        }
    }

    private void ReturnToStartPoint()
    {
        transform.position = Vector2.MoveTowards(transform.position, startingPosition, speed * Time.deltaTime);
    }

    private void Flip(float towards)
    {
        if (transform.position.x > towards)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (transform.position.x < towards)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    
}
