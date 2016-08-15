using UnityEngine;
using System.Collections;

public class PlayerControl : MovementBase
{
    public float speed = 10;

    public float fastSpeed;

    private Vector3 oldMovement = Vector3.zero;

    [HideInInspector]
    public float inputMovementH;

    [HideInInspector]
    public float inputMovementV;

    void FixedUpdate()
    {
        if (enableMovement)
        {
            movement = new Vector3(inputMovementH, inputMovementV, 0.0F);

            speedDownLerp();

            movement = speed * Vector3.ClampMagnitude( movement,1);

            applayMovement(MotiveType.velocity, movement);
        }
    }
    public void speedDownLerp()
    {
        if (movement == Vector3.zero && oldMovement != Vector3.zero)
        {
            movement = Vector3.Lerp(oldMovement, Vector3.zero, 0.03f);
        }
        oldMovement = movement;
    }
}
