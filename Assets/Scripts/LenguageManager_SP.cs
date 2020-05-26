using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class LenguageManager_SP : MonoBehaviour
{

    [SerializeField] GameObject localization;
    [SerializeField] GameObject engSelection;
    [SerializeField] GameObject jpnSelection;
    [SerializeField] GameObject ItaSelection;


    Localization locScript;

    public static string leng = "standard";

    private bool isEng, isJP, isIta;

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
            ItaSelection.SetActive(false);
            isJP = false;
            isEng = true;
            isIta = false;
        }
        else if (leng == "JP" && !isJP)
        {
            engSelection.SetActive(false);
            jpnSelection.SetActive(true);
            ItaSelection.SetActive(false);
            isJP = true;
            isEng = false;
            isIta = false;
        }
        else if (leng == "IT" && !isJP)
        {
            engSelection.SetActive(false);
            jpnSelection.SetActive(false);
            ItaSelection.SetActive(true);
            isJP = false;
            isEng = false;
            isIta = true;
        }
    }

    public void SetJpn()
    {

        leng = "JP";
        engSelection.SetActive(false);
        jpnSelection.SetActive(true);
        ItaSelection.SetActive(false);
        Debug.Log("lenguage setted to japan");


    }

    public void setEng()
    {

        leng = "standard";
        engSelection.SetActive(true);
        jpnSelection.SetActive(false);
        ItaSelection.SetActive(false);
        Debug.Log("lenguage setted to English");

    }

    public void setIta()
    {
        leng = "IT";
        engSelection.SetActive(false);
        jpnSelection.SetActive(false);
        ItaSelection.SetActive(true);
        Debug.Log("lenguage setted to ita");
    }
}
