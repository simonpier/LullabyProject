using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;


public class GetItem_SP : MonoBehaviour
{

    [SerializeField] Flowchart flowchart;
    [SerializeField] string dialogue;
    [SerializeField] GameObject snakeBoss;
    [SerializeField] GameObject player;

    Animator playerAnim;
    PlayerMove_KT playerScript;
    ChangeWeapon_NN playerWeapon;
    Rigidbody2D playerRB;
    SnakeBossFightTrigger_ML snakeTrigger;

    private bool check = true, switchOn_Off = false;

    public bool SwitchOn_Off { get => switchOn_Off; set => switchOn_Off = value; }

    private void Start()
    {
        snakeTrigger = snakeBoss.GetComponent<SnakeBossFightTrigger_ML>();
        playerAnim = player.GetComponent<Animator>();
        playerScript = player.GetComponent<PlayerMove_KT>();
        playerWeapon = player.GetComponent<ChangeWeapon_NN>();
        playerRB = player.GetComponent<Rigidbody2D>();

    }

    private void OnTriggerStay2D(Collider2D other)
    {

        if ((other.gameObject.tag == "Player") && check == true && Input.GetButtonDown("Interaction"))
        {
            
            playerAnim.enabled = false;
            playerScript.enabled = false;
            playerWeapon.enabled = false;
            playerRB.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            flowchart.ExecuteBlock(dialogue);
            check = false;
            switchOn_Off = true;
        }

    }
}
