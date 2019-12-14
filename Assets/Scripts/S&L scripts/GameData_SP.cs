using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using TMPro;


[System.Serializable]
public class GameData_SP
{
    public float candleRemaining;
    public float lanternRemaining;
    public float[] playerPosition;
    public int hp;
    public string lenguage;
    public string location;
    public string sceneName;
    public string checkpointName;

    public GameData_SP ( GameObject player,  InGameUI_SP candle, InGameUILantern_SP lantern, GameObject localizationManager, TextMeshProUGUI levelLocation, string scene, string checkpoint )
    {

        candleRemaining = candle.light;

        lanternRemaining = lantern.light;

        hp = player.GetComponent<PlayerStats_ML>().health;

        playerPosition = new float[3];

        playerPosition[0] = player.transform.position.x;
        playerPosition[1] = player.transform.position.y;
        playerPosition[2] = player.transform.position.z;

        lenguage = localizationManager.GetComponent<Localization>().ActiveLanguage;

        location = levelLocation.text;

        sceneName = scene;

        checkpointName = checkpoint;
    }

}
