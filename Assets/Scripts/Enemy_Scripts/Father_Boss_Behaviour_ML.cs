using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Father_Boss_Behaviour_ML : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject darkBullet;
    [SerializeField] GameObject bossRoomPosDX;
    [SerializeField] GameObject bossRoomPosSX;

    [SerializeField] float speed;

    private GameObject instanceBullet;

    private bool facingLeft = true;
    private bool isGoingRight;
    private bool isGoingLeft;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BasicMovement();

        if (Input.GetKeyDown(KeyCode.L))
        {
            RangeAttack();
        }
    }

    private void BasicMovement()
    {
        if (player.transform.position.x <= transform.position.x)
        {
            if (!facingLeft)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);
            }

            facingLeft = true;
        }
        else if (player.transform.position.x >= transform.position.x)
        {
            if (facingLeft)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);
            }

            facingLeft = false;
        }

        if (!facingLeft)
        {
            Vector2 targetPos = new Vector2(player.transform.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            isGoingRight = true;
            isGoingLeft = false;
        }
        else if (facingLeft)
        {
            Vector2 targetPos = new Vector2(player.transform.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            isGoingLeft = true;
            isGoingRight = false;
        }

        if (isGoingRight && facingLeft)
        {
            facingLeft = false;
        }
        else if (isGoingLeft && !facingLeft)
        {
            facingLeft = true;
        }
    }

    private void RangeAttack()
    {
        instanceBullet = Instantiate(darkBullet, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        instanceBullet.transform.localScale = new Vector3(instanceBullet.transform.localScale.x * transform.localScale.x, instanceBullet.transform.localScale.y, instanceBullet.transform.localScale.z);
    }
}
