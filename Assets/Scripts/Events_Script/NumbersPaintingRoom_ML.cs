using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumbersPaintingRoom_ML : MonoBehaviour
{
    [SerializeField] GameObject rightPainting;
    [SerializeField] GameObject snakeBoss;

    private bool check;
    private bool rightNumber;
    public bool RightNumber { get => rightNumber; set => rightNumber = value; }

    SnakeBossFightTrigger_ML snakeTrigger;

    // Start is called before the first frame update
    void Start()
    {
        snakeTrigger = snakeBoss.GetComponent<SnakeBossFightTrigger_ML>();
        Invoke("NumberCheck", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {

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
        if (collision.tag == "Player" && Input.GetButtonDown("Interaction") && !check && rightNumber)
        {
            snakeTrigger.WoodPiecesCount++;
            check = true;
        }
    }
}
