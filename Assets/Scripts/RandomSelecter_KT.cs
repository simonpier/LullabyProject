using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSelecter_KT : MonoBehaviour
{
    [SerializeField] GameObject[] objects;
    int size;

    void Start()
    {
        size = objects.Length;
        Randomize();
    }

    void Randomize()
    {
        List<int> before = new List<int>();
        Vector3[] originPosition = new Vector3[size];
        int beforeSize = size;
        for (int i = 0; i < size; i++) {
            before.Add(i);
            originPosition[i] = objects[i].transform.position;
        }

        List<int> after = new List<int>();

        while (beforeSize > 0)
        {
            var randIndex = Random.Range(0, beforeSize);
            after.Add(before[randIndex]);
            before.RemoveAt(randIndex);
            beforeSize--;
        }

        for (int i = 0; i < size; i++)
        {
            objects[i].transform.position = originPosition[ after[i] ];
        }
    }
}
