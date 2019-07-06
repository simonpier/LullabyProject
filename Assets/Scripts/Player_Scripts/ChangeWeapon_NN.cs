﻿using System.Collections;
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

    GameObject _nowSelectInstance;

    Dictionary<GameObject, LightRotate_KT> pair;

    //if light raise
    public bool LightRaise { get; private set; }

    public WEAPON NowWeapon { get; set; }

    void Start()
    {
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
            candle.gameObject.SetActive(_nowSelectInstance == candle && !_nowSelectInstance.active);
            lantern.gameObject.SetActive(_nowSelectInstance == lantern && !_nowSelectInstance.active);

            LightRaise = false;
            pair[_nowSelectInstance].Drop();

            WeaponStateUpdate();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            bool temp = _nowSelectInstance.active;
            _nowSelectInstance.SetActive(false);
            _nowSelectInstance = (_nowSelectInstance == candle) ? lantern : candle;
            _nowSelectInstance.SetActive(temp);

            //For now, anne drop light when switching weapon
            LightRaise = false;
            pair[_nowSelectInstance].Drop();

            WeaponStateUpdate();
        }
    }

    void WeaponStateUpdate()
    {
        NowWeapon = _nowSelectInstance == candle ? WEAPON.Candle : WEAPON.Lantern;
        NowWeapon = _nowSelectInstance.active ? NowWeapon : WEAPON.None;
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
