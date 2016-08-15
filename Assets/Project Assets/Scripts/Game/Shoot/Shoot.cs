using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

    public BulletSpawner bulletSpawner;

    // available weapons for easy access

    public PoolManager.WEAPONS weapon;

    public Transform startPoint;

    public float bulletSpeed = 1;

    private Vector3 oldPosition;

    private Vector3 currentVelocity;

    private AttackBehaviorBase attackBehaviorBase;

    public virtual void Start()
    {
        attackBehaviorBase = GetComponent<AttackBehaviorBase>();

      
    }

    public virtual void Update()
    {
        currentVelocity = (transform.position - oldPosition) / Time.deltaTime;

        oldPosition = transform.position;
    }
	public virtual void shoot(Vector3 shootPoint)
    {
        Vector3 pos = startPoint.position;

        pos.z = 0;

        var type = weapon.ToString();

        var weaponGameObj = bulletSpawner.createBullet(type); // Loader.LoadGameObject("Weapon/" + type);

        weaponGameObj.transform.position = pos;

        var weaponBehavior = weaponGameObj.GetComponent<WeaponBehavior>();

        attackBehaviorBase.useWeapon(weaponBehavior);

        weaponBehavior.init(attackBehaviorBase,shootPoint, currentVelocity);
    }
    public void setWeapon(string name)
    {
        weapon = (PoolManager.WEAPONS) System.Enum.Parse(typeof(PoolManager.WEAPONS), name);
    }
}
