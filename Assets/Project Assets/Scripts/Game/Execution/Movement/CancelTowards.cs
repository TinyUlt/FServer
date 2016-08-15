using UnityEngine;
using System.Collections;

public class CancelTowards : MovementBase
{
    Towards towards;

    new public void Start()
    {
        base.Start();

        towards = GetComponent<Towards>();
    }

    public override void OnEnter()
    {
        base.OnEnter();

        towards.OnExit();
    }
}
