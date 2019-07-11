using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerItem
{
    private int _itemId;
    public int ItemId { get { return _itemId; } }
    private int _counter;//個数をカウントする
    public int Counter { get; set; }

    public PlayerItem(int id, int counter)
    {
        _itemId = id;
        Counter = counter;
    }

    public override string ToString()
    {
        return string.Format("Id:{0}, Counter:{1}", ItemId, Counter);
    }
}
