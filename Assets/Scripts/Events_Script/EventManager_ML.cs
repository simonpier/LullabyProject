using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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

    SecondLibraryEvent_ML firstSwitch;
    SecondLibraryEvent_ML secondSwitch;
    SecondLibraryEvent_ML thirdSwitch;

    GameObject firstLibrary;
    GameObject secondLibrary;
    GameObject thirdLibrary;

    private int randomLibrary;
    private bool secondLibraryPassed;
    private float firstWallPosY;
    private float secondWallPosY;
    private float thirdWallPosY;
    private float endingWallPosition;

    #endregion

    #region Player Management
    private bool isPlayerOnLibrary = false;
    public bool IsPlayerOnLibrary { get => isPlayerOnLibrary; set => isPlayerOnLibrary = value; }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        #region Second Library Event
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
        if (firstSwitch.SwitchActivated && !secondLibraryPassed)
        {
            firstWall.transform.DOMoveY(endingWallPosition, 2f);
        }
        else if (!firstSwitch.SwitchActivated && !secondLibraryPassed && firstWall.transform.position.y != firstWallPosY)
        {
            firstWall.transform.DOMoveY(firstWallPosY, 2f);
        }

        if (secondSwitch.SwitchActivated && !secondLibraryPassed)
        {
            secondWall.transform.DOMoveY(endingWallPosition, 2f);
        }
        else if (!secondSwitch.SwitchActivated && !secondLibraryPassed && secondWall.transform.position.y != secondWallPosY)
        {
            firstWall.transform.DOMoveY(secondWallPosY, 2f);
        }

        if (thirdSwitch.SwitchActivated && !secondLibraryPassed)
        {
            thirdWall.transform.DOMoveY(endingWallPosition, 2f);
            secondLibraryPassed = true;
        }
        else if (!thirdSwitch.SwitchActivated && !secondLibraryPassed && thirdWall.transform.position.y != thirdWallPosY)
        {
            thirdWall.transform.DOMoveY(thirdWallPosY, 2f);
        }
    }

    private void SwitchCheck()
    {
        if (firstSwitch.SwitchActivated == false)
        {
            secondSwitch.SwitchActivated = false;
            thirdSwitch.SwitchActivated = false;
        }
        else if (firstSwitch.SwitchActivated == true)
        {
            if (secondSwitch.SwitchActivated == false && thirdSwitch.SwitchActivated == true)
            {
                thirdSwitch.SwitchActivated = false;
                firstSwitch.SwitchActivated = false;
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
