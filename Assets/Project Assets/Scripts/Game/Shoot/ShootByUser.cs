using UnityEngine;
using System.Collections;

public class ShootByUser : Shoot
{
    public Transform cameraTransform;

    public float fireRate = 1 / 3.0f;

    float nextFire = 0;

    bool isShooting;

    Vector3 position;

    float cameraDistance;

    protected virtual void OnEnable()
    {
        Lean.LeanTouch.OnFingerDown += OnFingerDown;
        Lean.LeanTouch.OnFingerUp += OnFingerUp;
        Lean.LeanTouch.OnFingerDrag += OnFingerDrag;
    }

    protected virtual void OnDisable()
    {
        Lean.LeanTouch.OnFingerDown -= OnFingerDown;
        Lean.LeanTouch.OnFingerUp -= OnFingerUp;
        Lean.LeanTouch.OnFingerDrag -= OnFingerDrag;
    }

    public override void Start()
    {
        base.Start();
        cameraDistance = -cameraTransform.position.z;
    }

    public override void Update()
    {
        base.Update();
        OnShooting();
    }
    public void OnFingerDown(Lean.LeanFinger finger)
    {
        if (finger.IsOverGui == false)
        {
            isShooting = true;

            position = finger.GetWorldPosition(cameraDistance);
        }
    }
    public void OnFingerUp(Lean.LeanFinger finger)
    {
        isShooting = false;
    }
    public void OnFingerDrag(Lean.LeanFinger finger)
    {
        if (finger.IsOverGui == false)
        {
            position = finger.GetWorldPosition(cameraDistance);
        }
    }
    public void OnShooting()
    {
        if (isShooting && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            base.shoot(position);
        }
    }
}
