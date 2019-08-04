using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicStopperScript_SP : MonoBehaviour
{
    public static MusicStopperScript_SP instance;

    [SerializeField] GameObject player;
    [SerializeField] GameObject gameCamera;
    //[SerializeField] GameObject otherTrigger;


    AudioSource cameraSource;
    //MusicTriggerScript_SP otherScript;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        cameraSource = gameCamera.GetComponent<AudioSource>();
    }

    void OnTriggerStay2D(Collider2D other)
    {

        if ((other.gameObject.tag == "Player") && Input.GetButtonDown("Interaction"))
        {

            cameraSource.Stop();

        }
    }
}
