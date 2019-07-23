using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampMonsterBehaviour_ML : EnemyController_ML
{
    [SerializeField] GameObject playerCandle;
    [SerializeField] GameObject playerLantern;

    [SerializeField] AudioManager audio;
    [SerializeField] AudioSource source;
    public AudioClip[] sounds;

    private bool check = true, check2 = false;
    private int pickedSound;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        CandleCheck();
    }

    public override void DeathChecker()
    {
        if (canDie && hitPoint <= 0 && !canFly)
        {
            source.Stop();
            anim.SetBool("death", true);
            isDied = true;
            DefeatSound();
        }
    }

    private void CandleCheck()
    {
        if (!playerCandle.activeSelf && !playerLantern.activeSelf)
        {
            anim.SetBool("isTransformed", false);
            anim.ResetTrigger("transformation");
            anim.SetBool("attack", false);
            anim.SetBool("reset", true);
            anim.SetBool("death", true);
            DefeatSound();
            check = true;
        }
    }

    public override void TargetTracking()
    {
        if (!isDied)
        {
            float distance = Vector2.Distance(target.position, transform.position);

            if(anim.GetBool("isTransformed"))
            {
                check2 = true;
                if (canMove && distance > attackRange)
                {
                    Flip();
                    if (anim.GetBool("isTransformed") && !anim.GetBool("attack"))
                    {
                        //If the enemy can fly allow it to move also in y axis
                        if (canFly)
                        {
                            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
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
                    anim.SetBool("attack", true);
                    if (source.clip.name == "LLB_VM_SFX_FNL_LampWalkLoop")
                    {

                        source.Stop();

                    }
                    

                    if (source.isPlaying == false)
                    {
                        pickedSound = 1;
                        gameObject.GetComponent<AudioSource>().clip = sounds[pickedSound];
                        source.clip = sounds[pickedSound];
                        source.volume = Random.Range(0.8f, 1f);
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

    public override void OnTriggerStay2D(Collider2D collision)
    {
    }

    public override void OnTriggerExit2D(Collider2D collision)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player_CandleCollider" || collision.tag ==  "Player_LanternCollider")
        {
            anim.SetBool("reset", false);
            anim.SetBool("death", false);
            anim.SetTrigger("transformation");
            InvokeSound();
        }
    }

    void InvokeSound()
    {
        if (check == true)
        {

            audio.PlaySound("lamp transformation");
            check = false;

        }
    }

    void DefeatSound()
    {
        if (check2 == true)
        {
            audio.PlaySound("lamp defeat");
            check2 = false;
        }
    }
}
