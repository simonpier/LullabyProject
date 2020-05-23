using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLibraryEvent_ML : MonoBehaviour
{
    [SerializeField] GameObject bookMonster;

    private bool playerLibraryEvent;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interaction") && playerLibraryEvent)
        {
            if (!bookMonster.activeSelf) bookMonster.transform.position = this.transform.position + new Vector3(-1.5f, -1.5f, 0);
            bookMonster.SetActive(true);
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerLibraryEvent = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerLibraryEvent = false;
        }
    }
}
