using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using TMPro;

public class SubtitlesScript2_SP : MonoBehaviour
{

    [SerializeField] public TextMeshProUGUI subTitleTxt;
    [SerializeField] GameObject localizationManager;

    private string leng;

    // Start is called before the first frame update
    void Start()
    {
        leng = GameObject.Find("LanguageContainer").GetComponent<LanguageContainer_SP>().language;

        StartCoroutine(text1(11.50f));


        StartCoroutine(text2(15.50f));


        StartCoroutine(text3(21.50f));


        StartCoroutine(text4(23.48f));


        StartCoroutine(text5(25.54f));


        StartCoroutine(text6(28.61f));


        StartCoroutine(text7(34.54f));


        StartCoroutine(text8(38.64f));


        StartCoroutine(text9(43.60f));

    }

    // Update is called once per frame
    void Update()
    {
        leng = localizationManager.GetComponent<Localization>().ActiveLanguage;
    }

    private void Awake()
    {



    }


    private void ChangeText(string text)
    {

        subTitleTxt.text = text;
        Debug.Log(subTitleTxt.text);
    }

    private IEnumerator text1(float waitTime)
    {

        if (leng == "IT")
        {
            yield return new WaitForSeconds(waitTime);
            ChangeText("Dopo tutto, forse nesuno ti vuole bene");
        }

        else if (leng == "JP")
        {
            yield return new WaitForSeconds(waitTime);
            ChangeText("もう誰もあなたのことを\n 愛していない.");
        }

        StartCoroutine(pause(3f));
    }

    private IEnumerator text2(float waitTime)
    {

        if (leng == "IT")
        {
            yield return new WaitForSeconds(waitTime);
            ChangeText("Sono la voce misterios anella tua testa. La tua meravigliosa sorellina. La tua mata bambola.");
        }
        else if (leng == "JP")
        {
            yield return new WaitForSeconds(waitTime);
            ChangeText("私はあなたが作り出した 不思議な存在.\nあなたの最高の姉妹にして 最愛のお人形さん.");
        }
        
    }

    private IEnumerator text3(float waitTime)
    {

        if (leng == "IT")
        {
            yield return new WaitForSeconds(waitTime);
            ChangeText("Asuka");

        }
        else if (leng == "JP")
        {
            yield return new WaitForSeconds(waitTime);
            ChangeText("アスカ");
        }
        
    }

    private IEnumerator text4(float waitTime)
    {

        if (leng == "IT")
        {
            yield return new WaitForSeconds(waitTime);
            ChangeText("Non mi piaci più, Anne.");

        }
        else if (leng == "JP")
        {
            yield return new WaitForSeconds(waitTime);
            ChangeText("でももうあなたのことは 好きじゃないの\n アン.");
        }
        
    }

    private IEnumerator text5(float waitTime)
    {

        if (leng == "IT")
        {
            yield return new WaitForSeconds(waitTime);
            ChangeText("E inoltre, ho sempre detestato dormire insieme a te.");
        }
        else if (leng == "JP")
        {
            yield return new WaitForSeconds(waitTime);
            ChangeText("そもそもあなたと一緒に寝たいと\n 思ったことなんて一度もないわ.");
        }
        
    }

    private IEnumerator text6(float waitTime)
    {

        if (leng == "IT")
        {
            yield return new WaitForSeconds(waitTime);
            ChangeText("Oh, ma tu non crescerai mai, no? Un'amica infantile... non è popolare!");
        }
        else if (leng == "JP")
        {
            yield return new WaitForSeconds(waitTime);
            ChangeText("はぁ、あなたは成長しないわね\n そう思わない? いつまでも子供で...そんなの流行らないわよ!");
        }
        
    }

    private IEnumerator text7(float waitTime)
    {

        if (leng == "IT")
        {
            yield return new WaitForSeconds(waitTime);
            ChangeText("I miei nuovi amici sono molto più eccezionali rispetto a te.");
        }
        else if (leng == "JP")
        {
            yield return new WaitForSeconds(waitTime);
            ChangeText("私の新しい友達のほうが\n あなたより何倍もマシだわ.");
        }
        
    }

    private IEnumerator text8(float waitTime)
    {

        if (leng == "IT")
        {
            yield return new WaitForSeconds(waitTime);
            ChangeText("Oh, povera Anne. Forse ti lascerò essere nostra amica.");
        }
        else if (leng == "JP")
        {
            yield return new WaitForSeconds(waitTime);
            ChangeText("まぁ 可愛そうなアン. そうね\n 私達の友達に入れてあげてもいいわ.");
        }
        
    }

    private IEnumerator text9(float waitTime)
    {
        if (leng == "IT")
        {
            yield return new WaitForSeconds(waitTime);
            ChangeText("Abbiamo bisogno di... qualcuno da prendere in giro.");
        }
        else if (leng == "JP")
        {
            yield return new WaitForSeconds(waitTime);
            ChangeText("ちょうど いじめがいのあるオトモダチがほしかったところなの.");
        }
        
    }

    private IEnumerator text10(float waitTime)
    {
        if (leng == "IT")
        {
            yield return new WaitForSeconds(waitTime);
            ChangeText("");
        }
        else if (leng == "JP")
        {
            yield return new WaitForSeconds(waitTime);
            ChangeText("");
        }
        
    }


    private IEnumerator pause(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        ChangeText("");
    }
}