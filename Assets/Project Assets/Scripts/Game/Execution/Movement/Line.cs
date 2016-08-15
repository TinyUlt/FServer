using UnityEngine;
using System.Collections;

public class Line : MovementBase
{
    public float speed = 10;

    public Vector3 startPosition;

    public Vector3 endPosition;

    public Vector3 currentVelocity;

    void FixedUpdate()
    {
        if (enableMovement)
        {
            movement = (endPosition - startPosition).normalized * speed + currentVelocity;

            applayMovement(MotiveType.velocity, movement);
        }
    }
}
