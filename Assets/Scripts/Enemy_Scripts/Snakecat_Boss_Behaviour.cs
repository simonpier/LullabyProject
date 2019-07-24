using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField] float attackRange;
    [SerializeField] float hitPoint;
    [SerializeField] float speed;
    [SerializeField] float minTimeStateChange;
    [SerializeField] float maxTimeStateChange;
    [SerializeField] float minInvisibilityTime;
    [SerializeField] float maxInvisibilityTime;

    private bool invisibility;
    private bool isAttacking;
    private bool isTakingDamage;
    private bool facingLeft;
    private bool isGoingLeft;
    private bool isGoingRight;
    private bool highState;
    //TODO make it false
    private bool bossFightActive = true;
    private bool isPlayerRight;
    private bool isPlayerLeft;
    private float timeStateChange;
    private float invisibilityTime;
    private float playerDistance;
    
    Animator anim;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        #region Variable Setting
        timeStateChange = Random.Range(minTimeStateChange, maxTimeStateChange);
        invisibilityTime = Random.Range(minInvisibilityTime, maxInvisibilityTime);
        #endregion

        #region Component Declaration
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        else if (player.transform.position.x >= transform.position.x)
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

    public virtual void OnDrawGizmosSelected()
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
        invisibilityTime = Random.Range(minInvisibilityTime, maxInvisibilityTime);
        Invoke("Invisibility", 0.5f);
    }

    private void Invisibility()
    {
        invisibility = true;       
    }

    #endregion
}
