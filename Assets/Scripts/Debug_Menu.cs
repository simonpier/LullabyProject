using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Debug_Menu : MonoBehaviour
{
    [SerializeField] List<GameObject> floorDestination;
    [SerializeField] List<GameObject> enemies;

    [SerializeField] List<string> floorName;

    [SerializeField] GameObject player;
    [SerializeField] Camera gameCamera;

    [SerializeField] TMP_Dropdown floorList;

    EnemyController_ML enemyController;
    FlyingCamera_SP flyCamera;
    CameraController_NN normalCamera;
    PlayerMovement_NN playerMovement;

    private int selectedFloor;
    private float originalFov;

    //Light
    //Flight camera

    void Start()
    {
        floorList.AddOptions(floorName);
        originalFov = gameCamera.fieldOfView;
    }

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
        for (int i = 0; i < enemies.Count; i++)
        {
            enemyController = enemies[i].GetComponent<EnemyController_ML>();
            enemyController.EnemyReset();
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
