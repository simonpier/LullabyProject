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
        //_itemList = new List<Item>();
        //foreach (var item in itemTable.Items)
        //{
        //    _itemList.Add(item);
        //}
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

//List<string[]> csvDatas = new List<string[]>();
//Dictionary<int, Item> itemDic = new Dictionary<int, Item>();

//// Start is called before the first frame update
//void Start()
//{        
//    csvFile = Resources.Load("Item") as TextAsset;//csvの読み込み
//    StringReader reader = new StringReader(csvFile.text);

//    //次に読み込める文字列がなくなると-1になる
//    while (reader.Peek() != -1)
//    {
//        string line = reader.ReadLine();
//        csvDatas.Add(line.Split(','));
//    }

//    //リストをディクショナリに代入
//    for (int i = 0; i < csvDatas.Count; i++)
//    {
//        int key = int.Parse(csvDatas[i][(int)ItemElements.id]);
//        var item = new Item(key, csvDatas[i][(int)ItemElements.name]);
//        itemDic.Add(key, item);
//    }
//    foreach (KeyValuePair<int, Item> pair in itemDic)
//    {
//        Debug.Log("key:" + pair.Key);
//        Debug.Log("values:" + pair.Value.itemName);
//    }
//}

//public string GetByName(int id)
//{
//    foreach (KeyValuePair<int, Item> pair in itemDic)
//    {
//        Debug.Log("key:" + pair.Key);
//        Debug.Log("values:" + pair.Value.itemName);
//        if (pair.Key == id)
//        {
//            return pair.Value.itemName;
//        }
//    }
//    string enpty = "空です";
//    return enpty;
//}
//// Update is called once per frame
//void Update()
//{

//}



