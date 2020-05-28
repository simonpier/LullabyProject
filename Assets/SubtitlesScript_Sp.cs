using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using TMPro;

public class SubtitlesScript_Sp : MonoBehaviour
{

    [SerializeField] public TextMeshProUGUI subTitleTxt;
    [SerializeField] GameObject localizationManager;

    private string leng;

    // Start is called before the first frame update
    void Start()
    {
        leng = GameObject.Find("LanguageContainer").GetComponent<LanguageContainer_SP>().language;

        StartCoroutine(text1(11.21f));

        
        StartCoroutine(text2(28.63f));

        
        StartCoroutine(text3(40.10f));

        
        StartCoroutine(text4(50.09f));

        
        StartCoroutine(text5(51.27f));

        
        StartCoroutine(text6(63.24f));

        
        StartCoroutine(text7(67.80f));

        
        StartCoroutine(text8(76.07f));

        
        StartCoroutine(text9(77.20f));

        
        StartCoroutine(text10(87.51f));

        
        StartCoroutine(text11(97.17f));
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
        ChangeText("E' stata una giornata lunga e stressante. Non è vero... Anne?");
        }

        else if (leng == "JP")
        {
            yield return new WaitForSeconds(waitTime);
            ChangeText("今日は長くて\n大変な日だったわね...ねぇアン?");
        }

        StartCoroutine(pause(3f));
    }

    private IEnumerator text2(float waitTime)
    {

        if (leng == "IT")
        {
            yield return new WaitForSeconds(waitTime);
            ChangeText("Si, Mamma. Ma non voglio andare a dormire.");
        }
        else if (leng == "JP")
        {
            yield return new WaitForSeconds(waitTime);
            ChangeText("そうねママ.\nでも私寝たくないの.");
        }
        StartCoroutine(pause(3f));
    }

    private IEnumerator text3(float waitTime)
    {

        if (leng == "IT")
        {
            yield return new WaitForSeconds(waitTime);
            ChangeText("Stai immaginando ancora cose, mia piccola e dolce bambina?");

        }
        else if (leng == "JP")
        {
            yield return new WaitForSeconds(waitTime);
            ChangeText("まだ変なものが見えるっていうの?");
        }
        StartCoroutine(pause(3f));
    }

    private IEnumerator text4(float waitTime)
    {

        if (leng == "IT")
        {
            yield return new WaitForSeconds(waitTime);
            ChangeText("Non esistono cose come i mostri");

        }
        else if (leng == "JP")
        {
            yield return new WaitForSeconds(waitTime);
            ChangeText("バケモノなんているわけないじゃない.");
        }
        StartCoroutine(pause(3f));
    }

    private IEnumerator text5(float waitTime)
    {

        if (leng == "IT")
        {
            yield return new WaitForSeconds(waitTime);
            ChangeText("Ci siamo solo tu, io e papà");
        }
        else if (leng == "JP")
        {
            yield return new WaitForSeconds(waitTime);
            ChangeText("家には私とあなたとパパしかいないわ.");
        }
        StartCoroutine(pause(3f));
    }

    private IEnumerator text6(float waitTime)
    {

        if (leng == "IT")
        {
            yield return new WaitForSeconds(waitTime);
            ChangeText("Sogni d'oro, Anne <3");
        }
        else if (leng == "JP")
        {
            yield return new WaitForSeconds(waitTime);
            ChangeText("良い子だから寝なさい\n アン.");
        }
        StartCoroutine(pause(3f));
    }

    private IEnumerator text7(float waitTime)
    {

        if (leng == "IT")
        {
            yield return new WaitForSeconds(waitTime);
            ChangeText("Si, Mamma. Ma non voglio andare a dormire.");
        }
        else if (leng == "JP")
        {
            yield return new WaitForSeconds(waitTime);
            ChangeText("そうかもしれない...\nでも寝たくないの.");
        }
        StartCoroutine(pause(3f));
    }

    private IEnumerator text8(float waitTime)
    {

        if (leng == "IT")
        {
            yield return new WaitForSeconds(waitTime);
            ChangeText("Non voglio andare a dormire.");
        }
        else if (leng == "JP")
        {
            yield return new WaitForSeconds(waitTime);
            ChangeText("寝たら...\nダメなの.");
        }
        StartCoroutine(pause(3f));
    }

    private IEnumerator text9(float waitTime)
    {
        if (leng == "IT")
        {
            yield return new WaitForSeconds(waitTime);
            ChangeText("Ehylà! Pronti per un po' di divertimento?");
        }
        else if (leng == "JP")
        {
            yield return new WaitForSeconds(waitTime);
            ChangeText("やぁ諸君! 楽しむ準備はできてるかい?");
        }
        StartCoroutine(pause(3f));
    }

    private IEnumerator text10(float waitTime)
    {
        if (leng == "IT")
        {
            yield return new WaitForSeconds(waitTime);
            ChangeText("Tu... Mostro");
        }
        else if (leng == "JP")
        {
            yield return new WaitForSeconds(waitTime);
            ChangeText("この... バケモノ.");
        }
        StartCoroutine(pause(3f));
    }

    private IEnumerator text11(float waitTime)
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
        StartCoroutine(pause(3f));
    }

    private IEnumerator pause(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        ChangeText("");
    }
}
