using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class PlayerItemRepository
{
    //Store the items owned by the player
    private static Dictionary<int, PlayerItem> _itemDic = new Dictionary<int, PlayerItem>();

    //
    public static void AddItem(int id)
    {
        PlayerItem item = null;
        
        //Still in possession
        if (!_itemDic.TryGetValue(id, out item))
        {
            item = new PlayerItem(id, 0);
            _itemDic.Add(id, item);
        }
        //Check the item ownership limit
        else if (item.QuantityCounter < ItemRepository.RetrieveById(id).PossessionLimit)
        {
            item.QuantityCounter += 1;
        }
        else
        {
            Debug.Log("上限に達しています。");//over limit
        }
    }

    //Return all items in possession
    public static PlayerItem[] OwnItemAll()
    {
        return _itemDic.Values.ToArray();
    }

    private static List<string> ItemInformationList;

    public static void SaveItem()
    {
        //Change all item information to a string and save
        ItemInformationList = new List<string>();
        foreach (var playerItem in _itemDic.Values)
        {
            ItemInformationList.Add(String.Join("_", playerItem.ItemId, playerItem.QuantityCounter));
        }
        var csv = String.Join(",", ItemInformationList);

        PlayerPrefs.SetString("HAVE_ITEM_CSV", csv);
    }

    public static void LoadItem()
    {
        _itemDic = new Dictionary<int, PlayerItem>();
        var csv = PlayerPrefs.GetString("HAVE_ITEM_CSV");

        //Divide the stringed information and save it in the dictionary
        foreach (var chunk in csv.Split(','))
        {
            var itemStatus = chunk.Split('_');

            int tempId = int.Parse(itemStatus[0]);
            int tempCount = int.Parse(itemStatus[1]);

            var playerItem = new PlayerItem(tempId, tempCount);
            _itemDic.Add(tempId, playerItem);
        }
    }
}
