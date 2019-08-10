using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookshelfTrigger_KT : MonoBehaviour
{
    [SerializeField]
    GameObject warpHeight;
    Vector3 origin;
    [SerializeField]
    GameObject moveArea;
    public bool isPlayerOn { get; set; }

    [SerializeField] GameObject gameManager;
    EventManager_ML eventManager;

    public static int counter = 0;

    void Start()
    {
        isPlayerOn = false;
        eventManager = gameManager.GetComponent<EventManager_ML>();
    }

    void Update()
    {
        //Debug.Log(eventManager.IsPlayerOnLibrary);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if ((other.gameObject.tag == "Player"))
        {
            if (Input.GetButtonDown("Interaction") && warpHeight.transform.position.y - 0.01f > other.transform.position.y)
            {
                Climb(other.gameObject);
            }
            else if(Input.GetButtonDown("Interaction") && warpHeight.transform.position.y - 0.01f <= other.transform.position.y) // DownKeyCode
            {

                Dismount(other.gameObject);
            }
        }
    }

    void Climb(GameObject player)
    {
        if (PlayerMove_KT.Instance.MovingVertAnimation != PlayerMove_KT.VertAnimationType.Default) return;

        eventManager.IsPlayerOnLibrary = true;

        counter = Checkpoint_ML.Deadcount;

        PlayerMove_KT.Instance.StartVerticalAnimate(warpHeight.transform.position.y, PlayerMove_KT.VertAnimationType.Climb);
        if(moveArea) moveArea.SetActive(true);
        origin = player.transform.position;
        //warpHeight.transform.position = new Vector3(warpHeight.transform.position.x, player.transform.position.y, warpHeight.transform.position.z);
    }

    public void Dismount(GameObject player)
    {
        if (PlayerMove_KT.Instance.MovingVertAnimation != PlayerMove_KT.VertAnimationType.Default) return;

        eventManager.IsPlayerOnLibrary = false;

        if (Checkpoint_ML.Deadcount == BookshelfTrigger_KT.counter) PlayerMove_KT.Instance.StartVerticalAnimate(origin.y, PlayerMove_KT.VertAnimationType.Dismount);
        if (moveArea) moveArea.SetActive(false);
        //warpHeight.transform.position = new Vector3(warpHeight.transform.position.x, player.transform.position.y, warpHeight.transform.position.z);
    }
}
