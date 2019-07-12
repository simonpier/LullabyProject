using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerItem
{
    private int _itemId;
    public int ItemId { get { return _itemId; } }
    private int _quantityCounter;//個数をカウントする
    public int QuantityCounter { get; set; }

    public PlayerItem(int id, int quantityCounter)
    {
        _itemId = id;
        QuantityCounter = quantityCounter;
    }

    public override string ToString()
    {
        return string.Format("Id:{0}, Counter:{1}", ItemId, QuantityCounter);
    }
}
