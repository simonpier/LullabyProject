using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Checkpoint_ML : MonoBehaviour
{
    #region Singleton
    public static Checkpoint_ML instance;
    #endregion

    Animator anim;
    Transform player;
    PlayerStats_ML playerStat;

    [SerializeField] private GameObject candle;
    [SerializeField] private GameObject lantern;
    Light light, lightLantern;

    [SerializeField] GameObject candleBar;
    InGameUI_SP ui;

    [SerializeField] GameObject lanternBar;
    InGameUILantern_SP uiL;

    [SerializeField] Image contentCandle;
    [SerializeField] Image contentLantern;

    Collider2D candleCollider;
    [SerializeField] GameObject lanternCollider;

    // Start is called before the first frame update
    void Start()
    {
        light = candle.GetComponent<Light>();
        lightLantern = lantern.GetComponent<Light>();
        uiL = lanternBar.GetComponent<InGameUILantern_SP>();
        ui = candleBar.GetComponent<InGameUI_SP>();
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

            ui.Light = 100f;
            uiL.Light = 100f;

            if (ui.candleOn_Off == false)
            {

                ui.candleOn_Off = true;
                light.enabled = true;
                candleCollider = candle.GetComponent<Collider2D>();
                candleCollider.enabled = true;
            }

            if (uiL.lanternOn_Off == false)
            {

                uiL.lanternOn_Off = true;
                lightLantern.enabled = true;
                lanternCollider.SetActive(true);

            }
        }
    }

    //This method must be called when we want to respawn the player to the last checkpoint
    public void Respawn()
    {
        player.position = PlayerStats_ML.instance.respawnPoint;
        playerStat.ResetHealth();    
    }

}
