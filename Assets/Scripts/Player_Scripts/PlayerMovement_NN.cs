﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_NN : MonoBehaviour
{
    Rigidbody2D rigidbody;
    SpriteRenderer mainSpriteRenderer;
    GameObject Player;
    //public bool checkFlog;
    //Player's movement speed
    [SerializeField] private float _speed = 2.0f;

    [SerializeField] private GameObject candle;

    private bool candleSwitch;

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
        mainSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Go right and Walking Anim
        if (Input.GetKey("d"))
        {
            GetComponent<Animator>().SetBool("Walking", true);
            this.transform.rotation = new Quaternion(0, 0, 0, 0);
            Move(_speed);
        }
        //Go left and Walking Anim
        else if (Input.GetKey("a"))
        {
            GetComponent<Animator>().SetBool("Walking", true);
            this.transform.rotation = new Quaternion(0, 180.0f, 0, 0);
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            candleSwitch = !candleSwitch;
            TurnLight();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            CheckAction();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            ChangeWeapon();
        }
    }



    private void Move(float speed)
    {
        Player.GetComponent<Rigidbody2D>().velocity = (Vector3.right * speed);
    }
    private void TurnLight()
    {
        Debug.Log("Light");
        candle.SetActive(candleSwitch);
    }
    public void CheckAction()
    {
        Debug.Log("Check");
        mainSpriteRenderer.sprite = backSprite;
    }
    private void ChangeWeapon()
    {
        Debug.Log("Change");
    }
}
