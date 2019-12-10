using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[System.Serializable]
public class GameData_SP
{
    public float candleRemaining;
    public float lanternRemaining;
    public float[] playerPosition;
    public int hp;
    public string lenguage;

    public GameData_SP ( GameObject player,  InGameUI_SP candle, InGameUILantern_SP lantern, GameObject localizationManager)
    {

        candleRemaining = candle.light;

        lanternRemaining = lantern.light;

        hp = player.GetComponent<PlayerStats_ML>().health;

        playerPosition = new float[3];

        playerPosition[0] = player.transform.position.x;
        playerPosition[1] = player.transform.position.y;
        playerPosition[2] = player.transform.position.z;

        lenguage = localizationManager.GetComponent<Localization>().ActiveLanguage;

    }

}
