using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;

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

    [SerializeField] GameObject lightSource;

    [SerializeField] AudioManager audio;

    [SerializeField] GameObject playerObj;
    ChangeWeapon_NN weapon;

    [SerializeField] GameObject gameCamera;
    PositionConstraint cameraCon;
    [SerializeField] GameObject checkpoint;

    EnemyController_ML enemy;
    public GameObject[] bookMonsters;
    public GameObject[] lampMonsters;
    public GameObject[] chandelierMonsters;
    public GameObject[] vacuumMonsters;
    private int maxMonsters;

    bool check = false;
    private bool firstCheck = false;

    public bool FirstCheck { get => firstCheck; set => firstCheck = value; }

    // Start is called before the first frame update
    void Start()
    {
        cameraCon = gameCamera.GetComponent<PositionConstraint>();
        weapon = playerObj.GetComponent<ChangeWeapon_NN>();
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
            firstCheck = true;
            PlayerStats_ML.instance.respawnPoint = collision.transform.position;
            anim.SetBool("activated", true);
            lightSource.SetActive(true);

            if (check == false)
            {
                audio.PlaySound("checkpoint bell");
                check = true;
            }

            ui.Light = 100f;
            uiL.Light = 100f;

            contentCandle.fillAmount = 1f;
            contentLantern.fillAmount = 1f;

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

        playerStat.ResetHealth();
        weapon.enabled = true;

        if (firstCheck == true)
        {
            Debug.Log("respawn");
            playerObj.transform.position = PlayerStats_ML.instance.respawnPoint;
            playerObj.GetComponent<PlayerMove_KT>().CheckRoomSize(checkpoint.transform.parent.gameObject);
            Invoke("CameraConstraints", 0.5f);
        }
        else if (firstCheck == false)
        {
            Debug.Log("respawn 2");
            player.transform.position = new Vector3(-11.2f, -18.93f, 0f);

            var playerMove = playerObj.GetComponent<PlayerMove_KT>();
            playerMove.CheckRoomSize(playerMove.StartRoom);
        }

        playerStat.ResetHealth();
        weapon.enabled = true;

        cameraCon.gameObject.GetComponent<TargetCamera_KT>().Reset();

        for (int i = 0; i < vacuumMonsters.Length; i++)
        {

            vacuumMonsters[i].GetComponent<VacuumMonsterBehaviour>().Respawn();

        }

     
        for (int i = 0; i < lampMonsters.Length; i++)
        {

            lampMonsters[i].GetComponent<LampMonsterBehaviour_ML>().Respawn();

        }

       
        for (int i= 0; i < bookMonsters.Length; i++)
        {

            lampMonsters[i].GetComponent<LampMonsterBehaviour_ML>().Respawn();

        }


    }


    void CameraConstraints()
    {
        cameraCon.enabled = true;
    }
}
