using UnityEngine;
using System.Collections;

public class CreatFishingnet : ExecutionBase
{
    public override void OnEnter()
    {
        base.OnEnter();

        var pools = attackBehaviorBase.pools;

        var fn = pools.GetObjectFromPool("Fishingnet");

        fn.GetComponent<AttackBehaviorBase>().spawner = attackBehaviorBase.spawner;

        fn.transform.position = gameObject.transform.position;

        fn.transform.parent = attackBehaviorBase.spawner.transform;

        fn.GetComponent<WeaponBehavior>().init(attackBehaviorBase.injuredAttackBehavior);
    }
}
