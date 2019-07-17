using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableFurnitures_ML : MonoBehaviour
{
    protected bool interact;

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.Return) && collision.tag == "Player")
        {
            interact = true;
        }
    }
}
