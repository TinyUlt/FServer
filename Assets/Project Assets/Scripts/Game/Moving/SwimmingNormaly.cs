using UnityEngine;
using System.Collections;

public class SwimmingNormaly : MonoBehaviour {

    private MovementData movementData;

    private RotationData rotationData;

    void Awake()
    {
    }
    void Start()
    {
        movementData = gameObject.GetComponent<MovementData>();

        rotationData = GetComponent<RotationData>();
    }
    void FixedUpdate()
    {
        var movement = movementData.allMovement;

        if (rotationData)
        {
            rotationData.usingType = Rotation.UsingType.UsingDesignedMovement;
        }
        transform.position += movement;
    }
}
