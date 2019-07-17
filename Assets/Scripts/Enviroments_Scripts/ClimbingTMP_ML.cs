using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingTMP_ML : MonoBehaviour
{
    [SerializeField] GameObject player;

    [SerializeField] private float endingDistanceY = 4.7f;
    private float endingPositionY;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        endingPositionY = player.transform.position.y + endingDistanceY;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            player.transform.position = new Vector2(player.transform.position.x, endingPositionY);
        }
    }
}
