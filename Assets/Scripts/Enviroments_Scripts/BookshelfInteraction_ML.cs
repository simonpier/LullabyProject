using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Fungus;

public class BookshelfInteraction_ML : MonoBehaviour
{
    [SerializeField] GameObject halo;
    [SerializeField] GameObject smoke;
    [SerializeField] GameObject button;
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
    private bool wallSlided;

    //Tatsuyoshi added
    private bool messageFlag;
    private bool leverFlag;

    private void Start()
    {
        messageFlag = flowchart != null;
        leverFlag = smoke != null && halo != null && button != null;

        if (leverFlag)
        {
            renderer = smoke.GetComponent<SpriteRenderer>();
            collider = smoke.GetComponent<Collider2D>();
        }

        playerAnim = player.GetComponent<Animator>();
        playerScript = player.GetComponent<PlayerMove_KT>();
        playerWeapon = player.GetComponent<ChangeWeapon_NN>();
        playerRB = player.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(Input.GetButtonDown("Interaction") && check && !wallSlided)
        {
            audio.PlaySound("bookshelf interaction");

            if (leverFlag)
            {
                Invoke("SlidingWall", .1f);
                halo.SetActive(false);
                button.SetActive(false);
            }

            if (messageFlag)
            {
                playerAnim.enabled = false;
                playerScript.enabled = false;
                playerWeapon.enabled = false;
                playerRB.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                flowchart.ExecuteBlock(dialogue);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !check)
        {
            check = true;
        }

        if (!check && leverFlag && (collision.tag == "Player_CandleCollider" || collision.tag == "Player_LanternCollider"))
        {
            halo.SetActive(true);
            button.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (leverFlag && (collision.tag == "Player_CandleCollider" || collision.tag == "Player_LanternCollider"))
        {
            halo.SetActive(false);
            button.SetActive(false);
        }

        if (collision.tag == "Player")
        {
            check = false;
        }
    }

    private void SlidingWall()
    {
        wallSlided = true;
        renderer.DOFade(0f, fadingTime);
        collider.enabled = false;
        audio.PlaySound("wall moving");
    }

}
