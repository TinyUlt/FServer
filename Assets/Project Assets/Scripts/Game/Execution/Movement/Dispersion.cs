using UnityEngine;
using System.Collections;

public class Dispersion : RandomPosition
{
	// Use this for initialization
    public float circleRadius;

	public override void Start ()
    {
        base.Start();
    }

    public override void OnEnter()
    {
        base.OnEnter();

        InvokeRepeating("distrubutePosition", 0, changePositionTime);
    }

    public override void distrubutePosition()
    {
        Vector3 position = Random.insideUnitCircle * circleRadius;

        position += center;

        changePosition(position);
    }
    
    void FixedUpdate() {

        if (enableMovement)
        {
            calculateMovement();
        }
    }
    
}
