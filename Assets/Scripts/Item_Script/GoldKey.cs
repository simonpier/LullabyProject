﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldKey : MonoBehaviour
{
    [SerializeField]
    private int id = 3;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            PlayerItemRepository.AddItem(id);
            Destroy(this.gameObject);
        }
    }
}
