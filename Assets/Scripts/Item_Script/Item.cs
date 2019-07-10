﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemTable : ScriptableObject
{
    [SerializeField]
    private Item[] _Items;
    public Item[] Items { get { return _Items; } }
}

[Serializable]
public class Item
{
    [SerializeField]
    private int _id;

    [SerializeField]
    private string _name;

    public enum Type
    {
        consume,collect
    }

    public int Id { get { return _id; } }
    public string Name { get { return _name; } }
    

    public override string ToString()
    {
        return string.Format("Id: {0}, Name: {1}", _id, _name);
    }
}
