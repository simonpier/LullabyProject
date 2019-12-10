using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject candlebar;

    private float candleAmount;

    public void SaveGame()
    {

        //SaveSystem_SP.SavePlayer(player , );

    }

    public void LoadGame()
    {

        GameData_SP data = SaveSystem_SP.loadPlayer();

        

    }
}
