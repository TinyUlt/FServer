using UnityEngine;
using System.Collections;

public class BulletBehavior : WeaponBehavior
{
    override public void init(AttackBehaviorBase attackObj, Vector3 endPosition, Vector3 currentVelocity)
    {
        base.init( attackObj,  endPosition,  currentVelocity);

        var line = GetComponent<Line>();

        line.startPosition = transform.position;

        line.endPosition = endPosition;

        line.currentVelocity = currentVelocity;
    }
}
