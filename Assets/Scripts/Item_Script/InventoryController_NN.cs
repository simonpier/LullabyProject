using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryController_NN : MonoBehaviour
{

    [SerializeField] int size;

    public string key;
    public UnityEvent OnAdd;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Item")
        {
            Debug.Log("当たった");
            AddItem(collision.name);
        }
    }

    public void AddItem(string itemName)
    {
        Debug.Log(itemName);
        if (itemName == "Candele") { }
    }
}
