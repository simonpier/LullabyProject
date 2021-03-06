﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class FungusDialogue_SP : MonoBehaviour
{

    [SerializeField] Flowchart flowchart;
    [SerializeField] string dialogue;

    [SerializeField] GameObject player;
    Animator playerAnim;
    PlayerMove_KT playerScript;
    ChangeWeapon_NN playerWeapon;
    Rigidbody2D playerRB;

    [SerializeField] GameObject canvas;
    PauseMenu_SP pause;

    private bool check = true;

    private void Start()
    {
        playerAnim = player.GetComponent<Animator>();
        playerScript = player.GetComponent<PlayerMove_KT>();
        playerWeapon = player.GetComponent<ChangeWeapon_NN>();
        playerRB = player.GetComponent<Rigidbody2D>();
        pause = canvas.GetComponent<PauseMenu_SP>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if ((other.gameObject.tag == "Player") && check == true)
        {
            pause.enabled = false;
            playerAnim.enabled = false;
            playerScript.enabled = false;
            playerWeapon.enabled = false;
            playerRB.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            flowchart.ExecuteBlock(dialogue);
            check = false;
        }

    }


    void DilogueEnded()
    {
        pause.enabled = true;
        playerAnim.enabled = true;
        playerScript.enabled = true;
        playerWeapon.enabled = true;
        playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;

    }

}
