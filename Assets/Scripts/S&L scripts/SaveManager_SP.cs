using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;

public class SaveManager_SP : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] InGameUI_SP candle;
    [SerializeField] InGameUILantern_SP lantern;
    [SerializeField] GameObject localizationManager;
    [SerializeField] PauseMenu_SP pause;
    [SerializeField] TextMeshProUGUI levelLocation;
    [SerializeField] GameObject gameCamera;

    PositionConstraint cameraCon;

    Vector3 playerTrasform;
    string leng, leng2;
    string scene;

    private void Start()
    {
        cameraCon = gameCamera.GetComponent<PositionConstraint>();
        scene = SceneManager.GetActiveScene().name;
        leng = localizationManager.GetComponent<Localization>().ActiveLanguage;
        leng2 = localizationManager.GetComponent<Localization>().ActiveLanguage;
    }

    public void SaveGame()
    {

        //SaveSystem_SP.SavePlayer(player, candle, lantern, localizationManager, levelLocation, scene);
        Debug.Log(Application.persistentDataPath);
    }

    public void LoadGame()
    {

        GameData_SP data = SaveSystem_SP.loadPlayer();
        Debug.Log(data.checkpointName);

        player.GetComponent<PlayerStats_ML>().health = data.hp;

        player.GetComponent<Transform>().position = new Vector3(data.playerPosition[0], data.playerPosition[1], data.playerPosition[2]);

        var itr = GameObject.Find(data.checkpointName).transform;
        while (itr != null)
        {
            if (itr.name.Contains("Env")) break;
            itr = itr.parent;
        }
        if (!itr) Debug.LogError("Cant find object named \"Enviroment\"");
        player.GetComponent<PlayerMove_KT>().CheckRoomSize(itr.parent.gameObject);
        Invoke("CameraConstraints", 0.5f);

        //player.GetComponent<Transform>().position = new Vector3(data.playerPosition[0], data.playerPosition[1], data.playerPosition[2]);

        leng = data.lenguage;
        

        candle.light = data.candleRemaining;
        lantern.light = data.lanternRemaining;

        Debug.Log(data.playerPosition[0] + " " + data.playerPosition[1] + " " + data.playerPosition[2]);
    }

    private void Awake()
    {
        Invoke("LoadGame", 0f);
        Debug.Log(leng2);
    }

    void CameraConstraints()
    {
        cameraCon.enabled = true;
    }

    [SerializeField] TextMeshProUGUI itemsTxt, backTxt, exitTxt, anneTxt, ageTxt, hpTxt, currentLocationTxt, floorTxt, item1Txt, item2Txt, itemTitleTxt, returnTxt;

    void Update()
    {
        leng2 = localizationManager.GetComponent<Localization>().ActiveLanguage;
        Debug.Log(leng2);
        if (leng2 == "JP")
        {
            Debug.Log(leng2);
            itemsTxt.text = "持ち物";
            backTxt.text = "タイトルへ戻る";
            exitTxt.text = "やめる";
            anneTxt.text = "アン";
            ageTxt.text = "年齢：５歳";
            hpTxt.text = "体力";
            currentLocationTxt.text = "現在地";
            floorTxt.text = "３階";
            item1Txt.text = "なし";
            item2Txt.text = "あり";
            itemTitleTxt.text = "なし";
            returnTxt.text = "言語";

        }
    }
}
