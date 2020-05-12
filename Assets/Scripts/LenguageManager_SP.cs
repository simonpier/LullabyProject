using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class LenguageManager_SP : MonoBehaviour
{

    [SerializeField] GameObject localization;
    [SerializeField] GameObject engSelection;
    [SerializeField] GameObject jpnSelection;

    Localization locScript;

    public static string leng = "standard";

    private bool isEng, isJP;

    private void Start()
    {
        locScript = localization.GetComponent<Localization>();
        locScript.SetActiveLanguage(leng, true);
    }

    private void Update()
    {
        locScript.SetActiveLanguage(leng, true);

        if (leng == "standard" && !isEng)
        {
            engSelection.SetActive(true);
            jpnSelection.SetActive(false);
            isJP = false;
            isEng = true;
        }
        else if (leng == "JP" && !isJP)
        {
            engSelection.SetActive(false);
            jpnSelection.SetActive(true);
            isJP = true;
            isEng = false;
        }
    }

    public void SetJpn()
    {

        leng = "JP";
        engSelection.SetActive(false);
        jpnSelection.SetActive(true);
        Debug.Log("lenguage setted to japan");
        

    }
    
    public void setEng()
    {

        leng = "standard";
        engSelection.SetActive(true);
        jpnSelection.SetActive(false);
        Debug.Log("lenguage setted to English");
        
    }
    public void setIta()
    {
        leng = "IT";
        Debug.Log("lenguage setted to ita");
    }
}
