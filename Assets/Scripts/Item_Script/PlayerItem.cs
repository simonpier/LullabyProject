using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    private int _id;
    public int Id { get { return _id; } }
    private int _counter;//個数をカウントする
    public int Counter { get { return _counter; } }

    public PlayerItem(int id,int counter)
    {
        _id = id;
        _counter = counter;
    }

    // Start is called before the first frame update
    void Start()
    {
    }
   
    public override string ToString()
    {
        return string.Format("Id:{0}, Counter:{1}", Id, Counter);
    }
}
