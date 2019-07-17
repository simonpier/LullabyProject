using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class ClimbingTMP_ML : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject mainCamera;
    [SerializeField] private bool check = false;


    [SerializeField] private float endingDistanceY = 4.7f;

    PositionConstraint cameraCon;

    private float endingPositionY;
    private static float climbedPosY;
    private static float originalPosY;


    // Start is called before the first frame update
    void Start()
    {
        cameraCon = mainCamera.GetComponent<PositionConstraint>();
    }

    // Update is called once per frame
    void Update()
    {
        endingPositionY = player.transform.position.y + endingDistanceY;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.Q) && collision.tag == "Player")
        {
            if (check == false)
            {
                originalPosY = player.transform.position.y;
                climbedPosY = endingPositionY;
                check = true;
            }
            if (player.transform.position.y < climbedPosY)
            {
                cameraCon.translationAxis = Axis.X | Axis.Z;
                player.transform.position = new Vector2(player.transform.position.x, endingPositionY);
            }
            else if (player.transform.position.y > originalPosY)
            {
                cameraCon.translationAxis = Axis.X | Axis.Y | Axis.Z;
                player.transform.position = new Vector2(player.transform.position.x, originalPosY);
            }
        }
    }
}
