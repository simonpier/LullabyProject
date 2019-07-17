using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookshelfOnTrigger_KT : MonoBehaviour
{
    [SerializeField]
    BookshelfTrigger_KT parent;

    void OnTriggerExit2D(Collider2D other)
    {
        if ((other.gameObject.tag == "Player")) parent.Dismount(other.gameObject);
    }
}
