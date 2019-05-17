using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController_ML : MonoBehaviour
{
    //Enemy's range
    [SerializeField, Header("Enemy Look Range")] protected float lookRadius = 10f;
    //Enemy's attack range
    [SerializeField, Header("Enemy Attack Range")] protected float attackRange = 5f;
    //Enemy's movement speed
    [SerializeField] protected float speed = 5.0f;
    //Enemy's health point
    [SerializeField] protected float hitPoint = 5.0f;
    //Indicates if the enemy can fly
    [SerializeField] protected bool canFly = false;
    //Indicates if the enemy can die
    [SerializeField] protected bool canDie = true;
    //Indicates if the enemy can move
    [SerializeField] protected bool canMove = true;

    //Indicates if the enemy is facing right
    protected bool facingRight = true;

    protected Vector2 respawnPoint;
    protected Transform target;



    public virtual void Start()
    {
        respawnPoint = transform.localPosition;
        //TODO Should be replaced with an instance of the player for more optimization
        //Reference to the player
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }

    public virtual void Update()
    {
        TargetTracking();
    }

    //Regulates player tracking
    public virtual void TargetTracking()
    {
        float distance = Vector2.Distance(target.position, transform.position);
        Flip();

        if (canMove && distance <= lookRadius && distance > attackRange)
        {
            //If the enemy can fly allow it to move also in y axis
            if (canFly)
            {
                
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
            else
            {
                Vector2 targetPos = new Vector2(target.position.x, transform.position.y);
                transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            }
        }
        else if (distance <= attackRange)
        {
            Debug.Log("in attack range");
        }
    }

    //TODO must be adjusted once the assets will be implemented
    //To be activated when enemy's hitpoints run out
    public virtual void DeathChecker()
    {
        if (canDie && hitPoint <= 0)
        {
            Debug.Log("enemy is died");
        }
    }

    //TODO must be adjusted once the light collision will be ready
    //To be activated when the enemy gets hit by the light
    public virtual void TakeDamage()
    {
        hitPoint -= 1.0f * Time.deltaTime;
    }

    //Allow the enemy to flip his sprite basing on the position of the player
    public virtual void Flip()
    {
        if (transform.position.x >= target.position.x && facingRight || transform.position.x < target.position.x && !facingRight)
        {
            facingRight = !facingRight;

            Vector3 enemyScale = transform.localScale;
            enemyScale.x *= -1;
            transform.localScale = enemyScale;
        }
    }

    //Allow to see the enemy's range in the scene
    public virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);

    }
}
