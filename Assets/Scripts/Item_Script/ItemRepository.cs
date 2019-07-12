using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class ItemRepository
{
    private static string TABLE_PATH = "ItemTable";

    private static List<Item> _itemList;//itemTableの情報
    private static Dictionary<int, Item> _itemDic = new Dictionary<int, Item>();//アイテムの情報

    static ItemRepository()
    {
        var itemTable = Resources.Load<ItemTable>(TABLE_PATH);
        _itemList = new List<Item>(itemTable.Items);
    }

    public static Item[] RetrieveAll()
    {
        var list = new List<Item>();
        list.AddRange(_itemList);
        list.AddRange(_itemDic.Values);
        return list.ToArray();
    }

    public static Item RetrieveById(int id)
    {
        Item item = null;
        if (!_itemDic.TryGetValue(id, out item))
        {
            item = _itemList.Where(x => x.Id == id).FirstOrDefault();
            if (item != null)
            {
                _itemList.Remove(item);
            }
            _itemDic.Add(id, item);
        }
        return item;
    }
}




