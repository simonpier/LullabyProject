using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script works player's movement and animation
public class PlayerMove_KT : MonoBehaviour
{
    public static PlayerMove_KT Instance { get; private set; }

    //need to attach awaking Player locating room
    [SerializeField] GameObject startRoom;

    public Vector2 SXLimite { get; private set; }
    public Vector2 DXLimite { get; private set; }

    //origin image position's gap adjust
    [SerializeField] float offset_x;

    //Player's movement speed;
    [SerializeField] float maxSpeed = 0.15f;
    public float Speed { get; private set; }

    //player look right is true. [SerializeField] is attached this if level types is diffelent
    bool _ifLookRight;

    Rigidbody2D _rb;

    PlayerStats_ML stats;
    ChangeWeapon_NN weapon;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        _ifLookRight = true;
        _rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<PlayerStats_ML>();
        weapon = GetComponent<ChangeWeapon_NN>();
        CheckRoomSize(startRoom);
        Speed = 0;
    }

    void FixedUpdate()
    {
        if (stats.Health > 0)
        {
            Move();
        }
        else
        {
            //Dead
            weapon.ResetAllLight();
            weapon.enabled = false;
        }
    }

    void Move()
    {
        bool _nowDirection = _ifLookRight;
        Vector3 _velocity = Vector3.zero;
        if (Input.GetKey("d"))
        {
            _nowDirection = true;
            _velocity += maxSpeed * Vector3.right;
        }

        if (Input.GetKey("a"))
        {
            _nowDirection = false;
            _velocity += maxSpeed * Vector3.left;
        }

        if (_nowDirection != _ifLookRight)
        {
            this.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
        }
        Vector2 futurePos = (transform.position + _velocity);
        futurePos.x = Mathf.Min(Mathf.Max(SXLimite.x + offset_x, futurePos.x), DXLimite.x - offset_x);
        futurePos.y = Mathf.Min(Mathf.Max(SXLimite.y, futurePos.y), DXLimite.y);
        transform.position = new Vector3(futurePos.x, futurePos.y, transform.position.z);
        Speed = Mathf.Abs(_velocity.x);

        _ifLookRight = _nowDirection;
    }

    //if player check door, it need to call this function
    public void CheckRoomSize(GameObject room)
    {
        Transform _sx = null;
        Transform _dx = null;
        for (int i = 0; i < room.transform.childCount && !(_sx != null && _dx != null); i++)
        {
            var temp = room.transform.GetChild(i);
            if (temp.name.Contains("SX Limiter"))
            {
                _sx = temp;
            }
            if (temp.name.Contains("DX Limiter"))
            {
                _dx = temp;
            }
        }
        if (!(_sx != null && _dx != null)) Debug.LogError("sx limitter or dx limitter is not find!");

        SXLimite = new Vector2(_sx.position.x, _sx.position.y);
        DXLimite = new Vector2(_dx.position.x, _dx.position.y);
    }
}
