using UnityEngine;
using System.Collections;

public class Towards : MovementBase
{
    public float speed = 10;

    private Transform tr;

    public override void OnEnter()
    {
        base.OnEnter();

        tr = currentOnEnterCollider.gameObject.transform;
    }

    void FixedUpdate()
    {
        if (enableMovement )
        {
            var offset = tr.position - transform.position;

            movement = offset.normalized * speed;

            applayMovement(MotiveType.velocity, movement);
        }
    }
}
