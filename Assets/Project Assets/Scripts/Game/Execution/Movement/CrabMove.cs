using UnityEngine;
using System.Collections;

public class CrabMove : MovementBase
{
    public float speed = 10;

    public int wallLayer;

    public int limitLayer;

    private Vector3 groundNormal = Vector3.up;

    Rigidbody rig;

    int collisionCount = 0;

    float tempSpeed = 0;

    public override void Start()
    {
        base.Start();

        rig = GetComponent<Rigidbody>();

        tempSpeed = Random.Range(0f, 1f);

        if(tempSpeed > 0.5)
        {
            tempSpeed = -speed;
        }else
        {
            tempSpeed = speed;
        }
    }
    

    public override void OnEnter()
    {
        base.OnEnter();

    }

    public override void OnExit()
    {
        base.OnExit();
    }

    void FixedUpdate()
    {
        if (enableMovement)
        {
            movement = Vector3.zero;
            
            //Debug.DrawRay(transform.position, movement * 10, Color.blue);

            //Debug.DrawRay(transform.position, -groundNormal * 10, Color.red);

            if (collisionCount <= 0)
            {
                movement -= groundNormal * 5f;

                rig.velocity = Vector3.zero;
            }
            else
            {
                movement = new Vector3(tempSpeed, 0, 0);

                movement = AdjustGroundVelocityToNormal(movement, groundNormal);

                if (movement != Vector3.zero)
                {
                    //Debug.DrawRay(transform.position, transform.up * 10, Color.green);

                    transform.LookAt(transform.position + movement);

                    float angleZ = 0;

                    if (transform.up.z >= 1 || transform.up.z <= -1)
                    {
                        if (groundNormal.x > 0)
                        {
                            angleZ = -90;
                        }
                        else
                        {
                            angleZ = 90;
                        }
                    }
                    if (groundNormal.y < 0)
                    {
                        angleZ = 180;
                    }
                    transform.localEulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + angleZ);
                }
            }
            applayMovement(MotiveType.velocity, movement);
        }
    }

    private Vector3 AdjustGroundVelocityToNormal(Vector3 hVelocity, Vector3 groundNormal)
    {
        Vector3 sideways = Vector3.Cross(Vector3.up, hVelocity);

        return Vector3.Cross(sideways, groundNormal).normalized * hVelocity.magnitude;
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.layer == wallLayer)
        {
            collisionCount++;

            groundNormal = other.contacts[0].normal;
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == limitLayer)
        {
            tempSpeed *= -1;
        }
    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.layer == wallLayer)
        {
            groundNormal = other.contacts[0].normal;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.layer == wallLayer)
        {
            collisionCount--;
        }
       
    }
}
