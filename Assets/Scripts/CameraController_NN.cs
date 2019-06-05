using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController_NN : MonoBehaviour
{

    public GameObject player;
    private Vector3 offset;
    //Upper limit per floor size
    private float small = 18.0f, medium = 45.0f, big = 140.0f;
    private int progress = 0;
    private float[] floorSize ;
  
    void Start()
    {
        //Get the distance between the player and the camera and calculate its offset value.
        offset = transform.position - player.transform.position;
        floorSize = new float[3]{ small, medium, big};
    }

    void LateUpdate()
    {
        //Assign player position and offset value to camera position
        transform.position = player.transform.position + offset;
        Clamp();
    }
    //Limit the range of movement
    private void Clamp()
    {
        Vector3 pos = transform.position;
        // Position restriction
        pos.x = Mathf.Clamp(pos.x, -7,floorSize[progress]);
        // Assign the restricted value
        transform.position = pos;

        if (Input.GetKeyDown("i"))
        {
            Debug.Log("floorsize"+floorSize[progress]);
            Debug.Log("progress"+progress);
        }
        //if (Input.GetKeyDown("i"))
        //{
        //    SetMaximum();
        //}


    }
    //Determine the upper limit of the camera
    private void SetMaximum()
    {
        if (progress < 3)
        {
            progress += 1;
        }
    }
}
