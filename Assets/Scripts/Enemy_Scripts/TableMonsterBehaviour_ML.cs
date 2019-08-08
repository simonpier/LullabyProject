using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class TableMonsterBehaviour_ML : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject snakeCat;
    [SerializeField] Flowchart flowchart;
    [SerializeField] string dialogue;
    [SerializeField] string puzzle;

    PlayerStats_ML playerStats;
    SnakeBossFightTrigger_ML bossTrigger;
    Animator anim;
    Animator playerAnim;
    PlayerMove_KT playerScript;
    ChangeWeapon_NN playerWeapon;
    Rigidbody2D playerRB;

    private bool puzzleUnsolved;
    private bool puzzleSolved;

    private bool puzzleCheck;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playerStats = player.GetComponent<PlayerStats_ML>();
        bossTrigger = snakeCat.GetComponent<SnakeBossFightTrigger_ML>();
        playerAnim = player.GetComponent<Animator>();
        playerScript = player.GetComponent<PlayerMove_KT>();
        playerWeapon = player.GetComponent<ChangeWeapon_NN>();
        playerRB = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        puzzleUnsolved = flowchart.GetBooleanVariable("puzzleUnsolved");

        if (puzzleUnsolved && !puzzleCheck)
        {
            anim.SetBool("attack", true);
            puzzleCheck = true;
            playerAnim.enabled = true;
            playerScript.enabled = true;
            playerWeapon.enabled = true;
            playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
            Invoke("PlayerKill", 0.2f);
        }

        puzzleSolved = flowchart.GetBooleanVariable("puzzleSolved");

        if (puzzleSolved && !puzzleCheck)
        {
            bossTrigger.WoodPiecesCount++;
            puzzleCheck = true;
            playerAnim.enabled = false;
            playerScript.enabled = false;
            playerWeapon.enabled = false;
            playerRB.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            flowchart.ExecuteBlock(dialogue);
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Player") && Input.GetButtonDown("Interaction"))
        {

            playerAnim.enabled = false;
            playerScript.enabled = false;
            playerWeapon.enabled = false;
            playerRB.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            flowchart.ExecuteBlock(puzzle);

        }
    }

    private void PlayerKill()
    {
        playerStats.PlayerDeath();
        anim.SetBool("attack", false);
        flowchart.SetBooleanVariable("puzzleUnsolved", false);
        puzzleCheck = false;
    }
}
