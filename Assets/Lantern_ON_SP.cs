using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern_ON_SP : MonoBehaviour
{
    
    [SerializeField] GameObject gameManager;
    EventManager_ML eventManager;
    // Start is called before the first frame update
    void Start()
    {
        eventManager = gameManager.GetComponent<EventManager_ML>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            eventManager.LanternTaken = true;
            Debug.Log("fiero");
        }
    }
}
