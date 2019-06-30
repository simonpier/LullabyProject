using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script keep position no matter whether Parent turn
public class PositionKeeper_KT : MonoBehaviour
{
    [SerializeField] bool _x;
    [SerializeField] bool _y;
    [SerializeField] bool _z;

    Vector3 _originPos;
    Matrix4x4 _originMat;
    void Start()
    {
        if (!(_x || _y || _z)) {
            this.enabled = false;
            return;
        }
        _originPos = transform.position;
        SetOriginMat();
    }
    
    void Update()
    {
        transform.position = _originMat * new Vector4(transform.position.x, transform.position.y, transform.position.z, 1);
    }

    void SetOriginMat()
    {
        _originMat = Matrix4x4.identity;
        if (_x)
        {
            _originMat.m00 = 0;
            _originMat.m03 = _originPos.x;
        }
        if (_y)
        {
            _originMat.m11 = 0;
            _originMat.m13 = _originPos.y;
        }
        if (_z)
        {
            _originMat.m22 = 0;
            _originMat.m23 = _originPos.z;
        }
    }

    public void Reset()
    {
        Start();
    }
}
