using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using TMPro;

public class TranslationOBJ_Script_SP : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI itemsTxt, backTxt, exitTxt, anneTxt, ageTxt, hpTxt, currentLocationTxt, floorTxt, item1Txt, item2Txt, itemTitleTxt, returnTxt, retryTxt, backToTitleGOTxt;
    [SerializeField] GameObject localizationManager;
    string leng2;

    private void Start()
    {
        leng2 = localizationManager.GetComponent<Localization>().ActiveLanguage;
    }

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
            ageTxt.text = "年齢: 5歳";
            hpTxt.text = "体力";
            currentLocationTxt.text = "現在地";
            floorTxt.text = "3階";
            item1Txt.text = "なし";
            item2Txt.text = "あり";
            itemTitleTxt.text = "なし";
            returnTxt.text = "言語";
            retryTxt.text = "リトライ";
            backToTitleGOTxt.text = "タイトルへ戻る";
        }
    }
}
