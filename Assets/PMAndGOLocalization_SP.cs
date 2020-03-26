using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Fungus;

public class PMAndGOLocalization_SP : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI itemsTxt, backTxt, exitTxt, anneTxt, ageTxt, hpTxt, currentLocationTxt, floorTxt, item1Txt, item2Txt, itemTitleTxt, returnTxt;

    [SerializeField] GameObject localization;

    Localization locScript;
    public static string leng;
    // Start is called before the first frame update
    void Start()
    {
        locScript = localization.GetComponent<Localization>();
        leng = localization.GetComponent<Localization>().ActiveLanguage;
    }

    // Update is called once per frame
    void Update()
    {
        if (leng == "JP" )
        {
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
