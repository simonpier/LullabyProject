using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats_ML : MonoBehaviour
{
    #region Singleton
    public static PlayerStats_ML instance;
    #endregion
    //Player Health;
    [SerializeField] private int health = 1;

    //Reference to the player
    public GameObject player;
    //Player respawnPoint it will be set in Checkpoint_ML
    public Vector2 respawnPoint;

    private void Awake()
    {
        instance = this;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //When the player collide with the enemy collider it health will be set to 0 due that he has just 1 hp
        if (collision.tag == ("Enemy_AttackCollider"))
        {
            health = 0;
        }
    }

    //This is used to reset player hp when respawn
    public void ResetHealth()
    {
        health = 1;
    }
}
