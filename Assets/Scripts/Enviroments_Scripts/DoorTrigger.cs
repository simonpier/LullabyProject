using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] EnviromentInteraction_SP door;
    [SerializeField] PlayerPrefs player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if( other.tag == "player")
        {

            door.DoorOpen();

            
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.tag == "player")
        {

            door.DoorClose();

        }

    }

}
