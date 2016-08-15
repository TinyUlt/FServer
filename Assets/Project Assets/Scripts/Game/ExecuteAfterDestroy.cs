using UnityEngine;
using System.Collections;

public class ExecuteAfterDestroy : MonoBehaviour {

    public Injured injured;

    private AttackBehaviorBase attackBehavior;

    void Awake()
    {
        injured.ead += execution;

        attackBehavior = GetComponent<AttackBehaviorBase>();
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void execution(AttackBehaviorBase otherAttackBehavior)
    {
        otherAttackBehavior.catchSomething(attackBehavior);
    }
}
