using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternEvent_ML : MonoBehaviour
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Input.GetKeyDown(KeyCode.Return))
        {
            eventManager.LanternTaken = true;
            Destroy(this, 0.1f);
        }
    }

}
