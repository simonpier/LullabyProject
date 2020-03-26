using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class LenguageManager_SP : MonoBehaviour
{

    [SerializeField] GameObject localization;

    Localization locScript;

    public static string leng = "standard";

    private void Start()
    {
        locScript = localization.GetComponent<Localization>();
        locScript.SetActiveLanguage(leng, true);
    }

    private void Update()
    {
        locScript.SetActiveLanguage(leng, true);
    }

    public void SetJpn()
    {

        leng = "JP";
        Debug.Log("lenguage setted to japan");
        

    }
    
    public void setEng()
    {

        leng = "standard";
        Debug.Log("lenguage setted to English");
        
    }
    public void setIta()
    {
        leng = "IT";
        Debug.Log("lenguage setted to ita");
    }
}
