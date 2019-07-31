using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondLibraryEvent_ML : MonoBehaviour
{
    private bool switchActivated;
    public bool SwitchActivated { get => switchActivated; set => switchActivated = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player" && Input.GetButtonDown("Interaction"))
        {
            switchActivated = true;
        }
    }
}
