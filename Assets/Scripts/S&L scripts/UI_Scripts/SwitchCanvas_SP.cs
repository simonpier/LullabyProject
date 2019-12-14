using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwitchCanvas_SP : MonoBehaviour
{
    [SerializeField] GameObject firstObject;

    public void Switch()
    {

        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(firstObject, null);

    }
        
        
}
