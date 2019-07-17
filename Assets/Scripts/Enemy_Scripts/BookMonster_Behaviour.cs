using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BookMonster_Behaviour : EnemyController_ML
{

    [SerializeField] AudioManager audio;
    [SerializeField] AudioSource source;
    public AudioClip[] sounds;

    private bool check = true, check2 = true;
    private int pickedSound;

    public override void DeathChecker()
    {
        if (canDie && hitPoint <= 0)
        {
            anim.SetBool("death", true);
            isDied = true;
            transform.DOMoveY(respawnPoint.y, 1.5f);
            Invoke("DefeatSound", 0f);
            Invoke("DeathSound", 1.45f);
        }
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
                            if (source.isPlaying == false)
                            {
                                pickedSound = Random.Range(0, 2);
                                gameObject.GetComponent<AudioSource>().clip = sounds[pickedSound];
                                source.clip = sounds[pickedSound];
                                source.volume = Random.Range(0.1f, 0.15f);
                                source.pitch = Random.Range(0.8f, 1.5f);
                                source.Play();

                            }
                        }
                        else if (!canFly)
                        {
                            Vector2 targetPos = new Vector2(target.position.x, transform.position.y);
                            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
                        }
                    }
                }
                else if (distance <= attackRange)
                {
                    anim.SetBool("idle", false);
                    anim.SetBool("attack", true);
                    if (source.isPlaying == false)
                    {
                        pickedSound = 2;
                        gameObject.GetComponent<AudioSource>().clip = sounds[pickedSound];
                        source.clip = sounds[pickedSound];
                        source.volume = Random.Range(0.1f, 0.15f);
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

    void DeathSound()
    {
        if (check == true)
        {
            
            audio.PlaySound("book impact");
            check= false;

        }
    }

    void DefeatSound()
    {
        if (check2 == true)
        {
            audio.PlaySound("book defeat");
            check2 = false;
        }
    }
}

