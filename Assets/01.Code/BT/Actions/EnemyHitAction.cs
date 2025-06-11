using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace Code.BT.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "EnemyHit", story: "Enemy [particles]", category: "Action", id: "8977747410fbab3ef02d33a5b7209fd2")]
    public partial class EnemyHitAction : Action
    {
        [SerializeReference] public BlackboardVariable<ParticleSystem> Particles;
        protected override Status OnStart()
        {
            var par = Particles.Value;
            par.Play();
            return Status.Success;
        }
    }
}

