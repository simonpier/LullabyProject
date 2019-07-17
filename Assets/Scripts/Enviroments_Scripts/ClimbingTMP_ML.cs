using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class ClimbingTMP_ML : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject mainCamera;

    [SerializeField] private float endingDistanceY = 4.7f;

    PositionConstraint cameraCon;

    private float endingPositionY;
    private float climbedPosY;
    private float originalPosY;
    private bool check;

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
                cameraCon.constraintActive = false;
                player.transform.position = new Vector2(player.transform.position.x, endingPositionY);
            }
            else if (player.transform.position.y > originalPosY)
            {
                cameraCon.constraintActive = true;
                player.transform.position = new Vector2(player.transform.position.x, originalPosY);
            }
        }
    }
}
