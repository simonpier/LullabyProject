using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Spiderdog_Boss_Behaviour : EnemyController_ML
{

    [SerializeField] private float landDistance = 3.5f;
    [SerializeField] private float walkTime = 10f;
    [SerializeField] private float totalTimeWithoutDamages = 5f;
    [SerializeField] private float normalSpeed = 5f;
    [SerializeField] private float rampageSpeed = 10f;
    [SerializeField] private float rampageTime = 5f;
    [SerializeField] private Collider2D SlashCollider;
    [SerializeField] GameObject bossRoomPosDX;
    [SerializeField] GameObject bossRoomPosSX;
    [SerializeField] GameObject gameManager;
    [SerializeField] GameObject gameCamera;
    [SerializeField] AudioClip endBossFight;

    Vector3 originalBigness;
    EventManager_ML eventManager;

    AudioSource cameraMusic;


    private int attackRandomizer;
    private bool isTriggered;
    private bool rampage;
    private bool secondPhase;
    private bool canTakeDamage = true;
    private bool isLighted;
    private bool changeDirection;
    private bool isGoingRight;
    private bool isGoingLeft;
    private bool rampageCheck;
    private bool libraryCheck;
    private bool upsideDown;
    private float landingPos;
    private float timeWithoutDamages;
    private int rampageCounter = 0;

    public AudioClip[] sounds;

    public int pickedSound;

    [SerializeField] AudioSource source;
    private bool check = true, firstEncounter = true;

    public bool IsTriggered { get => isTriggered; set => isTriggered = value; }

    public override void Start()
    {
        base.Start();
        originalBigness = transform.localScale;
        landingPos = transform.position.y - landDistance;
        eventManager = gameManager.GetComponent<EventManager_ML>();
        timeWithoutDamages = totalTimeWithoutDamages;
        cameraMusic = gameCamera.GetComponent<AudioSource>();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Flip()
    {
        if (transform.position.x >= target.position.x && !facingRight)
        {
            facingRight = true;
            changeDirection = false;
            Vector3 enemyScale = transform.localScale;
            enemyScale.x = originalBigness.x;
            transform.localScale = enemyScale;
        }
        else if (transform.position.x < target.position.x && facingRight)
        {
            facingRight = false;
            changeDirection = true;
            Vector3 enemyScale = transform.localScale;
            enemyScale.x = -originalBigness.x;
            transform.localScale = enemyScale;
        }
    }

    public override void TargetTracking()
    {
        float distance = Vector2.Distance(target.position, transform.position);

        if (isTriggered)
        {
            if (hitPoint <= 0)
            {
                if (rampageCounter <= 2 && !secondPhase)
                {
                    Rampage();
                }
                else if (rampageCounter >= 2 && secondPhase && rampage == false)
                {
                    rampage = true;
                    //Play loud cry
                    Invoke("UpsideDown", .1f);
                }
            }

            if (!rampage)
            {
                if (!isLighted)
                {
                    if (changeDirection == false)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, bossRoomPosSX.transform.position, speed * Time.deltaTime);
                        if (transform.position.x <= bossRoomPosSX.transform.position.x)
                        {
                            transform.localScale = new Vector3(-originalBigness.x, transform.localScale.y, transform.localScale.z);
                            changeDirection = true;
                            facingRight = true;
                        }

                        if (source.isPlaying == false && firstEncounter == true) //steps sound
                        {
                            source.volume = Random.Range(0.1f, 0.15f);
                            source.pitch = Random.Range(0.8f, 1.5f);
                            source.Play();
                        }
                    }
                    else if (changeDirection == true)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, bossRoomPosDX.transform.position, speed * Time.deltaTime);

                        if (transform.position.x >= bossRoomPosDX.transform.position.x)
                        {
                            transform.localScale = new Vector3(originalBigness.x, transform.localScale.y, transform.localScale.z);
                            changeDirection = false;
                            facingRight = false;
                        }

                        if (source.isPlaying == false && firstEncounter == true) //steps sound
                        {
                            source.volume = Random.Range(0.1f, 0.15f);
                            source.pitch = Random.Range(0.8f, 1.5f);
                            source.Play();
                        }
                    }

                    if (!changeDirection && !facingRight)
                    {
                        facingRight = true;
                    }
                    else if (changeDirection && facingRight)
                    {
                        facingRight = false;
                    }

                }

                if (isLighted)
                {
                    Flip();
                    anim.SetBool("inRange", true);
                    if (!eventManager.IsPlayerOnLibrary || (upsideDown && eventManager.IsPlayerOnLibrary))
                    {
                        FirstPhase(distance);
                        if (target.position.y <= transform.position.y && anim.GetBool("lookUp"))
                        {
                            anim.SetBool("lookUp", false);
                        }
                        if (libraryCheck == true)
                        {
                            canTakeDamage = true;
                            libraryCheck = false;
                        }
                    }
                    else if (eventManager.IsPlayerOnLibrary && !upsideDown)
                    {
                        if (!anim.GetBool("lookUp") && distance <= transformRange)
                        {
                            anim.SetBool("lookUp", true);
                        }
                        else if (!anim.GetBool("lookUp") && distance > transformRange)
                        {
                            Flip();
                            Vector2 targetPos = new Vector2(target.position.x, transform.position.y);
                            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
                        }
                        else if (anim.GetBool("lookUp") && timeWithoutDamages >= 0)
                        {
                            canTakeDamage = false;
                            timeWithoutDamages -= 1.0f * Time.deltaTime;
                        }

                        if (timeWithoutDamages <= 0)
                        {
                            isLighted = false;
                            timeWithoutDamages = totalTimeWithoutDamages;
                            anim.SetBool("lookUp", false);
                            libraryCheck = true;
                        }

                    }
                }
            }

        }
    }

    private void CanTakeDamages()
    {
        canTakeDamage = true;
    }

    public override void EnemyReset()
    {
        anim.SetBool("reset", true);
        hitPoint = maxHitPoint;
        transform.position = respawnPoint;
        transform.localScale = originalBigness;
        anim.SetBool("death", false);
        anim.SetBool("inRange", false);
    }

    public override void Respawn()
    {
        if (transform.position.x != respawnPoint.x && target.position.x > dxRoomLimiter.transform.position.x || target.position.y > dxRoomLimiter.transform.position.y || target.position.x < sxRoomLimiter.transform.position.x || target.position.y < sxRoomLimiter.transform.position.y)
        {
            EnemyReset();
        }
    }

    public override void TakeDamage()
    {
        if (canTakeDamage == true)
        {
            base.TakeDamage();
        }

    }

    public override void OnTriggerStay2D(Collider2D collision)
    {
        if (hitPoint <= 0)
        {
            isTakingDamage = false;
        }
        else if (collision.tag == ("Player_LanternCollider") && hitPoint > 0)
        {
            isTakingDamage = true;
            TakeDamage();
        }
    }

    public override void DeathChecker()
    {
        if (canDie && hitPoint <= 0)
        {
            canTakeDamage = false;
            anim.SetBool("death", true);
            isDied = true;
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * -1f, transform.localScale.z);
            transform.DOScale(1f, 2f);
            transform.DOMoveY(landingPos, 2f);

            if (source.isPlaying == true && check == true)
                source.Stop();

            if (source.isPlaying == false && check == true) //deth sound
            {
                cameraMusic.clip = endBossFight;
                cameraMusic.Play();
                pickedSound = 4;
                gameObject.GetComponent<AudioSource>().clip = sounds[pickedSound];
                source.clip = sounds[pickedSound];
                source.pitch = Random.Range(0.8f, 1.5f);
                source.Play();
                check = false;
                Invoke("VictoryMusic", 2f);
            }

            if (source.isPlaying == false && check == false)
            {
                pickedSound = 6;
                gameObject.GetComponent<AudioSource>().clip = sounds[pickedSound];
                source.clip = sounds[pickedSound];
                source.Play();
            }
        }
    }

    private void FirstPhase(float distance)
    {
        if (distance > attackRange && anim.GetBool("inRange"))
        {
            anim.SetBool("attack", false);
            Flip();

            if (!anim.GetBool("buttAttack") && !anim.GetBool("slash"))
            {
                Vector2 targetPos = new Vector2(target.position.x, transform.position.y);
                transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            }

            if (source.isPlaying == false) //steps sound
            {
                pickedSound = 3;
                gameObject.GetComponent<AudioSource>().clip = sounds[pickedSound];
                source.volume = Random.Range(0.5f, 0.6f);
                source.pitch = Random.Range(0.8f, 1.5f);
                source.Play();
            }
        }
        else if (distance <= attackRange)
        {
            firstEncounter = false;
            anim.SetBool("attack", true);
            attackRandomizer = (Random.Range(0, 2));
            if (attackRandomizer == 0)
            {
                anim.SetBool("slash", false);
                anim.SetBool("buttAttack", true);
                if (source.isPlaying == false) //attack sound
                {
                    pickedSound = 0;
                    gameObject.GetComponent<AudioSource>().clip = sounds[pickedSound];
                    source.clip = sounds[pickedSound];
                    source.pitch = Random.Range(1f, 1.5f);
                    source.Play();

                }

            }
            else if (attackRandomizer == 1)
            {
                anim.SetBool("buttAttack", false);
                anim.SetBool("slash", true);
                if (source.isPlaying == false) //attack sound
                {
                    pickedSound = Random.Range(1, 3);
                    gameObject.GetComponent<AudioSource>().clip = sounds[pickedSound];
                    source.clip = sounds[pickedSound];
                    source.pitch = Random.Range(0.9f, 1.5f);
                    source.Play();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.tag == ("Player_CandleCollider") || collision.tag == ("Player_LanternCollider"))
        {
            isLighted = true;
        }


    }

    private void Rampage()
    {
        if (rampage == false)
        {
            anim.SetBool("buttAttack", false);
            anim.SetBool("slash", false);
            anim.SetBool("attack", false);
            speed = rampageSpeed;
            rampage = true;
            canTakeDamage = false;
            rampageCounter++;
        }

        if (changeDirection == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, bossRoomPosSX.transform.position, speed * Time.deltaTime);

            if (transform.position.x <= bossRoomPosSX.transform.position.x)
            {
                transform.localScale = new Vector3(-originalBigness.x, transform.localScale.y, transform.localScale.z);
                changeDirection = true;
                facingRight = true;
            }

            if (source.isPlaying == false ) //steps sound
            {
                pickedSound = 3;
                gameObject.GetComponent<AudioSource>().clip = sounds[pickedSound];
                source.volume = Random.Range(0.5f, 0.6f);
                source.pitch = Random.Range(1.2f, 1.5f);
                source.Play();
            }
        }
        else if (changeDirection == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, bossRoomPosDX.transform.position, speed * Time.deltaTime);

            if (transform.position.x >= bossRoomPosDX.transform.position.x)
            {
                transform.localScale = new Vector3(originalBigness.x, transform.localScale.y, transform.localScale.z);
                changeDirection = false;
                facingRight = false;
            }

            if (source.isPlaying == false ) //steps sound
            {
                Debug.Log("step");
                pickedSound = 3;
                gameObject.GetComponent<AudioSource>().clip = sounds[pickedSound];
                source.volume = Random.Range(0.5f, 0.6f);
                source.pitch = Random.Range(1.2f, 1.5f);
                source.Play();
            }
        }

        Invoke("RampageReset", rampageTime);
    }

    private void RampageReset()
    {
        rampage = false;
        canTakeDamage = true;
        hitPoint = maxHitPoint;
        speed = normalSpeed;
        isLighted = true;
        Flip();
        Vector2 targetPos = new Vector2(target.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        if (rampageCounter == 2)
        {
            secondPhase = true;
        }
    }

    private void UpsideDown()
    {
        transform.localScale = new Vector3 (transform.localScale.x, transform.localScale.y*-1f, transform.localScale.z);
        transform.DOMoveY(transform.position.y + 4.2395f, 0.5f);
        upsideDown = true;
        //transform.DOMoveY()
        hitPoint = maxHitPoint;
        canDie = true;
        isLighted = true;
        rampageCounter++;
        secondPhase = false;
        Invoke("ResetUpsideDown", 1f);

    }

    private void ResetUpsideDown()
    {
        rampage = false;
    }


    private void VictoryMusic()
    {

        pickedSound = 5;
        gameObject.GetComponent<AudioSource>().clip = sounds[pickedSound];
        source.clip = sounds[pickedSound];
        source.pitch = 1f;
        source.Play();
    }

    #region Animation Manager

    private void ButtAttackReset()
    {
        anim.SetBool("buttAttack", false);
    }

    private void SlashAttackReset()
    {
        anim.SetBool("slash", false);
    }

    private void ButtAttack()
    {
        attackCollider.enabled = !attackCollider.enabled;
    }

    private void SlashAttack()
    {
        SlashCollider.enabled = !SlashCollider.enabled;
    }

    private void ResetDeathReset()
    {
        anim.SetBool("reset", false);
    }

    private void DeathAttackReset()
    {
        attackCollider.enabled = false;
        SlashCollider.enabled = false;
    }

    #endregion
}

