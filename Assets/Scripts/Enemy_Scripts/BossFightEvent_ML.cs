using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossFightEvent_ML : MonoBehaviour
{
    [SerializeField] GameObject gameCamera;
    [SerializeField] GameObject bossTrigger;
    [SerializeField] GameObject player;
    [SerializeField] GameObject spiderBoss;
    
    [SerializeField] float bossPosition;
    [SerializeField] float lerpDuration;

    CameraController_NN cameraController;
    PlayerMovement_NN playerScript;
    Animator spiderBossAnim;
    Animator playerAnim;
    Rigidbody2D playerRB;

    // Start is called before the first frame update
    void Start()
    {
        cameraController = gameCamera.GetComponent<CameraController_NN>();
        spiderBossAnim = spiderBoss.GetComponent<Animator>();
        playerScript = player.GetComponent<PlayerMovement_NN>();
        playerRB = player.GetComponent<Rigidbody2D>();
        playerAnim = player.GetComponent<Animator>();
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
        playerAnim.SetBool("Walking", false);
        playerRB.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        bossTrigger.SetActive(false);
        float tmpGameCameraPos = gameCamera.transform.position.x;
        cameraController.enabled = false;
        gameCamera.transform.DOMoveX(bossPosition, lerpDuration);
        yield return new WaitForSeconds(lerpDuration);

        spiderBossAnim.Play("Spiderdog_Attack");
        yield return new WaitForSeconds(1f);
        spiderBossAnim.Play("Spiderdog_Attack");
        yield return new WaitForSeconds(1f);

        gameCamera.transform.DOMoveX(tmpGameCameraPos, lerpDuration);
        yield return new WaitForSeconds(lerpDuration);
        cameraController.enabled = true;
        playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
        playerScript.enabled = true;
    }
}
