using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class SaveManager_SP : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] InGameUI_SP candle;
    [SerializeField] InGameUILantern_SP lantern;
    [SerializeField] GameObject localizationManager;
    [SerializeField] PauseMenu_SP pause;

    Vector3 playerTrasform;
    string leng;

    private void Start()
    {

        playerTrasform = player.GetComponent<Transform>().position;
        leng = localizationManager.GetComponent<Localization>().ActiveLanguage;
    }

    public void SaveGame()
    {

        SaveSystem_SP.SavePlayer(player, candle, lantern, localizationManager);
        Debug.Log(Application.persistentDataPath);
    }

    public void LoadGame()
    {
        pause.Resume();

        GameData_SP data = SaveSystem_SP.loadPlayer();

        player.GetComponent<PlayerStats_ML>().health = data.hp;

        playerTrasform.x = data.playerPosition[0];
        playerTrasform.y = data.playerPosition[1];
        playerTrasform.z = data.playerPosition[2];

        leng = data.lenguage;

        candle.light = data.candleRemaining;
        lantern.light = data.lanternRemaining;
    }
}
