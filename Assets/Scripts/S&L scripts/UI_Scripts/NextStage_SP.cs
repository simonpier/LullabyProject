using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class NextStage_SP : MonoBehaviour
{

    void NextStage()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        File.Delete(Application.persistentDataPath + "/Lullaby.bieta");
    }

}
