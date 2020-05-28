using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageContainer_SP : MonoBehaviour
{
    public string language;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(this.gameObject.GetComponent<LanguageContainer_SP>());
    }


    public void ChangeLanguage( string lang)
    {
        language = lang;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
