using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRotate_KT : MonoBehaviour
{
    //serialize Relative parameter
    [SerializeField] float turnAngle = -90.0f;
    [SerializeField] Vector3 raisingPosition = new Vector3(0, 0, 0);

    Quaternion _originQuaternion;
    Vector3 _originPos;

    void Start()
    {
        _originPos = transform.localPosition;
        _originQuaternion = transform.localRotation;
    }

    public void Raise()
    {
        transform.localRotation = _originQuaternion * Quaternion.Euler(turnAngle, 0, 0);
        transform.localPosition = _originPos + raisingPosition;
    }

    public void Drop()
    {
        transform.localRotation = _originQuaternion;
        transform.localPosition = _originPos;
    }
}
