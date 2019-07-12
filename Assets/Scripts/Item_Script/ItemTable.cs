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