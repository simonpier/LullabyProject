using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager_ML : MonoBehaviour
{
    #region Lantern Event
    [SerializeField] private bool lanternTaken;
    public bool LanternTaken { get => lanternTaken; set => lanternTaken = value; }
    #endregion

    #region Second Library Event
    SecondLibraryEvent_ML firstSwitch;
    SecondLibraryEvent_ML secondSwitch;
    SecondLibraryEvent_ML thirdSwitch;

    [SerializeField] List<GameObject> libraries;

    private int randomLibrary;
    #endregion

    #region Player Management
    private bool isPlayerOnLibrary = false;
    public bool IsPlayerOnLibrary { get => isPlayerOnLibrary; set => isPlayerOnLibrary = value; }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        randomLibrary = Random.Range(0, libraries.Capacity);
        firstSwitch = libraries[randomLibrary].GetComponent<SecondLibraryEvent_ML>();
        libraries.RemoveAt(randomLibrary);

        randomLibrary = Random.Range(0, libraries.Capacity);
        secondSwitch = libraries[randomLibrary].GetComponent<SecondLibraryEvent_ML>();
        libraries.RemoveAt(randomLibrary);

        randomLibrary = Random.Range(0, libraries.Capacity);
        secondSwitch = libraries[randomLibrary].GetComponent<SecondLibraryEvent_ML>();
        libraries.RemoveAt(randomLibrary);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnLibraryOn()
    {
        isPlayerOnLibrary = true;
    }

    public void OnLibraryOff()
    {
        isPlayerOnLibrary = false;
    }
}
