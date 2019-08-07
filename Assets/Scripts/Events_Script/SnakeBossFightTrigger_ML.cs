using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBossFightTrigger_ML : MonoBehaviour
{

    Snakecat_Boss_Behaviour snakeBoss;

    private int woodPiecesCount;

    public int WoodPiecesCount { get => woodPiecesCount; set => woodPiecesCount = value; }

    // Start is called before the first frame update
    void Start()
    {
        snakeBoss = GetComponent<Snakecat_Boss_Behaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        BossTrigger();
    }

    void BossTrigger()
    {
        if(woodPiecesCount == 3)
        {
            snakeBoss.BossFightActive = true;
        }
    }
}
