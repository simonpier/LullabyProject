using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;

public static class SaveSystem_SP
{
    

    public static void SavePlayer (GameObject player, InGameUI_SP candle, InGameUILantern_SP lantern, GameObject localizationManager, TextMeshProUGUI levelLocation, string scene, string checkpoint , float volume)
    {

        BinaryFormatter formater = new BinaryFormatter();

        string path = Application.persistentDataPath + "/Lullaby.bieta";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData_SP data = new GameData_SP(player, candle, lantern, localizationManager, levelLocation, scene, checkpoint);

        formater.Serialize(stream, data);
        stream.Close();
    }


    public static GameData_SP loadPlayer()
    {

        string path = Application.persistentDataPath + "/Lullaby.bieta";

        if (File.Exists(path))
        {

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData_SP data = formatter.Deserialize(stream) as GameData_SP;
            stream.Close();


            return data;
        }

        else
        {

            Debug.LogError("Save file not found in " + path);
            return null;

        }
    }


}
