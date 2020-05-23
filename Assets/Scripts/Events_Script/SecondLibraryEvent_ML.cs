using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondLibraryEvent_ML : MonoBehaviour
{
    private bool switchActivated;
    public bool SwitchActivated { get => switchActivated; set => switchActivated = value; }

    private bool playerSecondLibrary;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interaction") && playerSecondLibrary)
        {
            switchActivated = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerSecondLibrary = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerSecondLibrary = false;
        }
    }
}
