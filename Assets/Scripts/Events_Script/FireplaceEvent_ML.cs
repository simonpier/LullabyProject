using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireplaceEvent_ML : MonoBehaviour
{
    [SerializeField] GameObject snakeCat;
    [SerializeField] GameObject firePlace;

    Animator anim;
    SnakeBossFightTrigger_ML snakeTrigger;

    private bool activationCheck;
    private bool litCheck;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        snakeTrigger = snakeCat.GetComponent<SnakeBossFightTrigger_ML>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !activationCheck && Input.GetButtonDown("Interaction"))
        {
            anim.SetBool("activation", true);
            activationCheck = true;
        }

        if(collision.tag == "Player" && !litCheck && snakeTrigger.WoodPiecesCount >= 3 && Input.GetButtonDown("Interaction"))
        {
            litCheck = true;
            anim.SetBool("catBoss", true);
        }
    }

    private void CatAppear()
    {
        snakeCat.SetActive(true);
        firePlace.SetActive(true);
        Destroy(gameObject);
    }
}
