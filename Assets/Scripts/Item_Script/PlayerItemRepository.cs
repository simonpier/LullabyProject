using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerItemRepository : MonoBehaviour
{
    private static List<PlayerItem> _itemList;
    private static Dictionary<int, PlayerItem> _itemDic = new Dictionary<int, PlayerItem>();
    PlayerItemRepository()
    {
        _itemList = new List<PlayerItem>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _itemList.Add(new PlayerItem(1,100));
        foreach (var item in _itemList)
        {
            Debug.Log(item);
        }
    }

    public static PlayerItem RetrieveById(int id)
    {
        PlayerItem item = null;
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
