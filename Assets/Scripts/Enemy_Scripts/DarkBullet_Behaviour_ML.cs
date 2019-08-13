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


    private void Update()
    {
        if (transform.position.x < sxLimiter.transform.position.x || transform.position.x > dxLimiter.transform.position.x)
        {
            Destroy(gameObject);
        }
    }
}
