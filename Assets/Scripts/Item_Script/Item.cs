using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{
    [SerializeField]
    private string _name;
    [SerializeField]
    private int _id;
    [SerializeField]
    private int _possessionLimit;

    public string Name { get { return _name; } }
    public int Id { get { return _id; } }
    public int PossessionLimit {get { return _possessionLimit; } }


    public override string ToString()
    {
        return string.Format("Id: {0}, Name: {1}", _id, _name);
    }
}
