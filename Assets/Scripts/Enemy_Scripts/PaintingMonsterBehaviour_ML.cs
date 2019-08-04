using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingMonsterBehaviour_ML : EnemyController_ML
{
    //[SerializeField] AudioManager audio;
    //[SerializeField] AudioSource source;
    //public AudioClip[] sounds;
    private bool check = true, check2 = true, isTriggered = false;
    //private int pickedSound;

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
            //DefeatSound();
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
            //InvokeSound();
            isTriggered = true;
        }
    }

    public override void TargetTracking()
    {
        float distance = Vector2.Distance(target.position, transform.position);

        if (distance <= attackRange && isTriggered == true)
        {
            anim.SetBool("attack", true);
            //if (source.isPlaying == false)
            //{
            //    pickedSound = 0;
            //    gameObject.GetComponent<AudioSource>().clip = sounds[pickedSound];
            //    source.clip = sounds[pickedSound];
            //    source.volume = Random.Range(0.1f, 0.15f);
            //    source.pitch = Random.Range(0.8f, 1.5f);
            //    source.Play();

            //}
        }
        else if (!canMove && distance > attackRange)
        {
            anim.SetBool("idle", true);
        }
    }

    //void InvokeSound()
    //{
    //    if (check == true)
    //    {

    //        audio.PlaySound("door transformation");
    //        check = false;

    //    }
    //}

    //void DefeatSound()
    //{
    //    if (check2 == true)
    //    {
    //        audio.PlaySound("book defeat");
    //        check2 = false;
    //    }
    //}
}

