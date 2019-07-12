using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    [ContextMenu("TEST")]
    void TEST()
    {
        var playerItem = PlayerItemRepository.OwnItemAll();
        foreach (var item in playerItem)
        {
            Debug.Log(ItemRepository.RetrieveById(item.ItemId).Name + " " + item);
        }
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
