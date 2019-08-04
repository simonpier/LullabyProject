using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManagerSecondStage_ML : MonoBehaviour
{
    #region Painting Room Event Variables
    [SerializeField] List<GameObject> paintings;

    [SerializeField] List<Vector3> paintingsPosition;

    NumbersPaintingRoom_ML numberScript;

    private int randomizer;
    private int indexRightNumber;

    public int IndexRightNumber { get => indexRightNumber; set => indexRightNumber = value; }


    #endregion

    #region Others Variables

    EventManager_ML firstEventManager;

    #endregion

    void Start()
    {
        #region Painting Room Event 
        PaintingRoomManager();
        #endregion

        #region Others
        firstEventManager = GetComponent<EventManager_ML>();
        firstEventManager.LanternTaken = true;
        #endregion
    }

    #region Painting Room Event Methods

    private void PaintingRoomManager()
    {
        for (int i = 0; i < paintings.Count; i++)
        {
            paintingsPosition[i] = paintings[i].transform.position;
        }

        for (int i = 0; i < paintings.Count; i++)
        {
            randomizer = Random.Range(0, paintingsPosition.Count - 1);
            paintings[i].transform.position = paintingsPosition[randomizer];
            paintingsPosition.RemoveAt(randomizer);
        }
    }

    #endregion

}
