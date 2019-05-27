using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMonsterBehaviour_ML : EnemyController_ML
{
    // Start is called before the first frame update
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

        if ((collision.gameObject.tag == "Player") && Input.GetKeyDown(KeyCode.E))
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
