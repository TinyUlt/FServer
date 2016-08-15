using UnityEngine;
using System.Collections;

public class OutOfWater : MonoBehaviour {

    public enum SpaceState//空间状态
    {
        None,
        InAir,
        InSeaLevel,
        InWater
    }
    public bool canControl = true;

    public float seaLevel = 0;//海平面高度

    public float jumpLimitSpeed = 10.0F;//跳出水面的最低速度

    [SerializeField]
    private float inwaterPercentage = 1;//在水里的比例

    [SerializeField]
    private SpaceState spaceState = SpaceState.None;//当前状态

    [SerializeField]
    private bool userGravity = false;//是否使用重力

    [SerializeField]
    private bool isJump = false;//是否跳出水面

    [SerializeField]
    private Vector3 AirVelocity = Vector3.zero; // 出水面时的速度

    private Vector3 gravityMovement = Vector3.zero;//重力速度

    private SpaceState oldSpaceState;//旧状态

    private RotationData rotationData;

    private MovementData movementData;

    private float g = 9.81f;//重力加速度

    private Vector3 MotiveTranslate = Vector3.zero;// 移动量

    private Vector3 movement = Vector3.zero;

    void Awake()
    {
    }
    // Use this for initialization
    void Start () {

        rotationData = GetComponent<RotationData>();

        movementData = GetComponent<MovementData>();

        oldSpaceState = spaceState;
    }

    void calculateSpaceState()
    {
        var h = rotationData.vPosition.y;

        var f = rotationData.vNPosition.y;

        var l = Mathf.Abs(h - f);

        var p = seaLevel;

        if (h > f)
        {
            if ((h > p && p > f))
            {
                inwaterPercentage = (p - f) / l;
            }
            else if (f >= p)
            {
                inwaterPercentage = 0;
            }
            else if (h < p)
            {
                inwaterPercentage = 1;
            }
        }
        else if (h < f)
        {
            if ((f > p && p > h))
            {
                inwaterPercentage = (p - h) / l;
            }
            else if (f < p)
            {
                inwaterPercentage = 1;
            }
            else if (h >= p)
            {
                inwaterPercentage = 0;
            }
        }
        else
        {
            if (f > p)
            {
                inwaterPercentage = 0;
            }
            else
            {
                inwaterPercentage = 1;
            }
        }

        if (inwaterPercentage == 1)
        {
            spaceState = SpaceState.InWater;
        }
        else if (inwaterPercentage == 0)
        {
            spaceState = SpaceState.InAir;
        }
        else
        {
            spaceState = SpaceState.InSeaLevel;
        }

        if (oldSpaceState == SpaceState.None)
        {
            switch (spaceState)
            {
                case SpaceState.InWater:
                    {
                        break;
                    }
                case SpaceState.InSeaLevel:
                    {
                        break;
                    }
                case SpaceState.InAir:
                    {
                        userGravity = true;

                        gravityMovement = Vector3.zero;

                        isJump = true;

                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        if (oldSpaceState != spaceState)
        {
            if (oldSpaceState == SpaceState.InWater && spaceState == SpaceState.InSeaLevel)
            {
                userGravity = true;

                gravityMovement = Vector3.zero;

                AirVelocity = movementData.allMovement;
            }
            else if (oldSpaceState == SpaceState.InSeaLevel && spaceState == SpaceState.InAir)
            {
                userGravity = true;
            }
            else if (oldSpaceState == SpaceState.InAir && spaceState == SpaceState.InSeaLevel)
            {
                userGravity = true;
            }
            else if (oldSpaceState == SpaceState.InSeaLevel && spaceState == SpaceState.InWater)
            {
                userGravity = false;

                gravityMovement = Vector3.zero;
            }
        }
        oldSpaceState = spaceState;
    }
    void calculateMovement()
    {
        float time = Time.deltaTime;

        if (userGravity)
        {
            gravityMovement.y += Time.deltaTime * g;
        }

        if (spaceState == SpaceState.InSeaLevel)
        {
            isJump = MotiveTranslate.y / time > jumpLimitSpeed;
        }

        if (canControl)
        {
            if (isJump)
            {
                rotationData.usingType = Rotation.UsingType.UsingActualMovement;
            }
            else
            {
                rotationData.usingType = Rotation.UsingType.UsingDesignedMovement;
            }
        }
        else
        {
            rotationData.usingType = Rotation.UsingType.UsingActualMovement;
        }

        movement = MotiveTranslate;

        if (spaceState == SpaceState.InSeaLevel && !isJump)
        {
            movement.y = Mathf.Min(0, movement.y);
        }
        if (isJump)
        {
            movement = AirVelocity;
        }
        movement = movement - gravityMovement * (1 - inwaterPercentage) * time;
    }
    void FixedUpdate()
    {
        movement = MotiveTranslate = movementData.allMovement;

        if(MotiveTranslate == Vector3.zero )
        {
            return;
        }
        if(transform.position.y > -10)
        {
            calculateSpaceState();

            calculateMovement();
        }
        transform.position += movement;
    }
}
