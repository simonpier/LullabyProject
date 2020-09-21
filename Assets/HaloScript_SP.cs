using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaloScript_SP : MonoBehaviour
{
    [SerializeField] GameObject halo;
    [SerializeField] GameObject player;

    private bool check, pressed;

    // Start is called before the first frame update
    void Start()
    {
        check = false;
        pressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interaction") && check == true)
        {

            pressed = true;

        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !check)
        {
            check = true;
        }

        if (!check && pressed == false && collision.tag == "Player_CandleCollider")
        {
            halo.SetActive(true);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ( collision.tag == "Player_CandleCollider" )
        {
            halo.SetActive(false);
        }

        if (collision.tag == "Player")
        {
            check = false;
        }
    }
}
