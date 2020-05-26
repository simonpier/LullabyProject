using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using TMPro;

public class TranslationOBJ_Script_SP : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI itemsTxt, backTxt, exitTxt, anneTxt, ageTxt, hpTxt, currentLocationTxt, floorTxt, item1Txt, item2Txt, itemTitleTxt, returnTxt, retryTxt, backToTitleGOTxt,
                                     confTxt1, yesTxt1, noTxt1, confTxt2, yesTxt2, noTxt2;
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
            confTxt1.text = "あなたは戻りたい?";
            yesTxt1.text = "はい";
            noTxt1.text = "いいえ";
            confTxt2.text = "ゲームを終了しますか?";
            yesTxt2.text = "はい";
            noTxt2.text = "いいえ";
        }

        else if (leng2 == "IT")
        {
            itemsTxt.text = "Oggetti";
            backTxt.text = "Torna al menu";
            exitTxt.text = "Chiudi gioco";
            anneTxt.text = "Anne";
            ageTxt.text = "Eta': 5 anni";
            hpTxt.text = "PF";
            currentLocationTxt.text = "Posizione attuale: ";
            floorTxt.text = "3 piano";
            item1Txt.text = "oggetto 1";
            item2Txt.text = "oggetto 2";
            itemTitleTxt.text = "Oggetti";
            returnTxt.text = "Indietro";
            retryTxt.text = "Riprova";
            backToTitleGOTxt.text = "Torna al menu";
            confTxt1.text = "vuoi tornare al menu?";
            yesTxt1.text = "Si";
            noTxt1.text = "No";
            confTxt2.text = "vuoi uscire dal gioco?";
            yesTxt2.text = "Si";
            noTxt2.text = "No";
        }
    }
}
