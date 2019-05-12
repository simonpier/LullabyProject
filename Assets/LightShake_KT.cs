using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightShake_KT : MonoBehaviour
{
    //
    [SerializeField] float swingRange;
    [SerializeField] float swingSpeed;
    Light spotLight;
    float _originRange;
    void Start()
    {
        spotLight = GetComponent<Light>();
        _originRange = spotLight.range;
    }

    void Update()
    {
        spotLight.range = _originRange + Mathf.Abs(Mathf.Sin(Time.time * swingSpeed)) * swingRange;
    }
}
