using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class TableMonsterBehaviour_ML : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject snakeCat;
    [SerializeField] Flowchart flowchart;


    PlayerStats_ML playerStats;
    SnakeBossFightTrigger_ML bossTrigger;
    Animator anim;

    private bool puzzleUnsolved;
    private bool puzzleSolved;

    private bool puzzleCheck;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playerStats = player.GetComponent<PlayerStats_ML>();
        bossTrigger = snakeCat.GetComponent<SnakeBossFightTrigger_ML>();
    }

    // Update is called once per frame
    void Update()
    {
        puzzleUnsolved = flowchart.GetBooleanVariable("puzzleUnsolved");

        if (puzzleUnsolved && !puzzleCheck)
        {
            anim.SetBool("attack", true);
            puzzleCheck = true;
        }

        puzzleSolved = flowchart.GetBooleanVariable("puzzleSolved");

        if (puzzleSolved && !puzzleCheck)
        {
            bossTrigger.WoodPiecesCount++;
            puzzleCheck = true;
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
