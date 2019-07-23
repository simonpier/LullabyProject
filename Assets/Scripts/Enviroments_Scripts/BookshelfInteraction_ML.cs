using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BookshelfInteraction_ML : MonoBehaviour
{
    [SerializeField] GameObject halo;
    [SerializeField] GameObject slidingWall;
    [SerializeField] private float moveTime;
    [SerializeField] private float endingDistance;
    [SerializeField] AudioManager audio;

    private float endingPosition;

    private void Start()
    {
        endingPosition = slidingWall.transform.position.y - endingDistance;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.Return) && collision.tag == "Player")
        {
            audio.PlaySound("bookshelf interaction");
            Invoke("SlidingWall", 1f);
        }

        if (collision.tag == "Player_CandleCollider" || collision.tag == "Player_LanternCollider")
        {
            halo.SetActive(true);
        }          
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player_CandleCollider" || collision.tag == "Player_LanternCollider")
        {
            halo.SetActive(false);
        }
    }

    private void SlidingWall()
    {

        slidingWall.transform.DOMoveY(endingPosition, moveTime);
        audio.PlaySound("wall moving");

    }

}
