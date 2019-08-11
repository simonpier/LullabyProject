using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLibraryEvent_ML : MonoBehaviour
{
    [SerializeField] GameObject bookMonster;

    // Start is called before the first frame update
    void Start()
    {
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Input.GetButtonDown("Interaction"))
        {
            bookMonster.transform.position = this.transform.position + new Vector3(-1.5f, -1.5f, 0);
            bookMonster.SetActive(true);
        }
    }
}
