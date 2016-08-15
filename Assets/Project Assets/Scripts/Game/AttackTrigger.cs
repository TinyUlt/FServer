using UnityEngine;
using System.Collections;

public class AttackTrigger : MonoBehaviour {

    public delegate void ColliderDelegate(Collider other);

    ColliderDelegate onTriggerEnter, onTriggerStay, onTriggerExit;

    public void setCallback(ColliderDelegate onTriggerEnter, ColliderDelegate onTriggerStay, ColliderDelegate onTriggerExit)
    {
        this.onTriggerEnter += onTriggerEnter;
        this.onTriggerStay += onTriggerStay;
        this.onTriggerExit += onTriggerExit;
    }
    void OnTriggerEnter(Collider other)
    {
        if (onTriggerEnter!=null)
        {
            onTriggerEnter(other);
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (onTriggerStay != null)
        {
            onTriggerStay(other);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (onTriggerExit!=null)
        {
            onTriggerExit(other);

        }
    }
}
