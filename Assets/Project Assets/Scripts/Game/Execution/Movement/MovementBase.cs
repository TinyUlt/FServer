using UnityEngine;
using System.Collections;

public class MovementBase : ExecutionBase
{
    public enum MotiveType
    {
        None,
        translate,
        velocity
    }

    

    protected Vector3 movement;

    private MovementData movementData;

    public bool enableMovement = false;

    public override void Start()
    {
        movementData = GetComponent<MovementData>();

        if (movementData)
        {
            movementData.attackMovementList[this] = new MovementData.Movement();
        }

        base.Start();

    }
    public override void OnEnter()
    {
        base.OnEnter();

        setEnableMovement(true);
    }
    public override void OnExit()
    {
        base.OnExit();

        setEnableMovement(false);
    }
    public void setEnableMovement(bool enable)
    {
        enableMovement = enable;

        movementData.attackMovementList[this].resetMovement();
    }
    public void applayMovement(MotiveType type, Vector3 movement)
    {
        movementData.attackMovementList[this].setMovement(type, movement);
    }
    public void applayMovement(ExecutionBase execution, MotiveType type, Vector3 movement)
    {
        movementData.attackMovementList[execution].setMovement(type, movement);
    }
}
