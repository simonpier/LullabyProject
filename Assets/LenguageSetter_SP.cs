using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using TMPro;
using UnityEngine.SceneManagement;

public class LenguageSetter_SP : MonoBehaviour
{

    public string leng;
    [SerializeField] GameObject localizationManager;
    // Start is called before the first frame update
    void Start()
    {
        leng = localizationManager.GetComponent<Localization>().ActiveLanguage;
        leng = GameObject.Find("LanguageContainer").GetComponent<LanguageContainer_SP>().language;   
        //Debug.Log(data.checkpointName);
        //leng = data.lenguage;
    }

    private void Awake()
    {
        Invoke("LoadLenguage", 0f);

    }

    private void LoadLenguage()
    {

        GameData_SP data = SaveSystem_SP.loadPlayer();
        //leng = data.lenguage;

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(leng);
    }
}
