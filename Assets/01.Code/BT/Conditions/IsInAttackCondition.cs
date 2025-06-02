using System;
using Code.Enemies;
using Unity.Behavior;
using UnityEngine;

namespace Code.BT.Conditions
{
    [Serializable, Unity.Properties.GeneratePropertyBag]
    [Condition(name: "IsInAttack", story: "[Self] check [Target] in attack range", category: "Conditions", id: "eb410b74958fe5325e93bfc436968f16")]
    public partial class IsInAttackCondition : Condition
    {
        [SerializeReference] public BlackboardVariable<Enemy> Self;
        [SerializeReference] public BlackboardVariable<Transform> Target;

        public override bool IsTrue()
        {
            float distance = Vector3.Distance(Self.Value.transform.position, Target.Value.position);
            return distance < Self.Value.attackRange;
        }
    }
}
