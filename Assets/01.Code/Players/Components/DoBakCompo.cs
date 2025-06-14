using Code.Entities;
using UnityEngine;

namespace Code.Players.Components
{
    public class DoBakCompo : MonoBehaviour,IEntityComponent
    {
        [SerializeField] private GameObject doBakUI;
        public Transform CameraTarget;

        
        public Transform Position;
        
        private Entity _entity;
        
        public void Initialize(Entity entity)
        {
            _entity = entity;
        }

        public void DoBak()
        {
            doBakUI.SetActive(true);
        }
    }
}