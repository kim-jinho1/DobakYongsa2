using System;
using System.Collections.Generic;
using Code.Enemies;
using Code.Entities;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace Code.BT.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "GetComponents", story: "Get components from [self]", category: "Enemy", id: "b4f809444b8cfbedbe08ffe67e3b82be")]
    public partial class GetComponentsAction : Action
    {
        [SerializeReference] public BlackboardVariable<Enemy> Self;

        protected override Status OnStart()
        {
            Enemy enemy = Self.Value;
            List<BlackboardVariable> varList = enemy.BtAgent.BlackboardReference.Blackboard.Variables;

            foreach (var variable in varList)
            {
                if(typeof(IEntityComponent).IsAssignableFrom(variable.Type) == false) continue; 
                
                SetVariable(enemy, variable.Name, enemy.GetCompo(variable.Type));
            }
            
            return Status.Success;
        }

        private void SetVariable(Enemy enemy, string variableName, IEntityComponent component)
        {
            Debug.Assert(component != null, $"Check {variableName} component not exist on {enemy.gameObject.name}");
            if (enemy.BtAgent.GetVariable(variableName, out BlackboardVariable variable))
            {
                variable.ObjectValue = component;
            }
        }
    }
}

