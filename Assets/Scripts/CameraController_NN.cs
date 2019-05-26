using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController_NN : MonoBehaviour
{

    public GameObject player;
    private Vector3 offset;
  
    void Start()
    {
        //Get the distance between the player and the camera and calculate its offset value.
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        //Assign player position and offset value to camera position
        transform.position = player.transform.position + offset;
    }
}
