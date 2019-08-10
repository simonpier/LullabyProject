using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Fungus;

public class BookshelfInteraction_ML : MonoBehaviour
{
    [SerializeField] GameObject halo;
    [SerializeField] GameObject smoke;
    [SerializeField] private float fadingTime;
    [SerializeField] AudioManager audio;
    [SerializeField] Flowchart flowchart;
    [SerializeField] string dialogue;
    [SerializeField] GameObject player;

    SpriteRenderer renderer;
    Collider2D collider;
    Animator playerAnim;
    PlayerMove_KT playerScript;
    ChangeWeapon_NN playerWeapon;
    Rigidbody2D playerRB;

    private bool check;

    private void Start()
    {
        renderer = smoke.GetComponent<SpriteRenderer>();
        collider = smoke.GetComponent<Collider2D>();
        playerAnim = player.GetComponent<Animator>();
        playerScript = player.GetComponent<PlayerMove_KT>();
        playerWeapon = player.GetComponent<ChangeWeapon_NN>();
        playerRB = player.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetButtonDown("Interaction") && collision.tag == "Player" && !check)
        {
            audio.PlaySound("bookshelf interaction");
            Invoke("SlidingWall", .1f);
            check = true;
            playerAnim.enabled = false;
            playerScript.enabled = false;
            playerWeapon.enabled = false;
            playerRB.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            flowchart.ExecuteBlock(dialogue);
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
