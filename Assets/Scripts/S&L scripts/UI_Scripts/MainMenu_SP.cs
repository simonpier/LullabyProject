using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.IO;

public class MainMenu_SP : MonoBehaviour
{
    [SerializeField] Button continueButton;

    [SerializeField] TextMeshProUGUI location;
    private string scene;

    private void Start()
    {
        GameData_SP data = SaveSystem_SP.loadPlayer();
        scene = data.sceneName;
        location.text = data.location;
        Debug.Log(scene);
    }

    private void Update()
    {
        
        if (!File.Exists(Application.persistentDataPath + "/Lullaby.bieta"))
        {

            continueButton.interactable = false;
            Debug.LogError("Save file not found in " + Application.persistentDataPath + "/Lullaby.bieta");

        }

        else
        {
            continueButton.interactable = true;
        }
    }

    public void PlayGame()
    {
        File.Delete(Application.persistentDataPath + "/Lullaby.bieta");
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
