using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DarkBullet_Behaviour_ML : MonoBehaviour
{
    [SerializeField] GameObject sxLimiter;
    [SerializeField] GameObject dxLimiter;
    [SerializeField] GameObject player;

    [SerializeField] float xOffset;
    [SerializeField] float lerpTime;
    [SerializeField] float alphaReduction;

    SpriteRenderer spriteRenderer;

    float alpha = 1f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (transform.position.x < sxLimiter.transform.position.x || transform.position.x > dxLimiter.transform.position.x)
        {
            Destroy(gameObject);
        }
        if (spriteRenderer.color.a <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player_LanternCollider")
        {
            alpha -= alphaReduction * Time.deltaTime;

            spriteRenderer.DOFade(alpha, 0f);
        }
    }
}
