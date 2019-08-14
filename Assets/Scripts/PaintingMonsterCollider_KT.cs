using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingMonsterCollider_KT : MonoBehaviour
{
    [SerializeField] PaintingMonsterBehaviour_ML painting;
    public void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Player"))
        {
            Debug.Log("abc");
            painting.EnemyReset();
            painting.isTriggered = false;
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
