using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorMonsterBehaviour_ML : EnemyController_ML
{
    [SerializeField] GameObject playerCandle;
    [SerializeField] GameObject playerLantern;


    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }


    public override void OnTriggerStay2D(Collider2D collision)
    {
        base.OnTriggerStay2D(collision);

        if ((collision.gameObject.tag == "Player") && Input.GetButtonDown("Interaction") && !playerCandle.activeSelf && !playerLantern.activeSelf && !isDied)
        {
            anim.SetBool("reset", false);
            anim.SetTrigger("transformation");
        }
    }

    public override void TargetTracking()
    {
        float distance = Vector2.Distance(target.position, transform.position);

        if (distance <= attackRange)
        {
            anim.SetBool("attack", true);
        }
        else if (!canMove && distance > attackRange)
        {
            anim.SetBool("idle", true);
        }
    }

}
