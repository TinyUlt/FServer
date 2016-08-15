using UnityEngine;
using System.Collections;
using System.Collections.Generic;//C# 包中的类

public class MovementData : MonoBehaviour {

    public class Movement
    {
        public MovementBase.MotiveType type;

        public Vector3 value
        {
            get
            {
                return _movement;
            }
            private set
            {
                _movement = value;
            }
        }
        public void resetMovement()
        {
            setMovement(MovementBase.MotiveType.None, Vector3.zero);
        }
        public void setMovement(MovementBase.MotiveType type, Vector3 movement)
        {
            this.type = type;

            switch (this.type)
            {
                case MovementBase.MotiveType.translate:
                    {
                        value = movement;

                        break;
                    }
                case MovementBase.MotiveType.velocity:
                    {
                        value = movement * Time.deltaTime;

                        break;
                    }
                case MovementBase.MotiveType.None:
                    {
                        value = Vector3.zero;
                        break;
                    }
            }
        }
        private Vector3 _movement;
    }

    public Dictionary<ExecutionBase, Movement> attackMovementList;

    public Vector3 allMovement;

    void FixedUpdate()
    {
        var movement = Vector3.zero;

        foreach (var item in attackMovementList.Values)
        {
            movement += item.value;
        }
        movement.z = 0;
        allMovement = movement;
    }
    public void addMovement(ExecutionBase execution)
    {
        if (!attackMovementList.ContainsKey(execution))
        {
            attackMovementList[execution] = new Movement();
        }
    }
    public void removeMovement(ExecutionBase execution)
    {
        if (attackMovementList.ContainsKey(execution))
        {
            attackMovementList.Remove(execution);
        }
    }
    MovementData()
    {
        attackMovementList = new Dictionary<ExecutionBase, Movement>();
    }
}
