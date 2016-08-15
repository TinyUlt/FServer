using UnityEngine;
using System.Collections;

public class DestroyByTime : ExecutionBase
{
    public float time=0;
    public override void OnEnter()
    {
        base.OnEnter();

        attackBehaviorBase.destroyWithOnTriggerExit(time);
    }
}
