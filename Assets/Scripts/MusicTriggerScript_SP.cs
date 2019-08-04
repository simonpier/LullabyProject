using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTriggerScript_SP : MonoBehaviour
{
    public static MusicTriggerScript_SP instance;

    [SerializeField] GameObject player;
    [SerializeField] GameObject gameCamera;
    //[SerializeField] GameObject otherTrigger;

    [SerializeField] AudioClip song;

    AudioSource cameraSource;
    //MusicTriggerScript_SP otherScript;

    public bool passed = false;
    

    private void Start()
    {
        instance = this;
        cameraSource = gameCamera.GetComponent<AudioSource>();
        
         
    }

    void OnTriggerStay2D(Collider2D other)
    {
        
        if ((other.gameObject.tag == "Player") && Input.GetButtonDown("Interaction"))
        {
            Debug.Log("change");
            cameraSource.clip = song;
            cameraSource.volume = 0.5f;
            cameraSource.Play();

        }
    }
}
