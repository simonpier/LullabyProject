using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleLight_KT : MonoBehaviour
{
    //player attack range and first light range
    [Header("Lighting and Collider"), Range(0.001f, 100)]
    public float colliderRange;

    //the range between first light range and second light range
    [Header("Lighting only"), Range(0.001f, 10)]
    public float bufferRange;
    //the range outside collider range
    [Range(0.001f, 100)]
    public float attenuationRange;

    //under here, the member variable same as normal light 
    public Color lightColor;
    [Range(0.001f, 100)]
    public float lightIntencsity;


    CircleCollider2D _collider;
    //tentative light created PointLight
    Light _light;

    void Awake()
    {
        _collider = GetComponent<CircleCollider2D>();
        _light = GetComponent<Light>();
    }

    void Start()
    {
        SetParameter();
    }

    void OnEnable()
    {
        SetParameter();
    }
    
    void Update()
    {
        
    }

    private void SetParameter()
    {
        _collider.radius = colliderRange;
        _light.intensity = lightIntencsity;
        _light.color = lightColor;
        //tentative magic number 5.0f
        _light.range = 5.0f * colliderRange;
    }
}
