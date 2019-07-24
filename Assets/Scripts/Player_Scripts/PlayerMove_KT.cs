using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script works player's movement and animation
public class PlayerMove_KT : MonoBehaviour
{
    public static PlayerMove_KT Instance { get; private set; }
    [SerializeField] TargetCamera_KT camera;

    //need to attach awaking Player locating room
    [SerializeField] GameObject startRoom;
    public GameObject StartRoom {
        get
        {
            return this.startRoom;
        }
    }

    public Vector2 SXLimite { get; private set; }
    public Vector2 DXLimite { get; private set; }

    //origin image position's gap adjust
    [SerializeField] float offset_x;

    //Player's movement speed;
    [SerializeField] float maxSpeed = 0.15f;
    public float Speed { get; private set; }

    //player look right is true. [SerializeField] is attached this if level types is diffelent
    bool _ifLookRight;

    [SerializeField] AudioManager audioM; //To get the audio managers sounds


    [SerializeField] GameObject gameOver;

    Rigidbody2D _rb;

    PlayerStats_ML stats;
    ChangeWeapon_NN weapon;

    [SerializeField] AudioSource source;

    public enum VertAnimationType
    {
        Default,
        Climb,
        Dismount
    }
    public VertAnimationType MovingVertAnimation { get; private set; }

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

        MovingVertAnimation = VertAnimationType.Default;

        //audioSource = GameObject.Find("Sound_0_footsteps_1");
        //source = audioSource.GetComponent<AudioSource>();
    }


    void FixedUpdate()
    {
        if (stats.Health > 0)
        {
            if (MovingVertAnimation == VertAnimationType.Default) Move();
            else VerticalAnimation();
        }
        else
        {
            //Dead
            weapon.ResetAllLight();
            weapon.enabled = false;
            gameOver.SetActive(true);

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
            if (source.isPlaying == false)
            {
                source.volume = Random.Range(0.1f, 0.15f);
                source.pitch = Random.Range(0.8f, 1.5f);
                source.Play();

            }
            //if(source.isPlaying == false)
            // audio.PlaySound("footsteps_1");
        }

        if (Input.GetKey("a"))
        {
            _nowDirection = false;
            _velocity += maxSpeed * Vector3.left;
            if (source.isPlaying == false)
            {
                source.volume = Random.Range(0.1f, 0.15f);
                source.pitch = Random.Range(0.8f, 1.5f);
                source.Play();

            }
            //if (source.isPlaying == false)
            //audio.PlaySound("footsteps_1");
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

        camera.Reset();
    }


    //function about climbing animation
    [SerializeField] float verticalAnimateTime;
    float verticalTimer = 0;
    float startHeight;
    float goalHeight;
    public bool FrontAnim { get; set; }
    public bool BackAnim { get; set; }

    public void StartVerticalAnimate(float height, VertAnimationType type)
    {
        if (verticalTimer > 0 || type == VertAnimationType.Default) return;
        MovingVertAnimation = type;
        verticalTimer = verticalAnimateTime;
        startHeight = transform.position.y;
        goalHeight = height;
        FrontAnim = type == VertAnimationType.Dismount;
        BackAnim = type == VertAnimationType.Climb;
    }

    void VerticalAnimation()
    {
        verticalTimer -= Time.deltaTime;
        if (verticalTimer <= 0.0f)
        {
            MovingVertAnimation = VertAnimationType.Default;
            transform.position = new Vector3(transform.position.x, goalHeight, transform.position.z);
            FrontAnim = BackAnim = false; //dont animate at same time
        }
        else
        {
            if (MovingVertAnimation == VertAnimationType.Climb)
            {
                //linear
                float _t = 1.0f - (verticalTimer / verticalAnimateTime);
                float _y = _t;
                float dist_y = _y * (goalHeight - startHeight);
                transform.position = new Vector3(transform.position.x, startHeight + dist_y, transform.position.z);
            }

            if (MovingVertAnimation == VertAnimationType.Dismount)
            {
                //y = (x-1)*(x-1) - 1
                float _t = 1.0f - (verticalTimer / verticalAnimateTime);
                float param1 = 0.2f;
                float _y = ((_t - param1) * (_t - param1) - param1 * param1) / (1.0f - param1) / (1.0f - param1);
                float dist_y = _y * (goalHeight - startHeight);
                transform.position = new Vector3(transform.position.x, startHeight + dist_y, transform.position.z);
            }
        }
    }
}
