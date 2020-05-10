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

    [SerializeField] TextMeshProUGUI newGameTxt, continueTxt, optionsTxt, exitTxt, creditsTxt, optionsTitleTxt, volumeTxt, onTxt, onTxt2, offTxt, offTxt2, musicTxt, languageTxt, controlsTxt, backTxt;

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

    public void ChangeLenguageJpn()
    {
        newGameTxt.text = "はじめから";
        continueTxt.text = "つづきから";
        optionsTxt.text = "設定";
        exitTxt.text = "やめる";
        creditsTxt.text = "製作者一覧";
        optionsTitleTxt.text = "設定";
        volumeTxt.text = "音量";
        onTxt.text = "あり";
        offTxt.text = "なし";
        onTxt2.text = "あり";
        offTxt2.text = "なし";
        languageTxt.text = "言語";
        controlsTxt.text = "操作方法";
        backTxt.text = "戻る";
        
    }

    public void ChangeLenguageEng()
    {

        newGameTxt.text = "New game";
        continueTxt.text = "Continue";
        optionsTxt.text = "Options";
        exitTxt.text = "Exit";
        creditsTxt.text = "Credits";
        optionsTitleTxt.text = "Options";
        volumeTxt.text = "Volume";
        onTxt.text = "on";
        offTxt.text = "off";
        onTxt2.text = "on";
        offTxt2.text = "off";
        languageTxt.text = "Language";
        controlsTxt.text = "Controls";
        backTxt.text = "Back";

    }
}
