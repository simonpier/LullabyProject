using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Animations;

public class FatherDeathCutscene_ML : MonoBehaviour
{
    [SerializeField] List<GameObject> lanternsLight;
    [SerializeField] GameObject player;
    [SerializeField] GameObject gameCamera;
    [SerializeField] GameObject father;
    [SerializeField] GameObject doll;
    [SerializeField] GameObject stairs;
    [SerializeField] GameObject firstChandelier;
    [SerializeField] GameObject secondChandelier;
    [SerializeField] GameObject thirdChandelier;
    [SerializeField] GameObject lampMonster;

    [SerializeField] float fatherEntryLerp;
    [SerializeField] float lerpDuration;
    [SerializeField] float lightLerpDuration;
    [SerializeField] float lightChangeDuration;
    [SerializeField] float lightFinalIntensity;
    [SerializeField] float alphaFatherLerp;
    //Tatsuyoshi Add
    [SerializeField] GameObject Enemys;

    [SerializeField] AudioClip sound;
    [SerializeField] AudioSource source;

    Animator playerAnim;
    Animator fatherAnim;
    Rigidbody2D playerRB;
    PlayerMove_KT playerScript;
    ChangeWeapon_NN playerWeaponScript;
    Father_Boss_Behaviour_ML fatherScript;
    PositionConstraint cameraCon;
    TargetCamera_KT cameraController;
    Light light;
    SpriteRenderer fatherRenderer;
    SpriteRenderer dollRenderer;
    SpriteRenderer stairRenderer;
    Color fullAlpha;
    AudioSource cameraSource;
    ChandelierMonsterBehaviour_ML firstChandelierScript;
    ChandelierMonsterBehaviour_ML secondChandelierScript;
    ChandelierMonsterBehaviour_ML thirdChandelierScript;
    LampMonsterBehaviour_ML lampScript;

    private float lightIntensity;

