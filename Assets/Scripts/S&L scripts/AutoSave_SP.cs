using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Fungus;

public class AutoSave_SP : MonoBehaviour
{
    [SerializeField] GameObject localizationManager;
    [SerializeField] TextMeshProUGUI levelLocation;
    [SerializeField] GameObject playerObject;
    [SerializeField] InGameUI_SP candleScript;
    [SerializeField] InGameUILantern_SP lanternScript;

    private string scene;
    private string leng;

    private void Start()
    {
        scene = SceneManager.GetActiveScene().name;
        leng = localizationManager.GetComponent<Localization>().ActiveLanguage;
    }

    private void Awake()
    {
        scene = SceneManager.GetActiveScene().name;
        Debug.Log(playerObject + " " + candleScript + " " + lanternScript + " " + localizationManager + " " + levelLocation + " " + scene);
        SaveSystem_SP.SavePlayer(playerObject, candleScript, lanternScript, localizationManager, levelLocation, scene, "", AudioListener.volume);
    }
}
