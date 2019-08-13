using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Fungus;

public class Snakecat_Boss_Behaviour : MonoBehaviour
{

    [SerializeField] GameObject bossRoomPosDX;
    [SerializeField] GameObject bossRoomPosSX;
    [SerializeField] GameObject player;
    [SerializeField] GameObject eyeSX;
    [SerializeField] GameObject eyeDX;
    [SerializeField] GameObject lowAttackCollider;
    [SerializeField] GameObject highAttackCollider;
    [SerializeField] GameObject lantern;
    [SerializeField] GameObject candle;
    [SerializeField] GameObject rubble;
    [SerializeField] GameObject sxLimiter;
    [SerializeField] GameObject dxLimiter;


    [SerializeField] float attackRange;
    [SerializeField] float hitPoint;
    [SerializeField] float speed;

    [SerializeField] float minTimeStateChange;
    [SerializeField] float maxTimeStateChange;
    [SerializeField] float minInvisibilityTime;
    [SerializeField] float maxInvisibilityTime;

    [SerializeField] float enrageHitPoint;
    [SerializeField] float enrageEndingPosition;
    [SerializeField] float enrageMoveTime;
    [SerializeField] float enrageTime;
    [SerializeField] float speedAfterEnrage;

    [SerializeField] float minRubbleSize;
    [SerializeField] float maxRubbleSize;
    [SerializeField] float maxSpawnDelay;

    [SerializeField] Flowchart flowchart;
    [SerializeField] string dialogue;

    private List<GameObject> rubblesList;

    private bool invisibility;
    private bool isAttacking;
    private bool isTakingDamage;
    private bool facingLeft;
    private bool isGoingLeft;
    private bool isGoingRight;
    private bool highState;
    //TODO make it false
    private bool bossFightActive;
    private bool isPlayerRight;
    private bool isPlayerLeft;
    private bool enraging;
    private bool ceilingPhase;
    private bool rubblePhase;
    private bool checkScale;
    private bool isActivated;
    private bool check = false, check2 = false, check3 = false;
    private bool isDied = false;

    private float timeStateChange;
    private float invisibilityTime;
    private float playerDistance;

    private float enrageStartingPosition;
    private float rubbleSize;
    private float rubbleSpawnPosition;
    private float spawnDelay;


    private int actualRubbles;
    private int pickedSound;

    [SerializeField] AudioClip[] sounds;
    [SerializeField] AudioSource source;

    Animator anim;
    SpriteRenderer spriteRenderer;
    GameObject thisRubble;
    Animator playerAnim;
    PlayerMove_KT playerScript;
    ChangeWeapon_NN playerWeapon;
    Rigidbody2D playerRB;

    public bool BossFightActive { get => bossFightActive; set => bossFightActive = value; }


    void Start()
    {
        #region Variable Setting
        timeStateChange = Random.Range(minTimeStateChange, maxTimeStateChange);
        invisibilityTime = Random.Range(minInvisibilityTime, maxInvisibilityTime);
        spawnDelay = maxSpawnDelay;
        #endregion

        #region Component Declaration
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerAnim = player.GetComponent<Animator>();
        playerScript = player.GetComponent<PlayerMove_KT>();
        playerWeapon = player.GetComponent<ChangeWeapon_NN>();
        playerRB = player.GetComponent<Rigidbody2D>();
        #endregion


    }

    void Update()
    {
        BehaviourManager();
    }

    private void BehaviourManager()
    {
        if (bossFightActive)
        {
            if(isActivated)
            {
                if (check == false)
                {
                    pickedSound = 0;
                    gameObject.GetComponent<AudioSource>().clip = sounds[pickedSound];
                    source.clip = sounds[pickedSound];
                    source.pitch = Random.Range(1f, 1f);
                    source.Play();
                    check = true;
                }

                if (source.isPlaying == false && check2 == false)
                {

                    check2 = true;
                    Invoke("RandomSound", Random.Range(5f, 8f));

                }

                if (!enraging)
                {
                    playerDistance = Vector2.Distance(transform.position, player.transform.position);
                    BasicMovement();
                    StateManager();
                    InvisibilityManager();

                    if (playerDistance <= attackRange && !invisibility)
                    {
                        AttackManager();
                    }
                    else if (playerDistance > attackRange && !invisibility)
                    {
                        TrackingMovement();
                    }         
                }
                Enrage();
            }
            DeathChecker();
        }
    }

