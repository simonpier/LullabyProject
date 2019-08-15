using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoManager_SP : MonoBehaviour
{
    [SerializeField] VideoPlayer video;
    [SerializeField] bool EndingCutScene = false;

    void Start()
    {
    }

    
    void Update()
    {
        if (Input.GetButtonDown("Interaction") || Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return))
        {

            if (EndingCutScene) SceneManager.LoadScene(2);
            else SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }
        LoadScene();
    }

    void LoadScene()
    {
        if (!video.isPlaying && video.isPrepared)
        {
            if (EndingCutScene) SceneManager.LoadScene(2);
            else SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
