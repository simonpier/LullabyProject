using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    
    [ContextMenu("TEST")]
    void TEST()
    {
    //    var item = ItemRepository.RetrieveById(1);
    //    Debug.LogWarning(item.Name);

    //    item = ItemRepository.RetrieveById(2);
    //    Debug.LogWarning(item);

    //    item = ItemRepository.RetrieveById(15);
    //    Debug.LogWarning(item);

        PlayerItemRepository.SaveItem();
        PlayerItemRepository.LoadItem();
    }
    [ContextMenu("TEST_SaveOnly")]
    void TEST_SaveOnly()
    {
        PlayerItemRepository.SaveItem();

        var playerItem = PlayerItemRepository.OwnItemAll();
        foreach (var item in playerItem)
        {
            Debug.Log(item);
        }
    }
    [ContextMenu("TEST_LordOnly")]
    void TEST_LordOnly()
    {
        PlayerItemRepository.LoadItem();

        var playerItem = PlayerItemRepository.OwnItemAll();
        foreach (var item in playerItem)
        {
            Debug.Log(item);
        }
    }
    [ContextMenu("ADD")]
    void ADD()
    {
        PlayerItemRepository.AddItem(1);
    }
    [ContextMenu("ADD_2")]
    void ADD_2()
    {
        PlayerItemRepository.AddItem(2);
    }
}
