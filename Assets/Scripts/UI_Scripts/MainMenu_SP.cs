using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu_SP : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI location;
    private string scene;

    private void Start()
    {
        GameData_SP data = SaveSystem_SP.loadPlayer();
        scene = data.sceneName;
        location.text = data.location;
        Debug.Log(scene);
        
    }

    public void PlayGame()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void QuitGame()
    {

        Application.Quit();

    }

    public void load()
    {

        

        SceneManager.LoadScene(scene);

    }

}
