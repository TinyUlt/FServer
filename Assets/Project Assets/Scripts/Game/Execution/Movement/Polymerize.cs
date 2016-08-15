using UnityEngine;
using System.Collections;

public class Polymerize : RandomPosition
{
    public float offsetRadiusDynamic = 0.5f;


    private Vector3 positionOffsetStatic;

   // private CircleLifeSpawn spawner;
    override public void OnEnable()
    {
        base.OnEnable();

        //if (attackBehaviorBase)
        //{
        //    spawner = (attackBehaviorBase.spawner as CircleLifeSpawn);
        //}
        //lifeSpawn = transform.parent.GetComponent<CircleLifeSpawn>();
    }
    public override void Start()
    {

        base.Start();

        //if (attackBehaviorBase)
        //{
        //    spawner = attackBehaviorBase.spawner as CircleLifeSpawn;
        //}
        //lifeSpawn = transform.parent.GetComponent<CircleLifeSpawn>();

        //InvokeRepeating("distrubutePosition", 0, changePositionTime);
    }
    public override void OnEnter()
    {
        base.OnEnter();

        var spawner = (attackBehaviorBase.spawner as CircleLifeSpawn);

        changePositionTime = spawner.getChangePositonTime();

        InvokeRepeating("distrubutePosition", 0, changePositionTime);

        var offsetRadiusStatic = spawner.offsetRadius;

        positionOffsetStatic = Random.insideUnitCircle * offsetRadiusStatic;
    }
    public override void distrubutePosition()
    {
        var spawner = (attackBehaviorBase.spawner as CircleLifeSpawn);

        Vector3 position = spawner.position;

        Vector3 positionOffsetDynamic = Random.insideUnitCircle * offsetRadiusDynamic;

        changePosition(position + positionOffsetStatic + positionOffsetDynamic);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (enableMovement)
        {
            calculateMovement();
        }
    }
}
