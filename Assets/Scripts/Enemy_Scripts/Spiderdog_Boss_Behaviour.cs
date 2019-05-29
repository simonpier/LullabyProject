﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiderdog_Boss_Behaviour : EnemyController_ML
{

    [SerializeField] private float landDistance = 3.5f;
    [SerializeField] private Collider2D SlashCollider;


    private int attackRandomizer;
    float landingPos;


    public override void Start()
    {
        base.Start();
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
        if (distance <= transformRange)
        {
            anim.SetBool("inRange", true);
        }

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
            Debug.Log("inrange");
            attackRandomizer = (Random.Range (0,2));
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

    public override void Respawn()
    {
        if (transform.position.x != respawnPoint.x && target.position.x > dxRoomLimiter.transform.position.x || target.position.y > dxRoomLimiter.transform.position.y || target.position.x < sxRoomLimiter.transform.position.x || target.position.y < sxRoomLimiter.transform.position.y)
        {
            transform.position = respawnPoint;
            anim.SetBool("inRange", false);
        }
    }

    public override void OnTriggerStay2D(Collider2D collision)
    {
        if (hitPoint <= 0)
        {
            isTakingDamage = false;
        }
        else if (collision.tag == ("Player_CandleCollider") && hitPoint > 0)
        {
            isTakingDamage = true;
            TakeDamage();
        }
    }

    public override void DeathChecker()
    {
        if (canDie && hitPoint <= 0)
        {
            anim.SetBool("death", true);
            isDied = true;
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1f, 1f, 1f), 1f * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, landingPos, 1f * Time.deltaTime), transform.position.z);
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

    #endregion
}

