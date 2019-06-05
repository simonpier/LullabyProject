using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_NN : MonoBehaviour
{
    Rigidbody2D rigidbody;
    SpriteRenderer mainSpriteRenderer;
    PlayerStats_ML playerStatus;
    GameObject Player;
    //public bool checkFlog;
    //Player's movement speed
    [SerializeField] private float _speed = 2.0f;
    private Vector3 homePos;
    
    [SerializeField] private Sprite raisingSpriteRight;
    [SerializeField] private Sprite raisingSpriteLeft;
    [SerializeField] private Sprite loweringSpriteRight;
    [SerializeField] private Sprite loweringSpriteLeft;
    [SerializeField] private Sprite backSprite;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        Player = GameObject.Find("Player");
        playerStatus = gameObject.GetComponent<PlayerStats_ML>();
        mainSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        homePos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Go right and Walking Anim
        if (Input.GetKey("d"))
        {
            GetComponent<Animator>().SetBool("Walking", true);
            this.transform.rotation = new Quaternion(0, 180.0f, 0, 0);
            Move(_speed);
        }
        //Go left and Walking Anim
        else if (Input.GetKey("a"))
        {
            GetComponent<Animator>().SetBool("Walking", true);
            this.transform.rotation = new Quaternion(0, 0, 0, 0);
            Move(-_speed);
        }
        //Stop Anim
        else
        {
            Player.GetComponent<Rigidbody2D>().velocity = (Vector3.right * 0.0f);
            GetComponent<Animator>().SetBool("Walking", false);
        }
        //Change to raising a candle 
        if (Input.GetKeyDown("w"))
        {
            GetComponent<Animator>().SetBool("Raising", true);
            mainSpriteRenderer.sprite = raisingSpriteRight;
            Debug.Log(mainSpriteRenderer.sprite);
        }
        //Change to lowering a candle
        else if (Input.GetKeyDown("s"))
        {
            GetComponent<Animator>().SetBool("Raising", false);
            mainSpriteRenderer.sprite = loweringSpriteRight;
        }        
        Clamp();
        if(playerStatus.Health == 0)
        {
            //RetrunPos();
            Debug.Log("Death");
        }
    }



    private void Move(float speed)
    {
        Player.GetComponent<Rigidbody2D>().velocity = (Vector3.right * speed);
    }
    //Limit the range of movement
    private void Clamp()
    {    
        Vector2 pos = transform.position;
        // Position restriction
        pos.x = Mathf.Clamp(pos.x, -15, 65);
        // Assign the restricted value
        transform.position = pos;
    }
    
    //If the player's health reaches 0, return to the start position

    //private void RetrunPos()
    //{
    //    transform.position = homePos;
    //    playerStatus.ResetHealth();
    //}
}
