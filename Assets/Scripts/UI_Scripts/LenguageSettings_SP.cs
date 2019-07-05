using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using TMPro;

public class LenguageSettings_SP : MonoBehaviour
{

    [SerializeField] GameObject localization;
    Localization local;

    [SerializeField] GameObject PMItems; //Pause Menu Items button Text
    [SerializeField] GameObject PMMenu; //Pause Menu "back to Main Menu" button Text
    [SerializeField] GameObject PMQuit; //Pause Menu quit button Text

    [SerializeField] GameObject PMName; //Pause Menu Character name Text
    [SerializeField] GameObject PMHealth; //Pause Menu health Text
    [SerializeField] GameObject PMLocation; //Pause Menu Current Location Text
    [SerializeField] GameObject PMAge; //Pause Menu Age Text
    [SerializeField] GameObject IMReturn; //Inventory Menu Return button Text
    TextMeshProUGUI text;


    public void SetLanguageIta() //Italian Translation function
    {
        local = localization.GetComponent<Localization>();
        local.SetActiveLanguage("IT", true);

        #region Menu translation IT
        text = PMItems.GetComponent<TextMeshProUGUI>();
        text.SetText("Oggetti");

        text = PMMenu.GetComponent<TextMeshProUGUI>();
        text.SetText("Menu");

        text = PMQuit.GetComponent<TextMeshProUGUI>();
        text.SetText("Esci");

        text = PMName.GetComponent<TextMeshProUGUI>();
        text.SetText("Anne");

        text = PMHealth.GetComponent<TextMeshProUGUI>();
        text.SetText("Salute:");

        text = PMLocation.GetComponent<TextMeshProUGUI>();
        text.SetText("Zona attuale:");

        text = PMAge.GetComponent<TextMeshProUGUI>();
        text.SetText("Età:");

        text = IMReturn.GetComponent<TextMeshProUGUI>();
        text.SetText("Indietro");
        #endregion

    }

    public void SetLanguageEng()
    {
        local = localization.GetComponent<Localization>(); //English Translation function
        local.SetActiveLanguage("Standard", true);

        #region Menu translation ENG
        text = PMItems.GetComponent<TextMeshProUGUI>();
        text.SetText("Items");

        text = PMMenu.GetComponent<TextMeshProUGUI>();
        text.SetText("Menu");

        text = PMQuit.GetComponent<TextMeshProUGUI>();
        text.SetText("Quit");

        text = PMName.GetComponent<TextMeshProUGUI>();
        text.SetText("Anne");

        text = PMHealth.GetComponent<TextMeshProUGUI>();
        text.SetText("Health:");

        text = PMLocation.GetComponent<TextMeshProUGUI>();
        text.SetText("Current Location:");

        text = PMAge.GetComponent<TextMeshProUGUI>();
        text.SetText("Age:");

        text = IMReturn.GetComponent<TextMeshProUGUI>();
        text.SetText("Return");
        #endregion
    }

    public void SetLanguageJpn()
    {
        local = localization.GetComponent<Localization>(); //Japanese Translation function
        local.SetActiveLanguage("JP", true);

        #region Menu translation JNP
        text = PMItems.GetComponent<TextMeshProUGUI>();
        text.SetText("Items");

        text = PMMenu.GetComponent<TextMeshProUGUI>();
        text.SetText("Menu");

        text = PMQuit.GetComponent<TextMeshProUGUI>();
        text.SetText("Quit");

        text = PMName.GetComponent<TextMeshProUGUI>();
        text.SetText("Anne");

        text = PMHealth.GetComponent<TextMeshProUGUI>();
        text.SetText("Health:");

        text = PMLocation.GetComponent<TextMeshProUGUI>();
        text.SetText("Current Location:");

        text = PMAge.GetComponent<TextMeshProUGUI>();
        text.SetText("Age:");

        text = IMReturn.GetComponent<TextMeshProUGUI>();
        text.SetText("Return");
        #endregion
    }

}
