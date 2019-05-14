using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_NN : MonoBehaviour
{
    Rigidbody2D rigidbody;
    GameObject Player;
    //Player's movement speed
    [SerializeField] private float _speed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //Go right and Walking Anim
        if (Input.GetKey("d"))
        {
            GetComponent<Animator>().SetBool("Walking", true);
            this.transform.rotation = new Quaternion(0,0,0,0);
            Move(_speed);
        }
        //Go left and Walking Anim
        else if (Input.GetKey("a"))
        {
            GetComponent<Animator>().SetBool("Walking", true);
            this.transform.rotation = new Quaternion(0,180.0f,0,0);
            Move(-_speed);
        }
        //Stop Anim
        else
        {
            Player.GetComponent<Rigidbody2D>().velocity = (Vector3.right * 0.0f);
            GetComponent<Animator>().SetBool("Walking", false);
        }       
    }

    private void Move(float speed)
    {
        Player.GetComponent<Rigidbody2D>().velocity = (Vector3.right * speed);
    }
}
