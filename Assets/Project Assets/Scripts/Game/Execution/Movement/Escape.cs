using UnityEngine;
using System.Collections;
using System.Collections.Generic;//C# 包中的类

public class Escape : MovementBase
{
    public float speed = 10;

    void FixedUpdate() {
        
        if (enableMovement)
        {
            movement = Vector3.zero;

            foreach (var item in others.Values)
            {
                float r = item.radius + radius;

                var distance = (transform.position - item.transform.position).sqrMagnitude;

                var offset = r * r - distance;

                offset = Mathf.Max(0, offset);

                movement += (transform.position - item.transform.position).normalized * offset * speed;
            }
            applayMovement(MotiveType.velocity, movement);
        }
        
    }
}
