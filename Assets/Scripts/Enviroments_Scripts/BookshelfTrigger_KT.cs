﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookshelfTrigger_KT : MonoBehaviour
{
    [SerializeField]
    GameObject warpHeight;
    [SerializeField]
    GameObject moveArea;
    public bool isPlayerOn { get; set; }
    void Start()
    {
        isPlayerOn = false;
    }

    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if ((other.gameObject.tag == "Player"))
        {
            if (Input.GetKeyDown(KeyCode.Q) && warpHeight.transform.position.y - 0.01f > other.transform.position.y)
            {
                Climb(other.gameObject);
            }
            else if(Input.GetKeyDown(KeyCode.Q) && warpHeight.transform.position.y - 0.01f <= other.transform.position.y) // DownKeyCode
            {
                Dismount(other.gameObject);
            }
        }
    }

    void Climb(GameObject player)
    {
        if (PlayerMove_KT.Instance.MovingVertAnimation != PlayerMove_KT.VertAnimationType.Default) return;
        PlayerMove_KT.Instance.StartVerticalAnimate(warpHeight.transform.position.y, PlayerMove_KT.VertAnimationType.Climb);
        if(moveArea) moveArea.SetActive(true);
        warpHeight.transform.position = new Vector3(warpHeight.transform.position.x, player.transform.position.y, warpHeight.transform.position.z);
    }

    public void Dismount(GameObject player)
    {
        if (PlayerMove_KT.Instance.MovingVertAnimation != PlayerMove_KT.VertAnimationType.Default) return;
        PlayerMove_KT.Instance.StartVerticalAnimate(warpHeight.transform.position.y, PlayerMove_KT.VertAnimationType.Dismount);
        if (moveArea) moveArea.SetActive(false);
        warpHeight.transform.position = new Vector3(warpHeight.transform.position.x, player.transform.position.y, warpHeight.transform.position.z);
    }
}