    private void BasicMovement()
    {
        if (invisibility == true)
        {
            if (!facingLeft)
            {
                transform.position = Vector2.MoveTowards(transform.position, bossRoomPosDX.transform.position, speed * Time.deltaTime);
                isGoingRight = true;
                isGoingLeft = false;

                if (transform.position.x >= bossRoomPosDX.transform.position.x)
                {
                    transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);
                    facingLeft = true;
                    isGoingRight = false;
                }

            }
            else if (facingLeft)
            {
                transform.position = Vector2.MoveTowards(transform.position, bossRoomPosSX.transform.position, speed * Time.deltaTime);
                isGoingLeft = true;
                isGoingRight = false;

                if (transform.position.x <= bossRoomPosSX.transform.position.x)
                {
                    transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);
                    facingLeft = false;
                    isGoingLeft = false;
                }
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
    }

    private void TrackingMovement()
    {
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
    
    private void Enrage()
    {
        if (hitPoint <= enrageHitPoint && ceilingPhase == false)
        {

            invisibility = false;

            enraging = true;

            anim.SetBool("climbing", true);
            anim.SetBool("lowState", false);
            anim.SetBool("highState", false);

            enrageStartingPosition = transform.position.y;

            transform.DOMoveY(enrageEndingPosition, enrageMoveTime);
        }

        if (enraging)
        {
            ObjectsFalling();
        }
    }

    private void ObjectsFalling()
    {
        ceilingPhase = true;
        rubblePhase = true;

        enrageTime -= 1f * Time.deltaTime;

        if (rubblePhase)
        {
            spawnDelay -= 1f * Time.deltaTime;

            if (spawnDelay <= 0)
            {
                spawnDelay = maxSpawnDelay;
                rubbleSpawnPosition = Random.Range(sxLimiter.transform.position.x, dxLimiter.transform.position.x);
                rubbleSize = Random.Range(minRubbleSize, maxRubbleSize);
                thisRubble = (GameObject)Instantiate(rubble, new Vector3(rubbleSpawnPosition, transform.position.y, transform.position.z), Quaternion.identity);
                thisRubble.transform.localScale = new Vector3(rubbleSize, rubbleSize, rubbleSize);
                
                pickedSound = Random.Range(6, 8);
                gameObject.GetComponent<AudioSource>().clip = sounds[pickedSound];
                source.clip = sounds[pickedSound];
                source.pitch = Random.Range(1f, 1f);
                if (source.isPlaying == false)
                    source.Play();
                Destroy(thisRubble, 3f);
            }
        }

        if (enrageTime <= 0)
        {
            rubblePhase = false;
            EndingEnrage();
        }
    }

    private void EndingEnrage()
    {
        anim.SetBool("goDown", true);
        anim.SetBool("climbing", false);

        if (!checkScale)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * -1, transform.localScale.z);
            checkScale = true;
        }

        transform.DOMoveY(enrageStartingPosition, enrageMoveTime);

