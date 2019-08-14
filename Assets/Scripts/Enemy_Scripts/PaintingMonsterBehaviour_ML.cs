using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PaintingMonsterBehaviour_ML : EnemyController_ML
{
    [SerializeField] AudioManager audio;
    [SerializeField] AudioSource source;
    public AudioClip[] sounds;
    private bool check = true, check2 = false;
    public bool isTriggered = false;

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
    }

    public override void DeathChecker()
    {
        if (canDie && hitPoint <= 0 && !canFly)
        {
            anim.SetBool("death", true);
            isDied = true;


            if (source.isPlaying == false && check2 == false)
            {
                pickedSound = 1;
                gameObject.GetComponent<AudioSource>().clip = sounds[pickedSound];
                source.clip = sounds[pickedSound];
                source.volume = Random.Range(0.3f, 0.4f);
                source.pitch = Random.Range(0.8f, 1.5f);
                source.Play();
                check2 = true;
            } 
            isTriggered = false;
        }
    }

    public override void OnTriggerStay2D(Collider2D collision)
    {
        base.OnTriggerStay2D(collision);

        if ((collision.gameObject.tag == "Player") && Input.GetButtonDown("Interaction"))
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
            isTriggered = true;
        }
    }

    public override void TargetTracking()
    {
        float distance = Vector2.Distance(target.position, transform.position);

        if (distance <= attackRange && isTriggered == true)
        {
            anim.SetBool("attack", true);
            if (source.isPlaying == false && isDied == false && isTriggered == true)
            {
                pickedSound = 0;
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

