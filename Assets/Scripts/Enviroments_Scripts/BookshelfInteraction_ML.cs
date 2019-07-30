using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BookshelfInteraction_ML : MonoBehaviour
{
    [SerializeField] GameObject halo;
    [SerializeField] GameObject smoke;
    [SerializeField] private float fadingTime;
    [SerializeField] AudioManager audio;

    SpriteRenderer renderer;
    Collider2D collider;

    private bool check;

    private void Start()
    {
        renderer = smoke.GetComponent<SpriteRenderer>();
        collider = smoke.GetComponent<Collider2D>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.Return) && collision.tag == "Player" && !check)
        {
            audio.PlaySound("bookshelf interaction");
            Invoke("SlidingWall", .1f);
            check = true;
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
        renderer.DOFade(0f, fadingTime);
        collider.enabled = false;
        audio.PlaySound("wall moving");
    }

}
