using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint_ML : MonoBehaviour
{
    #region Singleton
    public static Checkpoint_ML instance;
    #endregion

    Animator anim;
    Transform player;
    PlayerStats_ML playerStat;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = PlayerStats_ML.instance.player.transform;
        playerStat = PlayerStats_ML.instance;
        instance = this;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //this is used to modify the respawnpoint
        if ((collision.gameObject.tag == "Player") && Input.GetKeyDown(KeyCode.E))
        {
            PlayerStats_ML.instance.respawnPoint = collision.transform.position;
            anim.SetBool("activated", true);
        }
    }

    //This method must be called when we want to respawn the player to the last checkpoint
    public void Respawn()
    {
        player.position = PlayerStats_ML.instance.respawnPoint;
        playerStat.ResetHealth();    
    }

}