        StartCoroutine(PostEnrage());
    }

    private void DeathChecker()
    {
        if (hitPoint <= 0)
        {
            invisibility = false;
            spriteRenderer.enabled = true;
            anim.SetBool("death", true);
            isActivated = false;
            isDied = true;

            pickedSound = 3;
            gameObject.GetComponent<AudioSource>().clip = sounds[pickedSound];
            source.clip = sounds[pickedSound];
            source.pitch = Random.Range(1f, 1f);
            if (check3 == false)
            {
                source.Play();
                check3 = true;
            }
        }
    }

    private void InvisibilityManager()
    {
        invisibilityTime -= 1f * Time.deltaTime;

        if (invisibilityTime <= 0)
        {
            invisibility = false;
        }

        if (invisibility)
        {
            spriteRenderer.enabled = false;
            eyeDX.SetActive(true);
            eyeSX.SetActive(true);
        }
        else if (!invisibility)
        {
            spriteRenderer.enabled = true;
            eyeDX.SetActive(false);
            eyeSX.SetActive(false);
        }

        if (candle.activeSelf || lantern.activeSelf)
        {
            eyeDX.SetActive(false);
            eyeSX.SetActive(false);
        }
    }

    private void AttackManager()
    {
        anim.SetBool("attack", true);
        if (source.isPlaying == true ) 
        {
            pickedSound = 1;
            gameObject.GetComponent<AudioSource>().clip = sounds[pickedSound];
            source.clip = sounds[pickedSound];
            source.pitch = Random.Range(1f, 1f);
            if(source.isPlaying == false)
                source.Play();

        }

        else if (source.isPlaying == false)
        {
            pickedSound = 1;
            gameObject.GetComponent<AudioSource>().clip = sounds[pickedSound];
            source.clip = sounds[pickedSound];
            source.pitch = Random.Range(1f, 1f);
            source.Play();
        }
    }

    private void StateManager()
    {
        timeStateChange -= 1f *Time.deltaTime;

        if (timeStateChange <= 0)
        {
            StateChange();
        }
    }

    private void StateChange()
    {
        highState = !highState;
        timeStateChange = Random.Range(minTimeStateChange, maxTimeStateChange);

        if (!highState)
        {
            anim.SetBool("lowState", true);
            anim.SetBool("highState", false);
        }
        else if (highState)
        {
            anim.SetBool("lowState", false);
            anim.SetBool("highState", true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (hitPoint <= 0)
        {
            isTakingDamage = false;
        }
        else if (collision.tag == ("Player_LanternCollider") && hitPoint > 0 && !invisibility)
        {
            isTakingDamage = true;
            TakeDamage();
        }

        if (collision.tag == "Player" && isDied == true && Input.GetButtonDown("Interaction"))
        {

            playerAnim.enabled = false;
            playerScript.enabled = false;
            playerWeapon.enabled = false;
            playerRB.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            flowchart.ExecuteBlock(dialogue);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == ("Player_LanternCollider") || collision.tag == ("Player_CandleCollider") && isAttacking == false)
        {
            eyeDX.SetActive(true);
            eyeSX.SetActive(true);
        }
        else if (collision.tag == ("Player_LanternCollider"))
        {
            isTakingDamage = false;
        }
    }

    public virtual void TakeDamage()
    {
        hitPoint -= 1.0f * Time.deltaTime;
        StartCoroutine(TakingDamage());
    }

    private IEnumerator TakingDamage()
    {
        while (isTakingDamage && !invisibility)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(.1f);
        }
    }

    private IEnumerator PostEnrage()
    {
        yield return new WaitForSeconds(enrageMoveTime);
        if (enraging == true)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * -1, transform.localScale.z);
            enraging = false;
            anim.SetBool("goDown", false);
            speed = speedAfterEnrage;
        }
    }

    private void RandomSound()
    {
        pickedSound = 2;
        gameObject.GetComponent<AudioSource>().clip = sounds[pickedSound];
        source.clip = sounds[pickedSound];
        source.pitch = Random.Range(1f, 1f);
        source.Play();
        check2 = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    #region Animation Methods

    private void Attack()
    {
        if (highState)
        {
            highAttackCollider.SetActive(true);
        }
        else if (!highState)
        {
            lowAttackCollider.SetActive(true);
        }
    }

    private void ResetAttack()
    {
        anim.SetBool("attack", false);
        lowAttackCollider.SetActive(false);
        highAttackCollider.SetActive(false);
        invisibilityTime = Random.Range(minInvisibilityTime, maxInvisibilityTime);
        Invoke("Invisibility", 0.5f);
    }

    private void Invisibility()
    {
        invisibility = true;       
    }

    private void Activation()
    {
        isActivated = true;
    }

    #endregion
}
