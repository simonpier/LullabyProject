using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EventManagerSecondStage_ML : MonoBehaviour
{
    #region Painting Room Event Variables
    [Header ("Painting Room Event")]
    [SerializeField] List<GameObject> paintings;

    [SerializeField] List<Vector3> paintingsPosition;

    NumbersPaintingRoom_ML numberScript;

    private int randomizer;
    private int indexRightNumber;

    public int IndexRightNumber { get => indexRightNumber; set => indexRightNumber = value; }


    #endregion

    #region Chandelier Room Event Variables

    [Header("Chandelier Room Event")]

    [SerializeField] List<GameObject> wrongCombination;
    [SerializeField] List<GameObject> rightCombination;
    [SerializeField] List<GameObject> chandeliers;
    [SerializeField] GameObject snakeCat;

    [SerializeField] float fadeTime;

    ChandelierMonsterBehaviour_ML firstChandelierMonster;
    ChandelierMonsterBehaviour_ML secondChandelierMonster;
    ChandelierMonsterBehaviour_ML thirdChandelierMonster;
    SnakeBossFightTrigger_ML bossTrigger;

    SpriteRenderer wrongRenderer;
    SpriteRenderer rightRenderer;

    #endregion

    #region Others Variables
    [Header("Others")]

    EventManager_ML firstEventManager;

    #endregion

    void Start()
    {
        #region Painting Room Event 
        PaintingRoomManager();
        #endregion

        #region Chandelier Room Event 

        firstChandelierMonster = chandeliers[0].GetComponent<ChandelierMonsterBehaviour_ML>();
        secondChandelierMonster = chandeliers[1].GetComponent<ChandelierMonsterBehaviour_ML>();
        thirdChandelierMonster = chandeliers[2].GetComponent<ChandelierMonsterBehaviour_ML>();

        bossTrigger = snakeCat.GetComponent<SnakeBossFightTrigger_ML>();

        #endregion

        #region Others
        firstEventManager = GetComponent<EventManager_ML>();
        firstEventManager.LanternTaken = true;
        #endregion
    }

    private void Update()
    {
        ChandelierCheck();
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

    #region Chandelier Room Event Methods

    private void ChandelierCheck()
    {
        if (firstChandelierMonster.IsDied)
        {
            StartCoroutine(numbersFading(0));
        }
        if (secondChandelierMonster.IsDied)
        {
            StartCoroutine(numbersFading(1));
        }
        if (thirdChandelierMonster.IsDied)
        {
            StartCoroutine(numbersFading(2));
        }
    }

    public void WoodPiecesIncrement()
    {
        bossTrigger.WoodPiecesCount++;
    }

    private IEnumerator numbersFading(int index)
    {
        wrongRenderer = wrongCombination[index].GetComponent<SpriteRenderer>();
        rightRenderer = rightCombination[index].GetComponent<SpriteRenderer>();

        wrongRenderer.DOFade(0, fadeTime);
        yield return new WaitForSeconds(fadeTime);
        wrongCombination[index].SetActive(false);
        rightRenderer.DOFade(255, fadeTime);
    }


    #endregion

}
