using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageTeleporter_SP : MonoBehaviour
{

    public void Stage1Teleport()
    {

        Time.timeScale = 1f;
        SceneManager.LoadScene(1);

    }

    public void Stage2Teleport()
    {

        Time.timeScale = 1f;
        SceneManager.LoadScene(2);

    }

    public void Stage3Teleport()
    {

        Time.timeScale = 1f;
        SceneManager.LoadScene(3);

    }
}
