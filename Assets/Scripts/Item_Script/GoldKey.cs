using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldKey : MonoBehaviour
{
    public 
    //ItemRepository itemRepository;
    string name = "";
    // Start is called before the first frame update
    void Start()
    {
        //itemRepository = new ItemRepository();
        ShowScriptableObjectData();
        //var itemRepository = new ItemRepository();
        //Debug.Log(itemRepository.num);
        //name = itemRepository.GetByName(0);
        //Debug.Log(name);
    }

    void ShowScriptableObjectData()
    {
        //Debug.Log("hoge");
        //Debug.Log("私の名前は"+ itemRepository.id);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
