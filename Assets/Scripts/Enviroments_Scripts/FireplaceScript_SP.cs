using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class FireplaceScript_SP : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Flowchart flowchart;
    [SerializeField] string dialogue;

    Animator playerAnim;
    PlayerMove_KT playerScript;
    Rigidbody2D playerRB;

    private void Start()
    {
        playerAnim = player.GetComponent<Animator>();
        playerScript = player.GetComponent<PlayerMove_KT>();
        playerRB = player.GetComponent<Rigidbody2D>();
    }

    void OnTriggerStay2D(Collider2D other)
    {

        if ((other.gameObject.tag == "Player") && Input.GetKeyDown(KeyCode.Return))
        {
            playerAnim.enabled = false;
            playerScript.enabled = false;
            playerRB.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            flowchart.ExecuteBlock(dialogue);
            
        }

    }
}
