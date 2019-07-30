using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeapon_NN : MonoBehaviour
{
    public enum WEAPON
    {
        Candle,
        Lantern,
        None,
    }

    [SerializeField] private GameObject candle;
    [SerializeField] private GameObject lantern;
    [SerializeField] AudioManager audio;

    Dictionary<GameObject, LightRotate_KT> pair;

    #region Added by Simone and Mauro
    [SerializeField] GameObject lanternCollider;
    [SerializeField] GameObject ui;
    [SerializeField] GameObject uiL;
    [SerializeField] GameObject gameManager;

    EventManager_ML eventManager;
    InGameUI_SP candleBar;
    InGameUILantern_SP lanternBar;
    Light light, lightLantern;
    GameObject _nowSelectInstance;
    Collider2D candleCollider;
    #endregion


    //if light raise
    public bool LightRaise { get; private set; }

    public WEAPON NowWeapon { get; set; }

    public bool check = true;

    void Start()
    {
        eventManager = gameManager.GetComponent<EventManager_ML>();
        light = candle.GetComponent<Light>();
        lightLantern = lantern.GetComponent<Light>();
        candleBar = ui.GetComponent<InGameUI_SP>();
        lanternBar = uiL.GetComponent<InGameUILantern_SP>();
        pair = new Dictionary<GameObject, LightRotate_KT>();
        pair[candle] = candle.GetComponent<LightRotate_KT>();
        pair[lantern] = lantern.GetComponent<LightRotate_KT>();
        ResetAllLight();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown("w"))
        {
            LightRaise = true;
            pair[_nowSelectInstance].Raise();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown("s"))
        {
            LightRaise = false;
            pair[_nowSelectInstance].Drop();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {

            candle.gameObject.SetActive(_nowSelectInstance == candle && !_nowSelectInstance.activeSelf);
            lantern.gameObject.SetActive(_nowSelectInstance == lantern && !_nowSelectInstance.activeSelf);

            LightRaise = false;
            pair[_nowSelectInstance].Drop();

            WeaponStateUpdate();


        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (lantern.activeSelf == true)
            {
                bool temp = _nowSelectInstance.activeSelf;
                _nowSelectInstance.SetActive(false);
                _nowSelectInstance = (_nowSelectInstance == candle) ? lantern : candle;
                _nowSelectInstance.SetActive(temp);

                LightRaise = false;
                pair[_nowSelectInstance].Drop();
                WeaponStateUpdate();
            }
            else if (candle.activeSelf == true && eventManager.LanternTaken)
            {
                bool temp = _nowSelectInstance.activeSelf;
                _nowSelectInstance.SetActive(false);
                _nowSelectInstance = (_nowSelectInstance == candle) ? lantern : candle;
                _nowSelectInstance.SetActive(temp);

                LightRaise = false;
                pair[_nowSelectInstance].Drop();
                WeaponStateUpdate();
            }

            //For now, anne drop light when switching weapon


            

            if (lantern.activeSelf == true) //lantern on sound
                audio.PlaySound("lantern_on/off");

            if (lantern.activeSelf == false && candle.activeSelf == true) //candle on sound
                audio.PlaySound("candle_on");
        }

        if (candleBar.candleOn_Off == false)
        {

            
            light.enabled = false;
            candleCollider = candle.GetComponent<Collider2D>();
            candleCollider.enabled = false;

        }

        if (lanternBar.lanternOn_Off == false)
        {


            lightLantern.enabled = false;
            lanternCollider.SetActive(false);
        }

    }

    void WeaponStateUpdate()
    {
        NowWeapon = _nowSelectInstance == candle ? WEAPON.Candle : WEAPON.Lantern;
        NowWeapon = _nowSelectInstance.activeSelf ? NowWeapon : WEAPON.None;
    }

    public void ResetAllLight()
    {
        _nowSelectInstance = candle;
        candle.SetActive(false);
        lantern.SetActive(false);
        NowWeapon = WEAPON.None;
    }

    //return light value in 0.0f~1.0f
    public float LightAmount(WEAPON key)
    {
        switch (key)
        {
            case WEAPON.Candle:
                return pair[candle].NowAmount;
            case WEAPON.Lantern:
                return pair[lantern].NowAmount;
        }
        return 0.0f;
    }


}
