using Code.Managers;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "AddGoldOnEnemyDeath", story: "EnemyDeath [gold] to [DataManager]", category: "Action", id: "cbc98981e053642ed873cf8c8241a4c9")]
public partial class AddGoldOnEnemyDeathAction : Action
{
    [SerializeReference] public BlackboardVariable<int> Gold;
    [SerializeReference] public BlackboardVariable<DataManager> DataManager;

    protected override Status OnStart()
    {
        var goldAmount = Gold?.Value ?? 0;
        var goldManager = DataManager?.Value;

        if (goldManager != null)
        {
            goldManager.UpGold(goldAmount);
            return Status.Success;
        }
        return Status.Running;
    }
}

