using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Debug_Menu : MonoBehaviour
{
    //TODO add enemy reset

    [SerializeField] List<GameObject> floorDestination;
    [SerializeField] List<GameObject> lampMonsters;
    [SerializeField] List<GameObject> doorMonsters;
    [SerializeField] List<GameObject> spiderDog;

    //[SerializeField] List<string> floorName;

    [SerializeField] GameObject player;
    [SerializeField] Camera gameCamera;
    //[SerializeField] GameObject debugMenu;

    [SerializeField] TMP_Dropdown floorList;

    EnemyController_ML enemyController;
    FlyingCamera_SP flyCamera;
    CameraController_NN normalCamera;
    PlayerMovement_NN playerMovement;
    PlayerStats_ML playerStat;
    Spiderdog_Boss_Behaviour spiderController;

    private int selectedFloor;
    private float originalFov;
    private bool activeDebug = false;

    void Start()
    {
        //floorList.AddOptions(floorName);
        originalFov = gameCamera.fieldOfView;
    }

    private void Update()
    {
        //DebugMenuEnabled();
    }

    //private void DebugMenuEnabled()
    //{
    //    if (Input.GetKeyDown(KeyCode.Tab))
    //    {
    //        activeDebug = !activeDebug;
    //    }
    //    if (activeDebug == false)
    //    {
    //        debugMenu.SetActive(false);
    //    }
    //    else if (activeDebug == true)
    //    {
    //        debugMenu.SetActive(true);
    //    }
    //}

    #region Teleportation Methods
    public void Teleportation()
    {
        player.transform.position = floorDestination[selectedFloor].transform.position;
    }

    public void floorSelection(int index)
    {
        selectedFloor = index;
    }
    #endregion

    #region Enemy Reset
    public void EnemyReset()
    {
        for (int i = 0; i < lampMonsters.Count; i++)
        {
            enemyController = lampMonsters[i].GetComponent<EnemyController_ML>();
            enemyController.EnemyReset();
        }
        for (int i = 0; i < doorMonsters.Count; i++)
        {
            enemyController = doorMonsters[i].GetComponent<EnemyController_ML>();
            enemyController.EnemyReset();
        }
        for (int i = 0; i < spiderDog.Count; i++)
        {
            spiderController = spiderDog[i].GetComponent<Spiderdog_Boss_Behaviour>();
            spiderController.EnemyReset();
        }

    }
    #endregion

    #region Flycamera
    public void FlyCameraActivation()
    {
        playerMovement = player.GetComponent<PlayerMovement_NN>();
        flyCamera = gameCamera.GetComponent<FlyingCamera_SP>();
        normalCamera = gameCamera.GetComponent<CameraController_NN>();

        playerMovement.enabled = false;
        normalCamera.enabled = false;
        gameCamera.fieldOfView = 125f;
        flyCamera.enabled = true;
    }
    public void FlyCameraDeactivation()
    {
        playerMovement.enabled = true;
        flyCamera = gameCamera.GetComponent<FlyingCamera_SP>();
        normalCamera = gameCamera.GetComponent<CameraController_NN>();

        flyCamera.enabled = false;
        gameCamera.fieldOfView = originalFov;
        normalCamera.enabled = true;
    }
    #endregion 
}
