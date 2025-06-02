using System;
using Code.Entities;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace Code.BT.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "ChangeAnimation", story: "[entityAnimator] change [oldBool] to [newBool]", category: "Action", id: "83e5516a6e43a1bcf1c94052e1b1bb00")]
    public partial class ChangeAnimationAction : Action
    {
        [SerializeReference] public BlackboardVariable<EntityAnimator> EntityAnimator;
        [SerializeReference] public BlackboardVariable<string> OldBool;
        [SerializeReference] public BlackboardVariable<string> NewBool;

        protected override Status OnStart()
        {
            
            //EntityAnimator.Value.SetParam(Animator.StringToHash(OldBool.Value), false);
            //EntityAnimator.Value.SetParam(Animator.StringToHash(NewBool.Value), true);

            //OldBool.Value = NewBool.Value;
            return Status.Success;
        }
    }
}

