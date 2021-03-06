﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternEvent_ML : MonoBehaviour
{
    [SerializeField] GameObject gameManager;
    EventManager_ML eventManager;

    private bool playerLantern;

    // Start is called before the first frame update
    void Start()
    {
        eventManager = gameManager.GetComponent<EventManager_ML>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interaction") && playerLantern)
        {
            eventManager.LanternTaken = true;

            Destroy(gameObject, 0.1f);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerLantern = true;
        }
    }

    private void OnDestroy()
    {
        playerLantern = false;
    }

}
