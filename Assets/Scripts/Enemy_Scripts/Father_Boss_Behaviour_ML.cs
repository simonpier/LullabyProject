using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Father_Boss_Behaviour_ML : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject darkBullet;
    [SerializeField] GameObject bossRoomPosDX;
    [SerializeField] GameObject bossRoomPosSX;
    [SerializeField] GameObject attackCollider;

    [SerializeField] float xBulletOffset;
    //the time that a bullet takes to travel LERPCONST units 
    [SerializeField] float bulletLerpTime;
    [SerializeField] float speed;
    [SerializeField] float sphereOffset;
    [SerializeField] float meleeRange;
    [SerializeField] float distanceRange;
    [SerializeField] float throwCooldown;

    [SerializeField] AudioClip[] sounds;
    [SerializeField] AudioSource source;

    GameObject instanceBullet;
    Animator anim;
    FatherDeathCutscene_ML fatherCutscene;


    private bool facingLeft = true;
    private bool isGoingRight;
    private bool isGoingLeft;
    private bool animationCheck;
    private bool death;
    private bool activation;
    private bool check = false, check2 = false;

    private int lanternCount;

    private float bulletDistance;
    private float playerDistance;
    private float actualThrowCooldown;
    private float lerpCalculator;
    private const float LERPCONST = 28f;

    private int pickedSound;

    public int LanternCount { get => lanternCount; set => lanternCount = value; }
    public bool Death { get => death; set => death = value; }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        fatherCutscene = GetComponent<FatherDeathCutscene_ML>();

        actualThrowCooldown = throwCooldown;
        lerpCalculator = bulletLerpTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(activation)
        {
            if (!death)
            {
                StateManager();
            }
            else if (death)
            {
                if (source.isPlaying == false && check2 == false)
                {
                    source.Stop();
                    pickedSound = 8;
                    gameObject.GetComponent<AudioSource>().clip = sounds[pickedSound];
                    source.clip = sounds[pickedSound];
                    source.pitch = 1;
                    source.volume = 1f;
                    source.Play();
                    check2 = true;
                }
                fatherCutscene.StartCoroutine(fatherCutscene.FatherDeath());
            }

            if (check == false)
            {

                check = true;
                Invoke("RandomSound", Random.Range(5f, 8f));

            }
        }
    }

    private void StateManager()
    {
        playerDistance = Vector2.Distance(transform.position, player.transform.position);

        if (playerDistance <= meleeRange && !animationCheck)
        {
            MeleeAttack();
            actualThrowCooldown -= 1f * Time.deltaTime;
        }
        else if (playerDistance > meleeRange && !animationCheck)
        {
            if (actualThrowCooldown <= 0 && playerDistance >= distanceRange)
            {
                ThrowAttack();
            }
            else if (playerDistance < distanceRange || (playerDistance >= distanceRange && actualThrowCooldown > 0))
            {
                BasicMovement();
                actualThrowCooldown -= 1f * Time.deltaTime;
            }
        }
    }

    private void MeleeAttack()
    {
        anim.SetBool("meleeAttack", true);
        if (source.isPlaying == false)
        {
            pickedSound = 1;
            gameObject.GetComponent<AudioSource>().clip = sounds[pickedSound];
            source.clip = sounds[pickedSound];
            source.pitch = Random.Range(0.8f, 1.5f);
            source.volume = 1f;
            source.Play();
        }

        if (source.isPlaying == true)
        {
            pickedSound = 1;
            gameObject.GetComponent<AudioSource>().clip = sounds[pickedSound];
            source.clip = sounds[pickedSound];
            source.pitch = Random.Range(1f, 1f);
            source.volume = 1f;
            if (source.isPlaying == false)
                source.Play();

        }
    }

    private void ThrowAttack()
    {
        anim.SetBool("throwAttack", true);
        if (source.isPlaying == false)
        {
            pickedSound = 0;
            gameObject.GetComponent<AudioSource>().clip = sounds[pickedSound];
            source.clip = sounds[pickedSound];
            source.pitch = Random.Range(0.8f, 1.5f);
            source.volume = 1f;
            source.Play();
        }

        if (source.isPlaying == true)
        {
            pickedSound = 0;
            gameObject.GetComponent<AudioSource>().clip = sounds[pickedSound];
            source.clip = sounds[pickedSound];
            source.pitch = Random.Range(1f, 1f);
            source.volume = 1f;
            if (source.isPlaying == false)
                source.Play();

        }
    }

    private void BasicMovement()
    {
        anim.SetBool("meleeAttack", false);
        anim.SetBool("throwAttack", false);

        if (source.isPlaying == false)
        {
            pickedSound = Random.Range(2 , 5);
            gameObject.GetComponent<AudioSource>().clip = sounds[pickedSound];
            source.clip = sounds[pickedSound];
            source.volume = 0.3f;
            source.pitch = Random.Range(1f, 1f);
            source.Play();
        }

        if (player.transform.position.x <= transform.position.x)
        {
            if (!facingLeft)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);
            }

            facingLeft = true;
        }
        else if (player.transform.position.x > transform.position.x)
        {
            if (facingLeft)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);
            }

            facingLeft = false;
        }

        if (!facingLeft)
        {
            Vector2 targetPos = new Vector2(player.transform.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            isGoingRight = true;
            isGoingLeft = false;
        }
        else if (facingLeft)
        {
            Vector2 targetPos = new Vector2(player.transform.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            isGoingLeft = true;
            isGoingRight = false;
        }

        if (isGoingRight && facingLeft)
        {
            facingLeft = false;
        }
        else if (isGoingLeft && !facingLeft)
        {
            facingLeft = true;
        }
    }

    public void Activation()
    {
        activation = true;
        anim.SetBool("activation", true);
        anim.SetBool("idle", true);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, meleeRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, distanceRange);

    }

    private void RandomSound()
    {
        pickedSound = Random.Range(6 , 8);
        gameObject.GetComponent<AudioSource>().clip = sounds[pickedSound];
        source.clip = sounds[pickedSound];
        source.pitch = Random.Range(1f, 1f);
        source.volume = 0.3f;
        source.Play();
        check = false;
    }

    #region Animation Methods

    private void Melee()
    {
        attackCollider.SetActive(true);
    }

    private void MeleeReset()
    {
        attackCollider.SetActive(false);

    }

    private void MeleeBoolReset()
    {
        anim.SetBool("meleeAttack", false);
    }

    private void RangeAttack()
    {
        if (player.transform.position.x <= transform.position.x)
        {
            instanceBullet = Instantiate(darkBullet, new Vector3(transform.position.x - sphereOffset, transform.position.y, transform.position.z), Quaternion.identity);
            bulletDistance = (Vector2.Distance(instanceBullet.transform.position, bossRoomPosSX.transform.position));
            lerpCalculator = bulletLerpTime * bulletDistance / LERPCONST;
            instanceBullet.transform.DOMoveX(bossRoomPosSX.transform.position.x - xBulletOffset, lerpCalculator);
        }
        else if (player.transform.position.x > transform.position.x)
        {
            instanceBullet = Instantiate(darkBullet, new Vector3(transform.position.x + sphereOffset, transform.position.y, transform.position.z), Quaternion.identity);
            bulletDistance = (Vector2.Distance(instanceBullet.transform.position, bossRoomPosDX.transform.position));
            lerpCalculator = bulletLerpTime * bulletDistance / LERPCONST;
            instanceBullet.transform.DOMoveX(bossRoomPosDX.transform.position.x + xBulletOffset, lerpCalculator);
        }

        instanceBullet.transform.localScale = new Vector3(instanceBullet.transform.localScale.x * transform.localScale.x, instanceBullet.transform.localScale.y, instanceBullet.transform.localScale.z);
        instanceBullet.SetActive(true);

        actualThrowCooldown = throwCooldown;

    }

    private void RangeAttackReset()
    {
        anim.SetBool("throwAttack", false);
    }

    private void CheckActive()
    {
        animationCheck = true;
    }

    private void CheckReset()
    {
        animationCheck = false;
    }

    #endregion
}
