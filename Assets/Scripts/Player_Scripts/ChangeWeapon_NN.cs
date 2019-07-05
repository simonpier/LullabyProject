using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeapon_NN : MonoBehaviour
{
    [SerializeField] private GameObject candle;
    [SerializeField] private GameObject lantern;

    GameObject _nowWeapon;

    void Start()
    {
        _nowWeapon = candle;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            candle.gameObject.SetActive(_nowWeapon == candle && !_nowWeapon.active);
            lantern.gameObject.SetActive(_nowWeapon == lantern && !_nowWeapon.active);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            bool temp = _nowWeapon.active;
            _nowWeapon.SetActive(false);
            _nowWeapon = (_nowWeapon == candle) ? lantern : candle;
            _nowWeapon.SetActive(temp);
        }
    }
}
