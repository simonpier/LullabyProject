using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class FungusDialogue_SP : MonoBehaviour
{

    [SerializeField] Flowchart flowchart;
    [SerializeField] string dialogue;

    private bool check = true;

    private void OnTriggerEnter2D(Collider2D other)
    {

        if ((other.gameObject.tag == "Player") && check == true)
        {

            flowchart.ExecuteBlock(dialogue);
            check = false;
        }

    }
}
