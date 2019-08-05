using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChandelierMonsterBehaviour_ML : EnemyController_ML
{
    [SerializeField] GameObject playerCandle;
    [SerializeField] GameObject playerLantern;
    [SerializeField] GameObject candleColliderDetector;

    ChandelierCandleDetection_ML candleDetection;

    [SerializeField] AudioManager audio;
    [SerializeField] AudioSource source;
    public AudioClip[] sounds;

    private bool check = true, check2 = true;
    private int pickedSound;

    public bool IsDied { get => isDied; protected set => IsDied = value; }

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        candleDetection = candleColliderDetector.GetComponent<ChandelierCandleDetection_ML>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        CandleCheck();
    }

    public override void TargetTracking()
    {
        if (!isDied)
        {
            float distance = Vector2.Distance(target.position, transform.position);

            if (distance <= transformRange)
            {
                anim.SetBool("reset", false);
                anim.SetTrigger("transformation");
            }

            if (anim.GetBool("isTransformed"))
            {
                if (canMove && distance > attackRange)
                {
                    Flip();
                    if (!anim.GetBool("attack"))
                    {
                        //If the enemy can fly allow it to move also in y axis
                        if (canFly)
                        {
                            Vector2 targetPos = new Vector2(target.position.x, target.position.y + yDistance);
                            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
                        }
                        else if (!canFly)
                        {
                            Vector2 targetPos = new Vector2(target.position.x, transform.position.y);
                            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
                            if (source.isPlaying == false)
                            {
                                pickedSound = 0;
                                gameObject.GetComponent<AudioSource>().clip = sounds[pickedSound];
                                source.clip = sounds[pickedSound];
                                source.volume = Random.Range(0.8f, 1f);
                                source.pitch = Random.Range(0.8f, 1.5f);
                                source.Play();

                            }
                        }
                    }
                }
                else if (distance <= attackRange)
                {
                    anim.SetBool("idle", false);
                    anim.SetBool("attack", true);
                    if (source.isPlaying == false)
                    {
                        pickedSound = 1;
                        gameObject.GetComponent<AudioSource>().clip = sounds[pickedSound];
                        source.clip = sounds[pickedSound];
                        source.volume = Random.Range(1f, 1f);
                        source.pitch = Random.Range(0.8f, 1.5f);
                        source.Play();
                    }
                }
                else if (!canMove && distance > attackRange)
                {
                    anim.SetBool("idle", true);
                }
            }
        }
    }

    public override void DeathChecker()
    {
        if (canDie && hitPoint <= 0 && !canFly)
        {
            anim.SetBool("death", true);
            isDied = true;
            DefeatSound();
        }
    }

    private void CandleCheck()
    {
        if (!playerCandle.activeSelf && !playerLantern.activeSelf && !isDied)
        {
            anim.SetBool("isTransformed", false);
            anim.ResetTrigger("transformation");
            anim.SetBool("attack", false);
            anim.SetBool("reset", true);
            anim.SetBool("death", true);
            
        }

        if (candleDetection.IsCandleColliding || candleDetection.IsLanternColliding && !isDied)
        {
            anim.SetBool("reset", false);
            anim.SetBool("death", false);
            anim.SetTrigger("transformation");
            InvokeSound();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    public override void OnTriggerStay2D(Collider2D collision)
    {
        base.OnTriggerStay2D(collision);

        if ((collision.tag == "Player_CandleCollider" || collision.tag == "Player_LanternCollider") && !isDied)
        {
            anim.SetBool("reset", false);
            anim.SetBool("death", false);
            anim.SetTrigger("transformation");
        }

    }

    void InvokeSound()
    {
        if (check == true)
        {

            audio.PlaySound("chandelier transformation");
            check = false;

        }
    }

    void DefeatSound()
    {
        if (check2 == true)
        {
            audio.PlaySound("chandelier defeat");
            check2 = false;
        }
    }
}
