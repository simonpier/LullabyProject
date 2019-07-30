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

    [SerializeField] float fadeDuration;

    [SerializeField] Flowchart flowchart;
    [SerializeField] string dialogue1, dialogue2, dialogue3;

    SecondLibraryEvent_ML firstSwitch;
    SecondLibraryEvent_ML secondSwitch;
    SecondLibraryEvent_ML thirdSwitch;

    GameObject firstLibrary;
    GameObject secondLibrary;
    GameObject thirdLibrary;

    SpriteRenderer rendererFirstSmoke;
    SpriteRenderer rendererSecondSmoke;
    SpriteRenderer rendererThirdSmoke;

    Collider2D colliderFirstSmoke;
    Collider2D colliderSecondSmoke;
    Collider2D colliderThirdSmoke;

    private int randomLibrary;
    private bool secondLibraryPassed;
    private bool firstCheck = false;
    private bool secondCheck = false;

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

        rendererFirstSmoke = firstWall.GetComponent<SpriteRenderer>();
        rendererSecondSmoke = secondWall.GetComponent<SpriteRenderer>();
        rendererThirdSmoke = thirdWall.GetComponent<SpriteRenderer>();

        colliderFirstSmoke = firstWall.GetComponent<Collider2D>();
        colliderSecondSmoke = secondWall.GetComponent<Collider2D>();
        colliderThirdSmoke = thirdWall.GetComponent<Collider2D>();

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
            rendererFirstSmoke.DOFade(0f, fadeDuration);
            colliderFirstSmoke.enabled = false;
            firstCheck = true;
            flowchart.ExecuteBlock(dialogue1);
        }
        else if (!firstSwitch.SwitchActivated && !secondLibraryPassed && rendererFirstSmoke.material.color.a != 255f)
        {
            rendererFirstSmoke.DOFade(255f, fadeDuration);
            colliderFirstSmoke.enabled = true;
            firstCheck = false;
        }

        if (secondSwitch.SwitchActivated && !secondLibraryPassed && secondCheck == false )
        {
            audio.PlaySound("wall moving");
            rendererSecondSmoke.DOFade(0f, fadeDuration);
            colliderSecondSmoke.enabled = false;
            secondCheck = true;
            flowchart.ExecuteBlock(dialogue2);
        }
        else if (!secondSwitch.SwitchActivated && !secondLibraryPassed && rendererSecondSmoke.material.color.a != 255f)
        {
            rendererSecondSmoke.DOFade(255f, fadeDuration);
            colliderSecondSmoke.enabled = true;
            secondCheck = false;
        }

        if (thirdSwitch.SwitchActivated && !secondLibraryPassed)
        {
            flowchart.ExecuteBlock(dialogue3);
            rendererThirdSmoke.DOFade(0f, fadeDuration);
            colliderThirdSmoke.enabled = false;
            audio.PlaySound("right enigma");
            secondLibraryPassed = true;
            
        }
        else if (!thirdSwitch.SwitchActivated && !secondLibraryPassed && rendererSecondSmoke.material.color.a != 255f)
        {
            rendererThirdSmoke.DOFade(255f, fadeDuration);
            colliderThirdSmoke.enabled = true;
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
