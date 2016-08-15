using UnityEngine;
using System.Collections;

public class PickUp : ExecutionBase
{
    public override void OnEnter()
    {
        base.OnEnter();

        //var otherAttackBehaviorBase = currentOnEnterCollider.transform.parent.GetComponent<AttackBehaviorBase>();

        //attackBehaviorBase.catchSomething(otherAttackBehaviorBase);
    }
}
