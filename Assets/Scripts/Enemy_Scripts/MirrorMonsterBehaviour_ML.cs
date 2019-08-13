using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorMonsterBehaviour_ML : EnemyController_ML
{
    [SerializeField] GameObject playerCandle;
    [SerializeField] GameObject playerLantern;
    [SerializeField] AudioManager audio;
    [SerializeField] AudioSource source;
    public AudioClip[] sounds;

    private int pickedSound;

    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }


    public override void OnTriggerStay2D(Collider2D collision)
    {
        base.OnTriggerStay2D(collision);

        if ((collision.gameObject.tag == "Player") && Input.GetButtonDown("Interaction") && !playerCandle.activeSelf && !playerLantern.activeSelf && !isDied)
        {
            anim.SetBool("reset", false);
            anim.SetTrigger("transformation");

            if (source.isPlaying == false)
            {
                pickedSound = 0;
                gameObject.GetComponent<AudioSource>().clip = sounds[pickedSound];
                source.clip = sounds[pickedSound];
                source.volume = Random.Range(0.3f, 0.4f);
                source.pitch = Random.Range(0.8f, 1.5f);
                source.Play();

            }
        }
    }

    public override void TargetTracking()
    {
        float distance = Vector2.Distance(target.position, transform.position);

        if (distance <= attackRange)
        {
            anim.SetBool("attack", true);

            if (source.isPlaying == false && isDied == false)
            {
                pickedSound = 1;
                gameObject.GetComponent<AudioSource>().clip = sounds[pickedSound];
                source.clip = sounds[pickedSound];
                source.volume = Random.Range(0.3f, 0.4f);
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
