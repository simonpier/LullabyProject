using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController_ML : MonoBehaviour
{
    //Enemy's range
    [SerializeField] protected float lookRadius = 10f;
    //Enemy's movement speed
    [SerializeField] protected float speed = 5.0f;
    //Indicates if the enemy can fly
    [SerializeField] protected bool canFly = false;

    protected Transform target;

    public virtual void Start()
    {
        //TODO Should be replaced with an instance of the player for more optimization
        //Reference to the player
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public virtual void Update()
    {
        TargetTracking();
    }


    //Regulates player tracking
    public virtual void TargetTracking()
    {
        float distance = Vector2.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            //If the enemy can fly allow it to move also in y axis
            if (canFly == true)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
            else
            {
                Vector2 targetPos = new Vector2(target.position.x, 0);
                transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            }

        }
    }

    //Allow to see the enemy's range through in the scene
    public virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
