using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoManager_SP : MonoBehaviour
{
    [SerializeField] VideoPlayer video;
    

    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetButtonDown("Interaction") || Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return))
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }

        if (!video.isPlaying )
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
