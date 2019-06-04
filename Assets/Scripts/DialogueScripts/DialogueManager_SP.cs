using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager_SP : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI dialogueText;

    [SerializeField] Animator animator;


    private Queue<string> senteces; //FIFO for the dialogue system. basicaly will load 
    //one per one dialogue lines

    // Start is called before the first frame update
    void Start()
    {

        senteces = new Queue<string>();

    }

    public void StartDialogue (Dialogue_SP dialogue)
    {
       
        animator.SetBool("isOpen", true);
        
        Debug.Log("starting conversation with " + dialogue.name);

        nameText.text = dialogue.name;

        senteces.Clear();

        foreach (string sentence in dialogue.senteces)
        {

            senteces.Enqueue(sentence);

        }

        DisplayNextSentence();

    }

    public void DisplayNextSentence()
    {

        if (senteces.Count == 0)
        {

            EndDialogue();
            return;

        }

        string sentence = senteces.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {

        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {

            dialogueText.text += letter;
            yield return null;


        }

    }

    void EndDialogue()
    {
        
        animator.SetBool("isOpen", false);
        
    }
}
