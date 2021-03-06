﻿using System.Collections;
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
    [SerializeField] protected float yDistance;
    //Indicates if the enemy can fly
    [SerializeField] protected bool canFly = false;
    //Indicates if the enemy can die
    [SerializeField] protected bool canDie = true;
    //Indicates if the enemy can move
    [SerializeField] protected bool canMove = true;
    //Indicates if the enemy is facing right

    [SerializeField] protected bool facingRight = true;

    [SerializeField] protected Collider2D attackCollider;
    [SerializeField, Header("Lower Left Room Limiter")] protected GameObject sxRoomLimiter;
    [SerializeField, Header("Top Right Room Limiter")] protected GameObject dxRoomLimiter;


    [SerializeField] protected bool isDied = false;
    protected bool isTakingDamage = false;

    protected Vector2 respawnPoint;
    protected Transform target;
    protected Animator anim;
    protected SpriteRenderer spriteRenderer;

    protected float maxHitPoint;

    public virtual void Start()
    {
        respawnPoint = transform.position;
        //TODO Should be replaced with an instance of the player for more optimization
        //Reference to the player
        target = PlayerStats_ML.instance.player.transform;
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        maxHitPoint = hitPoint;
    }

    public virtual void Update()
    {
        DeathChecker();
        TargetTracking();
        Respawn();
        
    }

    //Regulates player tracking
    public virtual void TargetTracking()
    {
        if (!isDied)
        {
            float distance = Vector2.Distance(target.position, transform.position);

            if (distance <= transformRange)
            {
                anim.SetBool("reset", false);
                anim.SetTrigger("transformation");
            }

            if (anim.GetBool("isTransformed"))
            {
                if (canMove && distance > attackRange)
                {
                    Flip();
                    if (!anim.GetBool("attack"))
                    {
                        //If the enemy can fly allow it to move also in y axis
                        if (canFly)
                        {
                            Vector2 targetPos = new Vector2(target.position.x, target.position.y + yDistance);
                            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
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
                    anim.SetBool("idle", false);
                    anim.SetBool("attack", true);
                }
                else if (!canMove && distance > attackRange)
                {
                    anim.SetBool("idle", true);
                }
            }
        }
    }

    //TODO must be adjusted once the assets will be implemented
    //To be activated when enemy's hitpoints run out
    public virtual void DeathChecker()
    {
        if (canDie && hitPoint <= 0 && !canFly)
        {
            anim.SetBool("death", true);
            isDied = true;
        }
    }

    public virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (hitPoint<=0)
        {
            isTakingDamage = false;
        }
        else if (collision.tag == ("Player_LanternCollider") && hitPoint > 0 && anim.GetBool("isTransformed"))
        {
            isTakingDamage = true;
            TakeDamage();
        }
    }

    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == ("Player_LanternCollider"))
        {
            isTakingDamage = false;
        }
    }

    //To be activated when the enemy gets hit by the light
    public virtual void TakeDamage()
    {
        hitPoint -= 1.0f * Time.deltaTime;
        StartCoroutine(TakingDamage());
    }

    private IEnumerator TakingDamage()
    {
        while (isTakingDamage)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(.1f);
        }
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

    public virtual void Respawn()
    {
        if (!target) Start();
        if (transform.position.x != respawnPoint.x && target.position.x > dxRoomLimiter.transform.position.x || target.position.y > dxRoomLimiter.transform.position.y || target.position.x < sxRoomLimiter.transform.position.x || target.position.y < sxRoomLimiter.transform.position.y)
        {
            EnemyReset();
        }
    }

    public virtual void EnemyReset()
    {
        isDied = false;
        transform.position = respawnPoint;
        anim.SetBool("reset", true);
        anim.SetBool("death", false);
        anim.ResetTrigger("transformation");
        anim.SetBool("isTransformed", false);
        hitPoint = maxHitPoint;
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
        attackCollider.enabled = !attackCollider.enabled;
    }

    private void ResetDeathReset()
    {
        anim.SetBool("reset", false);
    }

    private void ResetTransformation()
    {
        anim.SetBool("isTransformed", false);
        attackCollider.enabled = false;
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
