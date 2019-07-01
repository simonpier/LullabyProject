using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChandelierCandleDetection_ML : MonoBehaviour
{
    [SerializeField] GameObject playerCandle;

    bool isCandleColliding;

    public bool IsCandleColliding
    {
        get
        {
            return isCandleColliding;
        }
        protected set
        {
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player_CandleCollider" && playerCandle.activeSelf)
        {
            isCandleColliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player_CandleCollider" && !playerCandle.activeSelf)
        {
            isCandleColliding = false;
        }
    }
}
