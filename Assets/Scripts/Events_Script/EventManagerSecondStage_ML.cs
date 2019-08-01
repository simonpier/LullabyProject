using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManagerSecondStage_ML : MonoBehaviour
{
    #region Painting Room Event Variables
    [SerializeField] List<GameObject> paintings;
    [SerializeField] List<GameObject> numbers;

    [SerializeField] List<Vector3> paintingsPosition;
    [SerializeField] List<Vector3> numbersPosition;

    NumbersPaintingRoom_ML numberScript;

    private int randomizer;
    private int indexRightNumber;

    public int IndexRightNumber { get => indexRightNumber; set => indexRightNumber = value; }

    #endregion

    void Start()
    {
        PaintingRoomManager();
    }


    #region Painting Room Event Methods

    private void PaintingRoomManager()
    {
        for (int i = 0; i < paintings.Count; i++)
        {
            paintingsPosition[i] = paintings[i].transform.position;
        }

        for (int i = 0; i < numbers.Count; i++)
        {
            numbersPosition[i] = numbers[i].transform.position;
        }

        for (int i = 0; i < paintings.Count; i++)
        {
            randomizer = Random.Range(0, paintingsPosition.Count - 1);
            paintings[i].transform.position = paintingsPosition[randomizer];
            paintingsPosition.RemoveAt(randomizer);
        }

        for (int i = 0; i < numbers.Count; i++)
        {
            randomizer = Random.Range(0, numbersPosition.Count - 1);
            numbers[i].transform.position = numbersPosition[randomizer];
            numbersPosition.RemoveAt(randomizer);
        }

        Invoke("RightNumberFinder", 1f);
    }

    private void RightNumberFinder()
    {
        for (int i = 0; i < numbers.Count; i++)
        {
            numberScript = numbers[i].GetComponent<NumbersPaintingRoom_ML>();
            if (numberScript.RightNumber)
            {
                indexRightNumber = i;
            }
        }
    }
    #endregion

}
