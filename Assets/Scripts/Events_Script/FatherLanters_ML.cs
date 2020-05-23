using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatherLanters_ML : MonoBehaviour
{
    [SerializeField] GameObject light;
    [SerializeField] GameObject father;
    [SerializeField] AudioClip sound;
    [SerializeField] AudioSource source;

    Father_Boss_Behaviour_ML fatherScript;

    private bool check;
    private bool candleCollisionCheck;
    private bool playerLanternBoss;

    // Start is called before the first frame update
    void Start()
    {
        fatherScript = father.GetComponent<Father_Boss_Behaviour_ML>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerLanternBoss && candleCollisionCheck && Input.GetButtonDown("Interaction") && !check)
        {
            light.SetActive(true);
            check = true;
            fatherScript.LanternCount++;

            if (fatherScript.LanternCount >= 4)
            {
                fatherScript.Death = true;
            }

            gameObject.GetComponent<AudioSource>().clip = sound;
            source.clip = sound;
            source.Play();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.tag == "Player_CandleCollider")
        {
            candleCollisionCheck = true;
        }
        if (collision.tag == "Player")
        {
            playerLanternBoss = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player_CandleCollider")
        {
            candleCollisionCheck = false;
        }
        if (collision.tag == "Player")
        {
            playerLanternBoss = false;
        }
    }
}
