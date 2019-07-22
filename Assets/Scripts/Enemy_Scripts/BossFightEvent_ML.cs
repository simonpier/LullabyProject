﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Animations;

public class BossFightEvent_ML : MonoBehaviour
{
    [SerializeField] GameObject gameCamera;
    [SerializeField] GameObject bossTrigger;
    [SerializeField] GameObject player;
    [SerializeField] GameObject spiderBoss;
    [SerializeField] GameObject cinematicBar;
    
    [SerializeField] float bossPosition;
    [SerializeField] float lerpDuration;

    TargetCamera_KT cameraController;
    PlayerMove_KT playerScript;
    Animator spiderBossAnim;
    Animator playerAnim;
    Rigidbody2D playerRB;
    PositionConstraint cameraCon;
    CinematicBar_ML cinBar;
    Spiderdog_Boss_Behaviour spiderbossBehaviour;

    // Start is called before the first frame update
    void Start()
    {
        cameraCon = gameCamera.GetComponent<PositionConstraint>();
        cameraController = gameCamera.GetComponent<TargetCamera_KT>();
        spiderBossAnim = spiderBoss.GetComponent<Animator>();
        playerScript = player.GetComponent<PlayerMove_KT>();
        playerRB = player.GetComponent<Rigidbody2D>();
        playerAnim = player.GetComponent<Animator>();
        cinBar = cinematicBar.GetComponent<CinematicBar_ML>();
        spiderbossBehaviour = spiderBoss.GetComponent<Spiderdog_Boss_Behaviour>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Boss_Fight")
        {
            StartCoroutine(BossEvent());
        }
    }

    private IEnumerator BossEvent()
    {
        playerScript.enabled = false;
        playerRB.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        bossTrigger.SetActive(false);
        float tmpGameCameraPos = gameCamera.transform.position.x;
        cameraCon.enabled = false;
        cameraController.enabled = false;
        gameCamera.transform.DOMoveX(bossPosition, lerpDuration);
        yield return new WaitForSeconds(lerpDuration);
        cinBar.Show(170, 0.7f);

        spiderBossAnim.Play("Spiderdog_Attack");
        yield return new WaitForSeconds(1f);
        spiderBossAnim.Play("Spiderdog_Attack");
        yield return new WaitForSeconds(1f);

        playerAnim.enabled = false;
        gameCamera.transform.DOMoveX(tmpGameCameraPos, lerpDuration);
        cinBar.Hide(0.1f);
        yield return new WaitForSeconds(lerpDuration);
        cameraCon.enabled = true;
        cameraController.enabled = true;
        playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
        playerScript.enabled = true;
        spiderBossAnim.SetBool("inRange", true);
        spiderbossBehaviour.IsTriggered = true;
        playerAnim.enabled = true;
    }
}
