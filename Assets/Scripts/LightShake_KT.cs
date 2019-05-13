using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightShake_KT : MonoBehaviour
{
    //Brightness swing range
    [SerializeField, Header("Brightness Swing")] float swingRange;
    //Brightness swing speed
    [SerializeField] float swingSpeed;
    //The time until the light turns off
    [SerializeField, Header("Occasionally Turn Off")] float offTime;
    //The time until the light turns on full
    [SerializeField] float turnOnTime;
    //The time the light keeps turning off
    [SerializeField] float keepOffTime;
    //Random value related to keep off time
    [SerializeField, Range(0.0f, 1.0f)] float randomKeepOffTime;
    //Random value related to keep on time
    [SerializeField, Range(0.0f, 1.0f)] float randomKeepOnTime;

    Light spotLight;
    float _originRange;
    float _timer;
    float _randValueOff;

    void Start()
    {
        spotLight = GetComponent<Light>();
        _originRange = spotLight.range;
        _timer = offTime;
        _randValueOff = 1.0f;
    }
    
    void Update()
    {
        spotLight.range = _originRange * Mathf.Min(_timer / turnOnTime, 1.0f) + Mathf.Abs(Mathf.Sin(Time.time * swingSpeed)) * swingRange;

        _timer += Time.deltaTime;

        if (_timer > offTime * _randValueOff)
        {
            _timer -= offTime * _randValueOff;
            _timer -= keepOffTime * Random.Range(1.0f - randomKeepOffTime, 1.0f + randomKeepOffTime);
            _randValueOff = Random.Range(1.0f - randomKeepOnTime, 1.0f + randomKeepOnTime);
        }
    }
}