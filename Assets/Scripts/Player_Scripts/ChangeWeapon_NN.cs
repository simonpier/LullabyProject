using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeapon_NN : MonoBehaviour
{
    private bool candleSwitch;
    [SerializeField] private GameObject candle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            candleSwitch = !candleSwitch;
            TurnLight();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            CheckAction();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            ChangeWeapon();
        }
    }
    private void TurnLight()
    {
        Debug.Log("Light");
        candle.SetActive(candleSwitch);
        GetComponent<Animator>().SetBool("Candle",candleSwitch);
    }
    public void CheckAction()
    {
        Debug.Log("Check");
        //mainSpriteRenderer.sprite = backSprite;
    }
    private void ChangeWeapon()
    {
        //Debug.Log(playerStatus.Health);
    }
}
