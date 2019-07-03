using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChandelierMonsterBehaviour_ML : EnemyController_ML
{
    [SerializeField] GameObject playerCandle;
    [SerializeField] GameObject candleColliderDetector;

    ChandelierCandleDetection_ML candleDetection;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        candleDetection = candleColliderDetector.GetComponent<ChandelierCandleDetection_ML>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        CandleCheck();
    }

    private void CandleCheck()
    {
        if (!playerCandle.activeSelf)
        {
            anim.SetBool("isTransformed", false);
            anim.ResetTrigger("transformation");
            anim.SetBool("attack", false);
            anim.SetBool("reset", true);
            anim.SetBool("death", true);
        }

        if (candleDetection.IsCandleColliding && hitPoint >= 0)
        {
            anim.SetBool("reset", false);
            anim.SetBool("death", false);
            anim.SetTrigger("transformation");
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player_CandleCollider")
        {
            anim.SetBool("reset", false);
            anim.SetBool("death", false);
            anim.SetTrigger("transformation");
        }
    }


}
