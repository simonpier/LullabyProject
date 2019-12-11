using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;
using Fungus;
using TMPro;

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

    //added by Tatuyoshi
    public static int Deadcount = 0;
    [SerializeField] Checkpoint_Manager_ML checkManager;

    //Table monster reset
    [SerializeField] GameObject tableMonster;
    Collider2D tableMonsterCollider;

    [SerializeField] GameObject localizationManager;
    [SerializeField] TextMeshProUGUI levelLocation;
    [SerializeField] GameObject playerObject;
    [SerializeField] InGameUI_SP candleScript;
    [SerializeField] InGameUILantern_SP lanternScript;

    private string scene;
    private string leng;

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
        playerStat.respawnPoint = player.transform.position;
        instance = this;

        tableMonsterCollider = tableMonster.GetComponent<Collider2D>();

        scene = SceneManager.GetActiveScene().name;
        leng = localizationManager.GetComponent<Localization>().ActiveLanguage;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //this is used to modify the respawnpoint
        if ((collision.gameObject.tag == "Player") && Input.GetButtonDown("Interaction"))
        {
            if(checkManager) checkManager.UnCheck();
            firstCheck = true;
            PlayerStats_ML.instance.respawnPoint = collision.transform.position;

            checkpoint = this.gameObject;

            anim.SetBool("activated", true);
            lightSource.SetActive(true);

            audio.PlaySound("checkpoint bell");
            if (check == false)
            {
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

            SaveSystem_SP.SavePlayer(playerObject, candleScript, lanternScript, localizationManager, levelLocation, scene, checkpoint.name);
        }
    }

    //This method must be called when we want to respawn the player to the last checkpoint
    public void Respawn()
    {
        playerStat.ResetHealth();
        weapon.enabled = true;
        playerObj.GetComponent<PlayerMove_KT>().ResetVerticalAnimate();
        Deadcount++;

        if (firstCheck == true)
        {
            Debug.Log("respawn");
            playerObj.transform.position = PlayerStats_ML.instance.respawnPoint;

            var itr = checkpoint.transform;
            while(itr != null)
            {
                if (itr.name.Contains("Env")) break;
                itr = itr.parent;
            }
            if (!itr) Debug.LogError("Cant find object named \"Enviroment\"");
            playerObj.GetComponent<PlayerMove_KT>().CheckRoomSize(itr.parent.gameObject);

            Invoke("CameraConstraints", 0.5f);
        }
        else if (firstCheck == false)
        {
            
            player.transform.position = playerStat.respawnPoint;

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
            bookMonsters[i].GetComponent<BookMonster_Behaviour>().Respawn();
            bookMonsters[i].gameObject.SetActive(false);
        }

        tableMonsterCollider.enabled = true;
    }


    void CameraConstraints()
    {
        cameraCon.enabled = true;
    }
}
