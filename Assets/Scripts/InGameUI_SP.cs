using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI_SP : MonoBehaviour
{
    

    [SerializeField] private float fillamount;

    [SerializeField] private Image content;

    private const float coef = 2f; //amount of light loosing per second
    private float light = 100.0f; //max light 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Handlebar();
        
    }

    private void Handlebar()
    {
        light -= coef * Time.deltaTime;
        content.fillAmount = Map( light, 0, 100, 0, 1);

    }

    private float Map(float value , float inMin, float inMax, float outMin, float outMax )
    {

        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;

    }
}
