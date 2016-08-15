using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour
{
    public enum UsingType
    {
        UsingDesignedMovement,

        UsingActualMovement
    }
    public bool isSmallFish = true;

    public bool isBehind = false;

    public Transform HTransform;

    public Transform VTransform;

    public Transform VNTransform;

    public float radius = 2;

    public float rotationSpeed = 5;

    private Vector3 centerPt;

    public Vector3 hPosition;

    public Vector3 vPosition;

    public Vector3 vNPosition;

    private Vector3 resetPosition = new Vector3(1, 1, 0);

    private Vector3 lookAtPosition;

    private float rotation;

    private RotationData rotationData;

    private MovementData movementData;

    float oldz = 0;

    void Awake()
    {
        rotationData = gameObject.AddComponent<RotationData>();
    }

    void Start()
    {
        movementData = GetComponent<MovementData>();

        hPosition = transform.position;

        vPosition = transform.position;
    }

    void FixedUpdate()
    {
        transform.position = Vector3.Scale(transform.position, resetPosition);

        Vector3 movement = Vector3.zero;

        switch (rotationData.usingType)
        {
            case UsingType.UsingDesignedMovement:
                {
                    rotationData.designedMovement = movementData.allMovement;

                    movement = rotationData.designedMovement;

                    break;
                }
            case UsingType.UsingActualMovement:
                {
                    movement = rotationData.actualMovement;

                    break;
                }
        }
        if (movement != Vector3.zero)
        {
            if (isSmallFish)
            {
                float angle = VecotrAngle(movement);

                float anglez = 0;

                if (isBehind)
                {
                    anglez = angle < 90 ? angle - 90 : angle < 270 ? 90 - angle : angle - 450;
                }
                else
                {
                    anglez = 90 - angle;
                }

                transform.localEulerAngles = new Vector3(angle - 90, 90, anglez);

                var x = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;

                var y = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;

                Vector3 v = new Vector3(x, y, 0);

                vPosition = transform.position + v;

                vNPosition = transform.position - v;
            }
            else
            {
                HTurn(movement.x * rotationSpeed, movement.y * rotationSpeed);

                VTurn(movement.x * rotationSpeed, movement.y * rotationSpeed);

                transform.LookAt(lookAtPosition);

                transform.Rotate(rotation, 0, 0);
            }
        }
        if (HTransform)
        {
            HTransform.position = lookAtPosition;
        }
        if (VTransform)
        {
            VTransform.position = vPosition;
        }
        if (VNTransform)
        {
            VNTransform.position = vNPosition;
        }
        rotationData.vPosition = vPosition;

        rotationData.vNPosition = vNPosition;

        rotationData.actualMovement = rotationData.currentPosition - rotationData.oldPosition;

        rotationData.oldPosition = rotationData.currentPosition;

        rotationData.currentPosition = transform.position;
    }
    float VecotrAngle(Vector2 to)
    {
        float angle = Vector2.Angle(Vector2.up, to);

        if (to.x < 0)
        {
            angle = 360 - angle;
        }

        return angle;
    }

    void HTurn(float x, float z)
    {
        centerPt = transform.position;

        var aaa = lookAtPosition.x - centerPt.x;

        if (Mathf.Abs(aaa) < 0.13f)
        {
            z = oldz;
        }else
        {
            oldz = z;
        }
        Vector3 movement = new Vector3(x, 0, z);

        Vector3 newPos = hPosition + movement;

        Vector3 offset = newPos - centerPt;

        offset.y = 0;

        offset = offset.normalized * radius;

        lookAtPosition = centerPt + offset;

        hPosition = lookAtPosition;

        if (!isBehind)
        {
            lookAtPosition.z = -Mathf.Abs(lookAtPosition.z);
        }
        
    }

    void VTurn(float x, float y)
    {
        centerPt = transform.position;

        Vector3 movement = new Vector3(x, y, 0);

        Vector3 newPos = vPosition + movement;

        Vector3 offset = newPos - centerPt;

        var offsetVector = Vector3.ClampMagnitude(offset, radius);

        vPosition = centerPt + offsetVector;

        vNPosition = centerPt - offsetVector;

        float height = vPosition.y - transform.position.y;

        rotation = -height / radius * 90;
    }
}

