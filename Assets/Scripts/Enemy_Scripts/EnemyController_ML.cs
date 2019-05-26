using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController_ML : MonoBehaviour
{
    //Enemy's transform range
    [SerializeField] protected float transformRange = 10f;
    //Enemy's attack range
    [SerializeField] protected float attackRange = 5f;
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

    [SerializeField] protected Collider2D AttackCollider;

    //Indicates if the enemy is facing right
    protected bool facingRight = true;
    protected bool isDied = false;

    protected Vector2 respawnPoint;
    protected Transform target;
    protected Animator anim;

    public virtual void Start()
    {
        respawnPoint = transform.localPosition;
        //TODO Should be replaced with an instance of the player for more optimization
        //Reference to the player
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
    }

    public virtual void Update()
    {
        DeathChecker();
        TargetTracking();
    }

    //Regulates player tracking
    public virtual void TargetTracking()
    {
        if (!isDied)
        {
            float distance = Vector2.Distance(target.position, transform.position);
            Flip();

            if (distance <= transformRange)
            {
                anim.SetTrigger("transformation");
            }

            if (canMove &&  distance > attackRange)
            {
                if (anim.GetBool("isTransformed") && !anim.GetBool("attack"))
                {
                    //If the enemy can fly allow it to move also in y axis
                    if (canFly)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                    }
                    else if (!canFly)
                    {
                        Vector2 targetPos = new Vector2(target.position.x, transform.position.y);
                        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
                    }
                }
            }
            else if (distance <= attackRange)
            {
                anim.SetBool("attack", true);
            }
        }
    }

    //TODO must be adjusted once the assets will be implemented
    //To be activated when enemy's hitpoints run out
    public virtual void DeathChecker()
    {
        if (canDie && hitPoint <= 0)
        {
            anim.SetBool("death", true);
            isDied = true;
        }
    }

    //TODO must be adjusted once the light collision will be ready
    //To be activated when the enemy gets hit by the light
    public virtual void TakeDamage()
    {
        hitPoint -= 1.0f * Time.deltaTime;
    }

    //Allow the enemy to flip his sprite basing on the position of the player
    private void Flip()
    {
        if (transform.position.x >= target.position.x && facingRight || transform.position.x < target.position.x && !facingRight)
        {
            facingRight = !facingRight;

            Vector3 enemyScale = transform.localScale;
            enemyScale.x *= -1;
            transform.localScale = enemyScale;
        }
    }

    //in this region there are the different methods used through the animation event system
    #region Animation Manager
    private void Transformation()
    {
        anim.SetBool("isTransformed", true);
    }

    private void AttackReset()
    {
        anim.SetBool("attack", false);
    }

    private void Attack()
    {
        AttackCollider.enabled = !AttackCollider.enabled;
    }
    #endregion

    //Allow to see the enemy's range in the scene
    public virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, transformRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
