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

    GameObject _nowSelectInstance;

    //if light raise
    public bool LightRaise { get; private set; }

    public WEAPON NowWeapon { get; set; }

    void Start()
    {
        _nowSelectInstance = candle;
        NowWeapon = WEAPON.None;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            LightRaise = NowWeapon != WEAPON.None && !LightRaise;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            candle.gameObject.SetActive(_nowSelectInstance == candle && !_nowSelectInstance.active);
            lantern.gameObject.SetActive(_nowSelectInstance == lantern && !_nowSelectInstance.active);

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

            WeaponStateUpdate();
        }
    }

    void WeaponStateUpdate()
    {
        NowWeapon = _nowSelectInstance == candle ? WEAPON.Candle : WEAPON.Lantern;
        NowWeapon = _nowSelectInstance.active ? NowWeapon : WEAPON.None;
    }
}
