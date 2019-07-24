using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snakecat_Boss_Behaviour : MonoBehaviour
{

    [SerializeField] GameObject bossRoomPosDX;
    [SerializeField] GameObject bossRoomPosSX;
    [SerializeField] GameObject player;

    [SerializeField] float hitPoint;
    [SerializeField] float speed;
    [SerializeField] float minTimeStateChange;
    [SerializeField] float maxTimeStateChange;

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
    
    Animator anim;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        #region Variable Setting
        timeStateChange = Random.Range(minTimeStateChange, maxTimeStateChange);
        #endregion

        #region Component Declaration
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        if (bossFightActive)
        {
            TrackingMovement();
            StateManager();
        }
    }

    private void BasicMovement()
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

    private void AttackManager()
    {
        if (!highState)
        {

        }

        else if (highState)
        {

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

    public virtual void TakeDamage()
    {
        hitPoint -= 1.0f * Time.deltaTime;
        StartCoroutine(TakingDamage());
    }

    private IEnumerator TakingDamage()
    {
        while (isTakingDamage)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(.1f);
        }
    }
}
