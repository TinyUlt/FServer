using UnityEngine;
using System.Collections;

public class Injured : ExecutionBase
{
    public delegate void ExecuteAfterDestroyDelegate(AttackBehaviorBase otherAttackBehavior);

    [HideInInspector]
    public ExecuteAfterDestroyDelegate ead;

    public override void OnEnter()
    {
        base.OnEnter();

        var otherAttackBehaviorBase = currentOnEnterCollider.transform.GetComponent<AttackBehaviorBase>();

        otherAttackBehaviorBase = otherAttackBehaviorBase ? otherAttackBehaviorBase : currentOnEnterCollider.transform.parent.GetComponent<AttackBehaviorBase>();

        //var weaponBehaviorBase = otherAttackBehaviorBase as WeaponBehavior;

        switch (attackBehaviorBase.catchType)
        {
            case AttackBehaviorBase.CatchType.ByHp:
                {
                    
                    attackBehaviorBase.HP -= otherAttackBehaviorBase.ATK;

                    if (GetComponent<AttackBehaviorBase>().HP <= 0)
                    {
                        //otherAttackBehaviorBase.injuredAttackBehavior.GetComponent<AttackBehaviorBase>().catchFish(attackBehaviorBase, otherAttackBehaviorBase);

                        //attackBehaviorBase.coinValue = 0;
                        //otherAttackBehaviorBase.catchSomething(attackBehaviorBase);
                        attackBehaviorBase.destroyWithOnTriggerExit();

                        if (ead != null)
                        {
                            ead(otherAttackBehaviorBase);
                        }
                    }

                    break;
                }
            case AttackBehaviorBase.CatchType.ByProbability:
                {
                    var random = Random.Range(0.0f, 1.0f);

                    if (attackBehaviorBase.Probability > random)
                    {
                        //otherAttackBehaviorBase.catchSomething(attackBehaviorBase);
                        attackBehaviorBase.destroyWithOnTriggerExit();

                        if (ead != null)
                        {
                            ead(otherAttackBehaviorBase);
                        }

                    }

                    break;
                }
        }
        
    }
}
