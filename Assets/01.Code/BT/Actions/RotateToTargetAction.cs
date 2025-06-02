using System;
using Code.Enemies;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace Code.BT.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "RotateToTarget", story: "[Movement] rotate to [Target] ", category: "Action", id: "5afdef7a248e1fc964de452a417d9330")]
    public partial class RotateToTargetAction : Action
    {
        [SerializeReference] public BlackboardVariable<NavMovement> Movement;
        [SerializeReference] public BlackboardVariable<Transform> Target;
        [SerializeReference] public BlackboardVariable<Transform> Self;

        protected override Status OnUpdate()
        {
            if(LookTargetSmoothly()) 
                return Status.Success;
            
            return Status.Running;
        }

        private bool LookTargetSmoothly()
        {
            Quaternion targetRot = Movement.Value.LookAtTarget(Target.Value.position);
            const float angleThreshold = 5f;
            return Quaternion.Angle(targetRot, Self.Value.rotation) < angleThreshold;
        }
    }
}

