using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;


public class LenguageSettings_SP : MonoBehaviour
{

    [SerializeField] GameObject localization;
    Localization local;

    public void SetLanguageIta()
    {
        local = localization.GetComponent<Localization>();
        local.SetActiveLanguage("IT", true);

    }

    public void SetLanguageEng()
    {
        local = localization.GetComponent<Localization>();
        local.SetActiveLanguage("Standard", true);

    }

    public void SetLanguageJpn()
    {
        local = localization.GetComponent<Localization>();
        local.SetActiveLanguage("JP", true);

    }

}
