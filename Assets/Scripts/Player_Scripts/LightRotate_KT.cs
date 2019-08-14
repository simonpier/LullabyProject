using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRotate_KT : MonoBehaviour
{
    //serialize Relative parameter
    [SerializeField] float turnAngle = -70.0f;
    [SerializeField] Vector3 raisingPosition = new Vector3(0, 0, 0);
    [SerializeField] Vector3 dropPosition = new Vector3(0, 0, 0);

    [SerializeField] float maxFuel = 100.0f;
    [SerializeField] float cost = 2.0f;
    
    //this value in 0.0f~1.0f 
    public float NowAmount { get; set; }

    Quaternion _originQuaternion;
    Vector3 _originPos;

    

    void Awake()
    {
        _originPos = transform.localPosition;
        _originQuaternion = transform.localRotation;
        gameObject.SetActive(false);
        NowAmount = 1.0f;
    }

    void Update()
    {
        NowAmount -= (cost * Time.deltaTime) / maxFuel;
    }

    public void Raise()
    {
        transform.localRotation = _originQuaternion * Quaternion.Euler(turnAngle, 0, 0);
        transform.localPosition = _originPos + raisingPosition;
    }

    public void Default()
    {
        transform.localRotation = _originQuaternion;
        transform.localPosition = _originPos;
    }

    public void Drop()
    {
        transform.localRotation = _originQuaternion * Quaternion.Euler(-turnAngle, 0, 0);
        transform.localPosition = _originPos + dropPosition;
    }
}