    // Start is called before the first frame update
    void Start()
    {
        cameraCon = gameCamera.GetComponent<PositionConstraint>();
        cameraController = gameCamera.GetComponent<TargetCamera_KT>();
        playerWeaponScript = player.GetComponent<ChangeWeapon_NN>();
        playerScript = player.GetComponent<PlayerMove_KT>();
        fatherScript = father.GetComponent<Father_Boss_Behaviour_ML>();
        playerRB = player.GetComponent<Rigidbody2D>();
        playerAnim = player.GetComponent<Animator>();
        fatherAnim = father.GetComponent<Animator>();
        fatherRenderer = father.GetComponent<SpriteRenderer>();
        dollRenderer = doll.GetComponent<SpriteRenderer>();
        stairRenderer = stairs.GetComponent<SpriteRenderer>();
        cameraSource = gameCamera.GetComponent<AudioSource>();
        firstChandelierScript = firstChandelier.GetComponent<ChandelierMonsterBehaviour_ML>();
        secondChandelierScript = secondChandelier.GetComponent<ChandelierMonsterBehaviour_ML>();
        thirdChandelierScript = thirdChandelier.GetComponent<ChandelierMonsterBehaviour_ML>();
        lampScript = lampMonster.GetComponent<LampMonsterBehaviour_ML>();

        StartCoroutine(FatherEntry());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator FatherEntry()
    {
        

        gameCamera.transform.position = new Vector3(gameCamera.transform.position.x, -45.97925f, gameCamera.transform.position.z);
        playerScript.enabled = false;
        playerWeaponScript.enabled = false;
        playerRB.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        playerAnim.enabled = false;

        float tmpGameCameraPos = gameCamera.transform.position.x;
        cameraCon.enabled = false;
        cameraController.enabled = false;

        gameCamera.transform.DOMoveX(father.transform.position.x, fatherEntryLerp);
        yield return new WaitForSeconds(fatherEntryLerp);

        gameCamera.transform.DOMoveX(tmpGameCameraPos, fatherEntryLerp);
        yield return new WaitForSeconds(fatherEntryLerp);

        cameraCon.enabled = true;
        cameraController.enabled = true;
        playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
        playerScript.enabled = true;
        playerWeaponScript.enabled = true;
        playerAnim.enabled = true;
        fatherScript.Activation();
        stairRenderer.DOFade(0, 2f);
        yield return new WaitForSeconds(2f);
        stairs.SetActive(false);

    }

    public IEnumerator FatherDeath()
    {
        cameraSource.Stop();

        gameObject.GetComponent<AudioSource>().clip = sound;
        source.clip = sound;
        source.volume = 1;
        source.Play();

        firstChandelierScript.enabled = false;
        secondChandelierScript.enabled = false;
        thirdChandelierScript.enabled = false;
        lampScript.enabled = false;
        fatherScript.enabled = false;
        playerScript.enabled = false;
        playerWeaponScript.enabled = false;
        playerRB.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        fatherAnim.Play("Father_Walk");
        fatherAnim.enabled = false;
        playerAnim.enabled = false;

        float tmpGameCameraPos = gameCamera.transform.position.x;
        cameraCon.enabled = false;
        cameraController.enabled = false;



        //First lantern
        gameCamera.transform.DOMoveX(lanternsLight[0].transform.position.x, lerpDuration);
        yield return new WaitForSeconds(lerpDuration);

        light = lanternsLight[0].GetComponent<Light>();
        lightIntensity = light.intensity;

        light.DOIntensity(lightFinalIntensity, lightChangeDuration);
        yield return new WaitForSeconds(lightChangeDuration);
        light.DOIntensity(lightIntensity, lightChangeDuration);
        yield return new WaitForSeconds(lightChangeDuration);
        light.DOIntensity(lightFinalIntensity, lightChangeDuration);
        yield return new WaitForSeconds(lightChangeDuration);
        light.DOIntensity(lightIntensity, lightChangeDuration);
        yield return new WaitForSeconds(lightChangeDuration);

        //Second lantern
        gameCamera.transform.DOMoveX(lanternsLight[1].transform.position.x, lerpDuration);
        yield return new WaitForSeconds(lerpDuration);

        light = lanternsLight[1].GetComponent<Light>();
        lightIntensity = light.intensity;

        light.DOIntensity(lightFinalIntensity, lightChangeDuration);
        yield return new WaitForSeconds(lightChangeDuration);
        light.DOIntensity(lightIntensity, lightChangeDuration);
        yield return new WaitForSeconds(lightChangeDuration);
        light.DOIntensity(lightFinalIntensity, lightChangeDuration);
        yield return new WaitForSeconds(lightChangeDuration);
        light.DOIntensity(lightIntensity, lightChangeDuration);
        yield return new WaitForSeconds(lightChangeDuration);

        //Third lantern
        gameCamera.transform.DOMoveX(lanternsLight[2].transform.position.x, lerpDuration);
        yield return new WaitForSeconds(lerpDuration);

        light = lanternsLight[2].GetComponent<Light>();
        lightIntensity = light.intensity;

        light.DOIntensity(lightFinalIntensity, lightChangeDuration);
        yield return new WaitForSeconds(lightChangeDuration);
        light.DOIntensity(lightIntensity, lightChangeDuration);
        yield return new WaitForSeconds(lightChangeDuration);
        light.DOIntensity(lightFinalIntensity, lightChangeDuration);
        yield return new WaitForSeconds(lightChangeDuration);
        light.DOIntensity(lightIntensity, lightChangeDuration);
        yield return new WaitForSeconds(lightChangeDuration);

        //Forth lantern
        gameCamera.transform.DOMoveX(lanternsLight[3].transform.position.x, lerpDuration);
        yield return new WaitForSeconds(lerpDuration);

        light = lanternsLight[3].GetComponent<Light>();
        lightIntensity = light.intensity;

        light.DOIntensity(lightFinalIntensity, lightChangeDuration);
        yield return new WaitForSeconds(lightChangeDuration);
        light.DOIntensity(lightIntensity, lightChangeDuration);
        yield return new WaitForSeconds(lightChangeDuration);
        light.DOIntensity(lightFinalIntensity, lightChangeDuration);
        yield return new WaitForSeconds(lightChangeDuration);
        light.DOIntensity(lightIntensity, lightChangeDuration);
        yield return new WaitForSeconds(lightChangeDuration);


        //Father camera
        gameCamera.transform.DOMoveX(father.transform.position.x, lerpDuration);
        yield return new WaitForSeconds(lerpDuration);

        //lights
        for (int i = 0; i < lanternsLight.Count; i++)
        {
            lanternsLight[i].transform.DOMove(father.transform.position, lightLerpDuration);
            yield return new WaitForSeconds(lightLerpDuration);
            if (i == 3)
            {
                Enemys.SetActive(false);
                player.GetComponent<PlayerStats_ML>().ResetHealth();
                fatherRenderer.DOFade(0, alphaFatherLerp);
            }
        }

        doll.SetActive(true);
        dollRenderer.DOFade(1, alphaFatherLerp);
        yield return new WaitForSeconds(alphaFatherLerp);

        gameCamera.transform.DOMoveX(tmpGameCameraPos, lerpDuration);
        cameraCon.enabled = true;
        cameraController.enabled = true;
        playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
        playerScript.enabled = true;
        playerAnim.enabled = true;
        playerWeaponScript.enabled = true;
    }
}
