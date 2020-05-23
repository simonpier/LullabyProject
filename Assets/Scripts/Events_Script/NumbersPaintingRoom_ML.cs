using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using TMPro;

public class NumbersPaintingRoom_ML : MonoBehaviour
{
    [SerializeField] GameObject rightPainting;
    [SerializeField] bool normalPaintingFlag = true;
    [SerializeField] GameObject snakeBoss;
    [SerializeField] Flowchart flowchart;
    [SerializeField] string dialogue;
    [SerializeField] GameObject player;
    [SerializeField] GameObject textBlock;

    Behaviour halo;
    
    
    Animator playerAnim;
    PlayerMove_KT playerScript;
    ChangeWeapon_NN playerWeapon;
    Rigidbody2D playerRB;
    TextMeshProUGUI text;

    private bool check;
    private bool rightNumber;
    private bool playerFirstPainting;
    private bool playerSecondPainting;
    public bool RightNumber { get => rightNumber; set => rightNumber = value; }



    SnakeBossFightTrigger_ML snakeTrigger;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        halo = GetComponent<Behaviour>();

        snakeTrigger = snakeBoss.GetComponent<SnakeBossFightTrigger_ML>();
        Invoke("NumberCheck", 0.5f);

        playerAnim = player.GetComponent<Animator>();
        playerScript = player.GetComponent<PlayerMove_KT>();
        playerWeapon = player.GetComponent<ChangeWeapon_NN>();
        playerRB = player.GetComponent<Rigidbody2D>();
        if(textBlock) text = textBlock.GetComponent<TextMeshProUGUI>();

    }

    // Update is called once per frame
    void Update()
    {
        if (playerFirstPainting && Input.GetButtonDown("Interaction") && !check && rightNumber && normalPaintingFlag)
        {
            snakeTrigger.WoodPiecesCount++;
            check = true;

            playerAnim.enabled = false;
            playerScript.enabled = false;
            playerWeapon.enabled = false;
            playerRB.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            flowchart.ExecuteBlock(dialogue);

            text.text = "" + snakeTrigger.WoodPiecesCount;
        }

        if (playerSecondPainting && Input.GetButtonDown("Interaction") && !rightNumber && flowchart && normalPaintingFlag)
        {

            playerAnim.enabled = false;
            playerScript.enabled = false;
            playerWeapon.enabled = false;
            playerRB.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            flowchart.ExecuteBlock(dialogue);

        }
    }

    void NumberCheck()
    {
        if (transform.position.x == rightPainting.transform.position.x)
        {
            rightNumber = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" )
        {
            playerFirstPainting = true;
            playerSecondPainting = true;
        }

        if (collision.tag == "Player_LanternCollider" || collision.tag == "Player_CandleCollider")
        {
            halo.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player_LanternCollider" || collision.tag == "Player_CandleCollider")
        {
            halo.enabled = false;
        }

        if (collision.tag == "Player")
        {
            playerFirstPainting = false;
            playerSecondPainting = false;
        }
    }
}
