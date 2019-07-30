using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Fungus;

public class EventManager_ML : MonoBehaviour
{
    #region Lantern Event
    [Header("Lantern Event")]
    [SerializeField] private bool lanternTaken;
    public bool LanternTaken { get => lanternTaken; set => lanternTaken = value; }
    #endregion

    #region Second Library Event
    [Header("Library Event")]
    [SerializeField] List<GameObject> libraries;

    [SerializeField] GameObject firstWall;
    [SerializeField] GameObject secondWall;
    [SerializeField] GameObject thirdWall;

    [SerializeField] float distanceWallPosition;

    [SerializeField] Flowchart flowchart;
    [SerializeField] string dialogue1, dialogue2, dialogue3;

    SecondLibraryEvent_ML firstSwitch;
    SecondLibraryEvent_ML secondSwitch;
    SecondLibraryEvent_ML thirdSwitch;

    GameObject firstLibrary;
    GameObject secondLibrary;
    GameObject thirdLibrary;

    SpriteRenderer rendererFirstWall;
    SpriteRenderer rendererSecondWall;
    SpriteRenderer rendererThirdWall;

    Collider2D colliderFirstWall;
    Collider2D colliderSecondWall;
    Collider2D colliderThirdWall;

    private int randomLibrary;
    private bool secondLibraryPassed;
    private bool firstCheck = false;
    private bool secondCheck = false;
    private float firstWallPosY;
    private float secondWallPosY;
    private float thirdWallPosY;
    private float endingWallPosition;

    #endregion

    #region Player Management
    private bool isPlayerOnLibrary = false;
    public bool IsPlayerOnLibrary { get => isPlayerOnLibrary; set => isPlayerOnLibrary = value; }
    #endregion

    [SerializeField] AudioManager audio;

    // Start is called before the first frame update
    void Start()
    {
        #region Second Library Event

        rendererFirstWall = firstWall.GetComponent<SpriteRenderer>();
        rendererSecondWall = secondWall.GetComponent<SpriteRenderer>();
        rendererThirdWall = thirdWall.GetComponent<SpriteRenderer>();

        colliderFirstWall = firstWall.GetComponent<Collider2D>();
        colliderSecondWall = secondWall.GetComponent<Collider2D>();
        colliderThirdWall = thirdWall.GetComponent<Collider2D>();

        firstWallPosY = firstWall.transform.position.y;
        secondWallPosY = secondWall.transform.position.y;
        thirdWallPosY = thirdWall.transform.position.y;

        endingWallPosition = firstWallPosY - distanceWallPosition;

        SecondLibraryRandomizer();
        #endregion
    }

    void Update()
    {
        SecondLibraryCheck();
    }

    #region Second Library Event
    private void SecondLibraryRandomizer()
    {
        randomLibrary = Random.Range(0, libraries.Capacity -1);
        firstLibrary = libraries[randomLibrary];
        firstSwitch = firstLibrary.GetComponent<SecondLibraryEvent_ML>();
        libraries.RemoveAt(randomLibrary);

        randomLibrary = Random.Range(0, libraries.Capacity -1);
        secondLibrary = libraries[randomLibrary];
        secondSwitch = secondLibrary.GetComponent<SecondLibraryEvent_ML>();
        libraries.RemoveAt(randomLibrary);

        randomLibrary = 0;
        thirdLibrary = libraries[randomLibrary];
        thirdSwitch = thirdLibrary.GetComponent<SecondLibraryEvent_ML>();
        libraries.RemoveAt(randomLibrary);
    }

    private void SecondLibraryCheck()
    {
        SwitchCheck();
        WallCheck();
    }

    private void WallCheck()
    {
        if (firstSwitch.SwitchActivated && !secondLibraryPassed && firstCheck == false )
        {
            audio.PlaySound("wall moving");
            rendererFirstWall.DOFade(0f, distanceWallPosition);
            colliderFirstWall.enabled = false;
            firstCheck = true;
            flowchart.ExecuteBlock(dialogue1);
        }
        else if (!firstSwitch.SwitchActivated && !secondLibraryPassed && firstWall.transform.position.y != firstWallPosY)
        {
            rendererFirstWall.DOFade(255f, distanceWallPosition);
            colliderFirstWall.enabled = true;
            firstCheck = false;
        }

        if (secondSwitch.SwitchActivated && !secondLibraryPassed && secondCheck == false )
        {
            audio.PlaySound("wall moving");
            rendererSecondWall.DOFade(0f, distanceWallPosition);
            colliderSecondWall.enabled = false;
            secondCheck = true;
            flowchart.ExecuteBlock(dialogue2);
        }
        else if (!secondSwitch.SwitchActivated && !secondLibraryPassed && secondWall.transform.position.y != secondWallPosY)
        {
            rendererSecondWall.DOFade(255f, distanceWallPosition);
            colliderSecondWall.enabled = true;
            secondCheck = false;
        }

        if (thirdSwitch.SwitchActivated && !secondLibraryPassed)
        {
            flowchart.ExecuteBlock(dialogue3);
            rendererThirdWall.DOFade(0f, distanceWallPosition);
            colliderThirdWall.enabled = false;
            audio.PlaySound("right enigma");
            secondLibraryPassed = true;
            
        }
        else if (!thirdSwitch.SwitchActivated && !secondLibraryPassed && thirdWall.transform.position.y != thirdWallPosY)
        {
            rendererThirdWall.DOFade(255f, distanceWallPosition);
            colliderThirdWall.enabled = true;
        }
    }

    private void SwitchCheck()
    {
        if (firstSwitch.SwitchActivated == false)
        {
            secondSwitch.SwitchActivated = false;
            thirdSwitch.SwitchActivated = false;
            //
            //audio.PlaySound("wrong enigma");
        }
        else if (firstSwitch.SwitchActivated == true)
        {
            if (secondSwitch.SwitchActivated == false && thirdSwitch.SwitchActivated == true)
            {
                thirdSwitch.SwitchActivated = false;
                firstSwitch.SwitchActivated = false;
                //
                audio.PlaySound("wrong enigma");
            }
        }
    }
    #endregion

    #region Player Management
    public void OnLibraryOn()
    {
        isPlayerOnLibrary = true;
    }

    public void OnLibraryOff()
    {
        isPlayerOnLibrary = false;
    }
    #endregion


}
