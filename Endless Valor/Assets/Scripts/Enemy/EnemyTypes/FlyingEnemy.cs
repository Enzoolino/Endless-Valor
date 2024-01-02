using UnityEngine;

public class FlyingEnemy : Enemy
{
    [HideInInspector] public bool isFalling; //Is responsible for transferring to fall state before dead state
    
    [HideInInspector] public bool isPlayerInDetectionRadius;

    public override void TakeDamage(AttackDetails attackDetails)
    {
        base.TakeDamage(attackDetails);

        if (currentHealth <= 0)
        {
            isFalling = true;
        }
    }

    public virtual void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("FlyingEnemy: Player in detection radius !");
            isPlayerInDetectionRadius = true;
        }
        else
        {
            //Debug.Log("FlyingEnemy: Something entered detection radius but it's not a player!"); -- Detection debugging
        }
    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("FlyingEnemy: Player leaves detection radius !");
            isPlayerInDetectionRadius = false;
        }
        else
        {
            //Debug.Log("FlyingEnemy: Something exited detection radius but it's not a player!"); -- Detection debugging
        }
        
    }
    
    public override bool CheckPlayerInCloseAggroRange()
    {
        //Debug.Log("Checking the flying enemy radius"); -- Check if function triggers
        return isPlayerInDetectionRadius;
    }


    //Moving Handlers
    public void ChaseTheTarget(Transform target, float chaseSpeed)
    {
        transform.position = Vector2.MoveTowards(transform.position,
            target.position, chaseSpeed * Time.deltaTime);
    }

    public void ReturnToInitialPosition(Vector3 initialPosition, float movementSpeed)
    {
        transform.position =
            Vector2.MoveTowards(transform.position, initialPosition, movementSpeed * Time.deltaTime);
    }
    
}
