using UnityEngine;
using System.Collections;

public class WeaponBehavior : AttackBehaviorBase
{
    

    virtual public void init(AttackBehaviorBase attackObj, Vector3 endPosition , Vector3 currentVelocity)
    {
        init(attackObj);
    }

    virtual public void init(AttackBehaviorBase attackObj)
    {
        injuredAttackBehavior = attackObj;
    }
}
