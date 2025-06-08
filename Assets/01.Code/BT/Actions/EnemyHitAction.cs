using System;
using Code.Entities;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "EnemyHit", story: "Enemy [particles]", category: "Action", id: "8977747410fbab3ef02d33a5b7209fd2")]
public partial class EnemyHitAction : Action
{
    [SerializeReference] public BlackboardVariable<ParticleSystem> Particles;

    private bool isHitEnd = false;

    protected override Status OnStart()
    {
        var par = Particles.Value;
        par.Play();
        return Status.Running;
    }

    private void EndHit()
    {
        isHitEnd = true;
    }

    protected override Status OnUpdate()
    {
        if (isHitEnd)
            return Status.Success;
        return Status.Running;
    }
}

