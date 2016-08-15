using UnityEditor;
using UnityEngine;

//自定义Tset脚本
[CustomEditor(typeof(AttackBehaviorBase), true)]
//[CustomEditor(typeof(PlayerBehavior))]
//在编辑模式下执行脚本，这里用处不大可以删除。
[ExecuteInEditMode]
[CanEditMultipleObjects]
//请继承Editor
public class AttackBehaviorBaseEditor : Editor
{
    void OnEnable()
    {
    }

    public override void OnInspectorGUI()
    {
        AttackBehaviorBase attackBehavior = (AttackBehaviorBase)target;

        attackBehavior.mode = (AttackBehaviorBase.MODE)EditorGUILayout.EnumPopup("Mode", attackBehavior.mode);

        attackBehavior.catchType = (AttackBehaviorBase.CatchType)EditorGUILayout.EnumPopup("Catch Type", attackBehavior.catchType);

        switch (attackBehavior.catchType)
        {
            case AttackBehaviorBase.CatchType.ByHp:
                {
                    //hp = 0 的时候 无论ATK是多少， 该对象碰撞后都会摧毁
                    attackBehavior.HP = EditorGUILayout.IntField("HP(>=0)", attackBehavior.HP);
                    break;
                }

            case AttackBehaviorBase.CatchType.ByProbability:
                {
                    attackBehavior.Probability = EditorGUILayout.FloatField("Probability(0.0~1.0)", attackBehavior.Probability);

                    break;
                }
        }

        attackBehavior.ATK = EditorGUILayout.IntField("ATK(>=0)", attackBehavior.ATK);

        attackBehavior.coinValue = EditorGUILayout.IntField("CoinValue(>=0)", attackBehavior.coinValue);

        attackBehavior.destroyDeep = EditorGUILayout.IntField("DestroyDeep(>=1000)", attackBehavior.destroyDeep);
    }
}
