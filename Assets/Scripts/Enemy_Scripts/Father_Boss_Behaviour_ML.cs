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
    [SerializeField] float bulletLerpTime;
    [SerializeField] float speed;
    [SerializeField] float sphereOffset;
    [SerializeField] float meleeRange;
    [SerializeField] float throwCooldown;

    GameObject instanceBullet;
    Animator anim;
    FatherDeathCutscene_ML fatherCutscene;

    private bool facingLeft = true;
    private bool isGoingRight;
    private bool isGoingLeft;
    private bool animationCheck;
    private bool death;
    private bool activation;

    private int lanternCount;

    private float playerDistance;
    private float actualThrowCooldown;

    public int LanternCount { get => lanternCount; set => lanternCount = value; }
    public bool Death { get => death; set => death = value; }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        fatherCutscene = GetComponent<FatherDeathCutscene_ML>();

        actualThrowCooldown = throwCooldown;
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
                fatherCutscene.StartCoroutine(fatherCutscene.FatherDeath());
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
            if (actualThrowCooldown <= 0)
            {
                ThrowAttack();
 
            }
            else if (actualThrowCooldown > 0)
            {
                BasicMovement();
                actualThrowCooldown -= 1f * Time.deltaTime;
            }
        }
    }

    private void MeleeAttack()
    {
        anim.SetBool("meleeAttack", true);
    }

    private void ThrowAttack()
    {
        anim.SetBool("throwAttack", true);
    }

    private void BasicMovement()
    {
        anim.SetBool("meleeAttack", false);
        anim.SetBool("throwAttack", false);

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
            instanceBullet.transform.DOMoveX(bossRoomPosSX.transform.position.x - xBulletOffset, bulletLerpTime);
        }
        else if (player.transform.position.x > transform.position.x)
        {
            instanceBullet = Instantiate(darkBullet, new Vector3(transform.position.x + sphereOffset, transform.position.y, transform.position.z), Quaternion.identity);
            instanceBullet.transform.DOMoveX(bossRoomPosDX.transform.position.x + xBulletOffset, bulletLerpTime);
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
