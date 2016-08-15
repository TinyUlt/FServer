using UnityEngine;
using System.Collections;

public class BulletSpawner : SpawnerBase
{
    public GameObject createBullet(string type)
    {
        var bullet = pools.GetObjectFromPool(type);

        bullet.GetComponent<AttackBehaviorBase>().spawner = this;

        bullet.transform.parent = gameObject.transform;


        return bullet;
    }
}
