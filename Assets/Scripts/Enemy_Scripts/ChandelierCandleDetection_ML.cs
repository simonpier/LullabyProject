using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChandelierCandleDetection_ML : MonoBehaviour
{
    [SerializeField] GameObject playerLanter;
    [SerializeField] GameObject playerCandle;


    bool isCandleColliding;
    bool isLanternColliding;

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

    public bool IsLanternColliding
    {
        get
        {
            return isLanternColliding;
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
        if (collision.tag == "Player_LanternCollider")
        {
            isLanternColliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player_CandleCollider")
        {
            isCandleColliding = false;
        }
        if (collision.tag == "Player_LanternCollider")
        {
            isLanternColliding = false;
        }
    }
}
