using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using TMPro;

public class ThirdEnigmaScript_SP : MonoBehaviour
{

    [SerializeField] Flowchart flowchart;
    [SerializeField] string dialogue;
    [SerializeField] GameObject snakeBoss;
    [SerializeField] GameObject player;
    [SerializeField] GameObject libray;
    [SerializeField] GameObject textBlock;
    [SerializeField] GameObject halo;


    Animator playerAnim;
    PlayerMove_KT playerScript;
    ChangeWeapon_NN playerWeapon;
    Rigidbody2D playerRB;
    SnakeBossFightTrigger_ML snakeTrigger;
    GetItem_SP lever;
    TextMeshProUGUI text;

    private bool check = true, taken = false;

    private void Start()
    {
        lever = libray.GetComponent<GetItem_SP>();
        snakeTrigger = snakeBoss.GetComponent<SnakeBossFightTrigger_ML>();
        playerAnim = player.GetComponent<Animator>();
        playerScript = player.GetComponent<PlayerMove_KT>();
        playerWeapon = player.GetComponent<ChangeWeapon_NN>();
        playerRB = player.GetComponent<Rigidbody2D>();
        text = textBlock.GetComponent<TextMeshProUGUI>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {

        if ((other.gameObject.tag == "Player") &&  lever.SwitchOn_Off == true)
        {
            check = true; 
        }

        if (!check && (other.tag == "Player_CandleCollider" ) && lever.SwitchOn_Off == true)
        {
            halo.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if ((other.tag == "Player_CandleCollider" ) && lever.SwitchOn_Off == true)
        {
            halo.SetActive(false);
        }

        if (other.tag == "Player")
        {
            check = false;
        }
    }


    private void Update()
    {
        if (check == true && Input.GetButtonDown("Interaction") && check == true && lever.SwitchOn_Off == true && taken == false)
        {
            snakeTrigger.WoodPiecesCount++;

            playerAnim.enabled = false;
            playerScript.enabled = false;
            playerWeapon.enabled = false;
            playerRB.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            flowchart.ExecuteBlock(dialogue);
            text.text = "" + snakeTrigger.WoodPiecesCount;
            taken = true;
        }
    }
}
