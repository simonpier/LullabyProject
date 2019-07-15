using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampMonsterBehaviour_ML : EnemyController_ML
{
    [SerializeField] GameObject playerCandle;
    [SerializeField] GameObject playerLantern;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        CandleCheck();
    }

    private void CandleCheck()
    {
        if (!playerCandle.activeSelf && !playerLantern.activeSelf)
        {
            anim.SetBool("isTransformed", false);
            anim.ResetTrigger("transformation");
            anim.SetBool("attack", false);
            anim.SetBool("reset", true);
            anim.SetBool("death", true);
        }
    }

    public override void TargetTracking()
    {
        if (!isDied)
        {
            float distance = Vector2.Distance(target.position, transform.position);

            if(anim.GetBool("isTransformed"))
            {
                if (canMove && distance > attackRange)
                {
                    Flip();
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
                else if (!canMove && distance > attackRange)
                {
                    anim.SetBool("idle", true);
                }
            }
        }
    }

    public override void OnTriggerStay2D(Collider2D collision)
    {
    }

    public override void OnTriggerExit2D(Collider2D collision)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player_CandleCollider" || collision.tag ==  "Player_LanternCollider")
        {
            anim.SetBool("reset", false);
            anim.SetBool("death", false);
            anim.SetTrigger("transformation");
        }
    }


}
