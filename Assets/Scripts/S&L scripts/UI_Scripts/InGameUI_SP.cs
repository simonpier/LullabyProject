using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI_SP : MonoBehaviour
{
    

    [SerializeField] private float fillamount;

    [SerializeField] private Image content;
    [SerializeField] public bool candleOn_Off = true;

    [SerializeField] GameObject candle;
    [SerializeField] GameObject lantern;
    private const float coef = 1f; //amount of light loosing per second
    public float light = 100.0f; //max light 
    private bool check = true;

    public float Light { get => light; set => light = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (candle.activeSelf == true || lantern.activeSelf == true)
        {

            if (candle.activeSelf == true)
            {

                Handlebar();

            }

            if (content.fillAmount == 0)
            {

                CandleOff();

            }
        }
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

    bool CandleOff()
    {

        candleOn_Off = false;
        return candleOn_Off;

    }
}
