using UnityEngine;
using System.Collections;

public class LifeBehavior : AttackBehaviorBase
{
    private Rigidbody rig;//刚体

   

    public override void Awake()
    {
        base.Awake();

        addRigdbody();
    }

   
   
    void addRigdbody()
    {
        rig = GetComponent<Rigidbody>();
        if (!rig)
        {
            rig = gameObject.AddComponent<Rigidbody>();
        }

        rig.angularDrag = 0.0F;

        rig.useGravity = false;

        rig.drag = 100;
    }
}
