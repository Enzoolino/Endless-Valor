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
    public void ChaseTheTarget(Vector3 target, float chaseSpeed)
    {
        /*transform.position = Vector2.MoveTowards(transform.position,
            target, chaseSpeed * Time.deltaTime);*/
        
        // Calculate the direction towards the target
        Vector3 moveDirection = (target - transform.position).normalized;

        // Move the enemy in the target direction without any positional adjustments
        transform.position += moveDirection * chaseSpeed * Time.deltaTime;
    }

    public Vector3 CalculateCloserAttackPoint(Transform attackPoint1, Transform attackPoint2)
    {
        Vector3 target = transform.position;
        
        float distance1 = Mathf.Abs(Vector3.Distance(target, attackPoint1.position));
        float distance2 = Mathf.Abs(Vector3.Distance(target, attackPoint2.position));

        //Debug.Log("Distance1: " + distance1);
        //Debug.Log("Distance2 " + distance2);
        
        if (distance1 < distance2)
            return attackPoint1.position;
        else
            return attackPoint2.position;

    }
    
    
    public void ReturnToInitialPosition(Vector3 initialPosition, float movementSpeed)
    {
        transform.position =
            Vector2.MoveTowards(transform.position, initialPosition, movementSpeed * Time.deltaTime);
    }
    
}
