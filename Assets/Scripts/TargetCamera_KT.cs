using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;


//this script attach camera if camera target player
public class TargetCamera_KT : MonoBehaviour
{
    PlayerMove_KT player;
    PositionConstraint constraint;
    Camera cam;

    Vector2 offset;
    float _z;

    ConstraintSource constSource;

    bool doneInitialize = false;
    
    void Start()
    {
        if (!doneInitialize) Initialize();
    }

    void Initialize()
    {
        player = PlayerMove_KT.Instance;
        constraint = GetComponent<PositionConstraint>();
        cam = GetComponent<Camera>();
        _z = -transform.position.z;
        offset = this.transform.position - player.transform.position;

        constSource = new ConstraintSource();
        constSource.sourceTransform = player.transform;
        constSource.weight = 1;
        constraint.AddSource(constSource);
        constraint.translationOffset = this.transform.position - player.transform.position;
        constraint.enabled = true;
        constraint.constraintActive = true;
        constraint.translationAxis = Axis.X | Axis.Y | Axis.Z;

        doneInitialize = true;
    }

    void Update()
    {
        if ((constraint.translationAxis & Axis.X) != Axis.None)
        {
            var pos = cam.ScreenToWorldPoint(new Vector3(0, Screen.height, _z));
            Vector2 distance = new Vector2(pos.x, pos.y) - player.SXLimite;
            if (distance.x < 0)
            {
                constraint.translationAxis &= Axis.Y | Axis.Z;
                transform.position = new Vector3(transform.position.x - distance.x, transform.position.y, transform.position.z);
            }

            pos = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0, _z));
            distance = new Vector2(pos.x, pos.y) - player.DXLimite;
            if (distance.x > 0)
            {
                constraint.translationAxis &= Axis.Y | Axis.Z;
                transform.position = new Vector3(transform.position.x - distance.x, transform.position.y, transform.position.z);
            }
        }
        else
        {
            var temp = player.transform.position - this.transform.position;
            Vector2 distance = new Vector2(temp.x, temp.y) + offset;

            if (distance.x > 0 && (transform.position.x < (player.SXLimite.x + player.DXLimite.x) / 2.0f))
            {
                constraint.translationAxis |= Axis.X;
            }
            if (distance.x < 0 && (transform.position.x > (player.SXLimite.x + player.DXLimite.x) / 2.0f))
            {
                constraint.translationAxis |= Axis.X;
            }
        }

        if ((constraint.translationAxis & Axis.Y) != Axis.None)
        {
            var pos = cam.ScreenToWorldPoint(new Vector3(0, 0, _z));
            Vector2 distance = new Vector2(pos.x, pos.y) - player.SXLimite;
            if (distance.y < 0)
            {
                constraint.translationAxis &= Axis.X | Axis.Z;
                transform.position = new Vector3(transform.position.x, transform.position.y - distance.y, transform.position.z);
            }

            pos = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, _z));
            distance = new Vector2(pos.x, pos.y) - player.DXLimite;
            if (distance.y > 0)
            {
                constraint.translationAxis &= Axis.X | Axis.Z;
                transform.position = new Vector3(transform.position.x, transform.position.y - distance.y, transform.position.z);
            }
        }
        else
        {
            var temp = player.transform.position - this.transform.position;
            Vector2 distance = new Vector2(temp.x, temp.y) + offset;

            if (distance.y > 0 && (transform.position.y < (player.SXLimite.y + player.DXLimite.y) / 2.0f))
            {
                constraint.translationAxis |= Axis.Y;
            }
            if (distance.y < 0 && (transform.position.y > (player.SXLimite.y + player.DXLimite.y) / 2.0f))
            {
                constraint.translationAxis |= Axis.Y;
            }
        }
    }
    public void Reset()
    {
        if (!doneInitialize) Initialize();
        var pos = cam.ScreenToWorldPoint(new Vector3(0, Screen.height, _z));
        Vector2 distance = new Vector2(pos.x, pos.y) - player.SXLimite;
        transform.position = new Vector3(transform.position.x - distance.x, transform.position.y - distance.y, transform.position.z);
        if (distance.x > 0 && (transform.position.x < (player.SXLimite.x + player.DXLimite.x) / 2.0f))
        {
            constraint.translationAxis |= Axis.X;
        }
        if (distance.x < 0 && (transform.position.x > (player.SXLimite.x + player.DXLimite.x) / 2.0f))
        {
            constraint.translationAxis |= Axis.X;
        }
        if (distance.y > 0 && (transform.position.y < (player.SXLimite.y + player.DXLimite.y) / 2.0f))
        {
            constraint.translationAxis |= Axis.Y;
        }
        if (distance.y < 0 && (transform.position.y > (player.SXLimite.y + player.DXLimite.y) / 2.0f))
        {
            constraint.translationAxis |= Axis.Y;
        }
    }
}
