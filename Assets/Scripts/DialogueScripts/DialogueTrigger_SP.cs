using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger_SP : MonoBehaviour
{
    public Dialogue_SP dialogue;

    public void TriggerDialogue()
    {

        FindObjectOfType<DialogueManager_SP>().StartDialogue(dialogue);

    }
}
