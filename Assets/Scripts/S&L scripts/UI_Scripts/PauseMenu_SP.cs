using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu_SP : MonoBehaviour
{
    public static bool GameIsPaused = false;

    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] GameObject inventoryMenuUI;
    [SerializeField] GameObject firstObject;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Menu"))
        {

            if (GameIsPaused == true)
            {

                Resume();

            }

            else
            {

                Pause();

            }

        }
    }


    public void Resume()
    {

        pauseMenuUI.SetActive(false);
        inventoryMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        

    }

    public void Pause()
    {

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(firstObject, null);
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(2);

    }

    public void QuitGame()
    {

        Application.Quit();

    }
}
