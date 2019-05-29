using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiderdog_Boss_Behaviour : EnemyController_ML
{

    [SerializeField] private Collider2D SlashCollider;

    private int i;

    public override void Start()
    {
        base.Start();   
    }

    public override void Update()
    {
        Debug.Log(i);
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
            i = (Random.Range (0,2));
            if (i == 0)
            {
                anim.SetBool("slash", false);
                anim.SetBool("buttAttack", true);
            }
            else if (i == 1)
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

