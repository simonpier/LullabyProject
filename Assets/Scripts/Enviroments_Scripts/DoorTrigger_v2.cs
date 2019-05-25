using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger_v2 : MonoBehaviour
{
    [SerializeField] GameObject doorNormal;
    EnviromentInteraction_SP door;

    private void Start()
    {

        door = doorNormal.GetComponent<EnviromentInteraction_SP>();

    }


    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            Debug.Log("Open");
            door.DoorOpen();
            

        }

    }

    void OnTriggerExit2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            Debug.Log("Close");
            door.DoorClose();
            
        }

    }

}
