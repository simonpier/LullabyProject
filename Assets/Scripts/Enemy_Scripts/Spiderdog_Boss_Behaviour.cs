using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Spiderdog_Boss_Behaviour : EnemyController_ML
{

    [SerializeField] private float landDistance = 3.5f;
    [SerializeField] private float walkTime = 10f;
    [SerializeField] private Collider2D SlashCollider;
    [SerializeField] GameObject bossRoomPosDX;
    [SerializeField] GameObject bossRoomPosSX;

    Vector3 originalBigness;
    private int attackRandomizer;
    private bool isTriggered;
    private bool isLighted;
    [SerializeField] private bool changeDirection;
    private float landingPos;


    public bool IsTriggered { get => isTriggered; set => isTriggered = value; }

    public override void Start()
    {
        base.Start();
        originalBigness = transform.localScale;
        landingPos = transform.position.y - landDistance ;
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Flip()
    {
        if (transform.position.x >= target.position.x && !facingRight || transform.position.x < target.position.x && facingRight)
        {
            facingRight = !facingRight;

            Vector3 enemyScale = transform.localScale;
            enemyScale.x *= -1;
            transform.localScale = enemyScale;
        }
    }

    public override void TargetTracking()
    {
        float distance = Vector2.Distance(target.position, transform.position);

        if(isTriggered)
        {         
            if(!isLighted)
            {
                if (changeDirection == false)
                {
                    transform.position = Vector2.MoveTowards(transform.position, bossRoomPosSX.transform.position, speed * Time.deltaTime);
                }
                else if (changeDirection == true)
                {
                    transform.position = Vector2.MoveTowards(transform.position, bossRoomPosDX.transform.position, speed * Time.deltaTime);
                }
            }

            if (isLighted)
            {
                anim.SetBool("inRange", true);
                if (target.position.y <= transform.position.y)
                {
                    NormalBehaviour(distance);
                    if (target.position.y <= transform.position.y && anim.GetBool("lookUp"))
                    {
                        anim.SetBool("lookUp", false);
                    }
                }
                else if (target.position.y > transform.position.y)
                {
                    if (!anim.GetBool("lookUp") && distance >= transformRange)
                    {
                        anim.SetBool("lookUp", true);
                    }
                }
            }
        }
    }

    private IEnumerator BossPatrol()
    {
        while (!isLighted)
        {
            transform.DOMoveX(bossRoomPosSX.transform.position.x, walkTime);
            yield return new WaitForSeconds(walkTime);
            transform.DOMoveX(bossRoomPosDX.transform.position.x, walkTime);
            yield return new WaitForSeconds(walkTime);
        }
    }

    private void NormalBehaviour(float distance)
    {
        if (distance > attackRange && anim.GetBool("inRange"))
        {
            anim.SetBool("attack", false);
            Flip();
            if (!anim.GetBool("buttAttack") && !anim.GetBool("slash"))
            {
                Vector2 targetPos = new Vector2(target.position.x, transform.position.y);
                transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            }
        }
        else if (distance <= attackRange)
        {
            anim.SetBool("attack", true);
            attackRandomizer = (Random.Range(0, 2));
            if (attackRandomizer == 0)
            {
                anim.SetBool("slash", false);
                anim.SetBool("buttAttack", true);
            }
            else if (attackRandomizer == 1)
            {
                anim.SetBool("buttAttack", false);
                anim.SetBool("slash", true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.tag == ("Player_CandleCollider") || collision.tag == ("Player_LanternCollider"))
        {
            isLighted = true;
        }
        else if (collision.tag == ("Edge"))
        {
            Debug.Log("aa");
            changeDirection = !changeDirection;
        }


    }

    public override void EnemyReset()
    {
        anim.SetBool("reset", true);
        hitPoint = maxHitPoint;
        transform.position = respawnPoint;
        transform.localScale = originalBigness;
        anim.SetBool("death", false);
        anim.SetBool("inRange", false);

    }

    public override void Respawn()
    {
        if (transform.position.x != respawnPoint.x && target.position.x > dxRoomLimiter.transform.position.x || target.position.y > dxRoomLimiter.transform.position.y || target.position.x < sxRoomLimiter.transform.position.x || target.position.y < sxRoomLimiter.transform.position.y)
        {
            EnemyReset();
        }
    }

    public override void OnTriggerStay2D(Collider2D collision)
    {
        if (hitPoint <= 0)
        {
            isTakingDamage = false;
        }
        else if (collision.tag == ("Player_LanternCollider") && hitPoint > 0)
        {
            isTakingDamage = true;
            TakeDamage();
        }

        //if (collision.tag == ("Edge"))
       // {
          //  Debug.Log("aa");
         //   changeDirection = !changeDirection;
       // }
    }

    public override void DeathChecker()
    {
        if (canDie && hitPoint <= 0)
        {
            anim.SetBool("death", true);
            isDied = true;
            transform.DOScale(1f, 2f);
            transform.DOMoveY(landingPos, 2f);
        }
    }


    #region Animation Manager

    private void ButtAttackReset()
    {
        anim.SetBool("buttAttack", false);
    }

    private void SlashAttackReset()
    {
        anim.SetBool("slash", false);
    }

    private void ButtAttack()
    {
        AttackCollider.enabled = !AttackCollider.enabled;
    }

    private void SlashAttack()
    {
        SlashCollider.enabled = !SlashCollider.enabled;
    }

    private void ResetDeathReset()
    {
        anim.SetBool("reset", false);
    }

    #endregion
}

