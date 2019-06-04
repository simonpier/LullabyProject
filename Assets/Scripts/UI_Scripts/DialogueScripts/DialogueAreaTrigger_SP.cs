using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueAreaTrigger_SP : MonoBehaviour
{
    [SerializeField] GameObject player;

    public Dialogue_SP dialogue;

    private bool check = true;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (check == true)
        {

            FindObjectOfType<DialogueManager_SP>().StartDialogue(dialogue);
            check = false;
        }
    }
}
