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

    [SerializeField]
    TextMeshProUGUI newGameTxt, continueTxt, optionsTxt, exitTxt, creditsTxt, optionsTitleTxt, volumeTxt, onTxt, onTxt2, offTxt, offTxt2, musicTxt, languageTxt, controlsTxt, backTxt,
                    wTxt, aTxt, sTxt, dTxt, spaceTxt, shiftTxt, escTxt, eTxt, sureTxt, advertiseTxt, yesTxt, noTxt;

    [SerializeField] GameObject newTab;

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

    public void PlayGameNoSave()
    {
        if (!File.Exists(Application.persistentDataPath + "/Lullaby.bieta"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        else
        {
            this.GetComponent<SwitchCanvas_SP>().Switch();
            newTab.SetActive(true);
            
        }

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
        wTxt.text = "ろうそくを高い位置で照らす";
        aTxt.text = "左に進む";
        sTxt.text = "ろうそくを低い位置で照らす";
        dTxt.text = "右に進む";
        spaceTxt.text = "ろうそく/懐中電灯を使う";
        shiftTxt.text = "ろうそくと懐中電灯を持ち替える";
        escTxt.text = "一時停止";
        eTxt.text = "調べる";
        sureTxt.text = "本気ですか？";
        advertiseTxt.text = "(これは古い保存ファイルを削除します)";
        yesTxt.text = "はい";
        noTxt.text = "いいえ";

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
        wTxt.text = "Hold candle at high position";
        aTxt.text = "Move Left";
        sTxt.text = "Hold candle at lower position";
        dTxt.text = "Move Right";
        spaceTxt.text = "Use candle/flashlight";
        shiftTxt.text = "Switch candle/flashlight";
        escTxt.text = "Pause";
        eTxt.text = "Check";
        sureTxt.text = "Are you sure?";
        advertiseTxt.text = "(This will delete old save files)";
        yesTxt.text = "Yes";
        noTxt.text = "No";
    }

    public void ChangeLenguageIta()
    {
        newGameTxt.text = "Nuovo Gioco";
        continueTxt.text = "continua";
        optionsTxt.text = "Opzioni";
        exitTxt.text = "Chiudi gioco";
        creditsTxt.text = "Crediti";
        optionsTitleTxt.text = "Opzioni";
        volumeTxt.text = "Volume";
        onTxt.text = "on";
        offTxt.text = "off";
        onTxt2.text = "on";
        offTxt2.text = "off";
        languageTxt.text = "Linguaggi";
        controlsTxt.text = "Controlli";
        backTxt.text = "Indietro";
        wTxt.text = "Punta lanterna verso l'alto";
        aTxt.text = "Sinistra";
        sTxt.text = "Punta lanterna verso terra";
        dTxt.text = "Destra";
        spaceTxt.text = "Usa candela/lanterna";
        shiftTxt.text = "Cambia tra candela/lanterna";
        escTxt.text = "Pausa";
        eTxt.text = "Interagisci";
        sureTxt.text = "Sei Sicuro?";
        advertiseTxt.text = "(Così i vecchi salvataggi verranno cancellati)";
        yesTxt.text = "Si";
        noTxt.text = "No";
    }
}
