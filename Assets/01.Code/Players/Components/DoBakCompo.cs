using Code.Entities;
using UnityEngine;

namespace Code.Players.Components
{
    public class DoBakCompo : MonoBehaviour,IEntityComponent
    {
        public Transform Position;
        
        private Entity _entity;
        
        public void Initialize(Entity entity)
        {
            _entity = entity;
        }
    }
}