using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class DoorTrigger_v2 : MonoBehaviour
{
    [SerializeField] GameObject doorNormal;
    EnviromentInteraction_SP door;
    [SerializeField] GameObject destination;
    [SerializeField] GameObject player;
    [SerializeField] GameObject gameCamera;
    PositionConstraint cameraCon;
  



    private void Start()
    {
        cameraCon = gameCamera.GetComponent<PositionConstraint>();
        door = doorNormal.GetComponent<EnviromentInteraction_SP>();
        Invoke("OnTriggerStay2d", 2);
    }


    void OnTriggerStay2D(Collider2D other)
    {

        if ((other.gameObject.tag == "Player") && Input.GetKeyDown(KeyCode.Return))
        {
            door.DoorOpen();
            Invoke("Teleportation", 1);
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {

        if (other.tag == "Player")
        {

            door.DoorClose();

        }

    }


    void Teleportation()
    {

        player.transform.position = destination.transform.position;
        player.GetComponent<PlayerMove_KT>().CheckRoomSize(destination.transform.parent.gameObject);
        Invoke("CameraConstraints", 0.5f);

    }

    void CameraConstraints()
    {
        cameraCon.enabled = true;
    }
}
