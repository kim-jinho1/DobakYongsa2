using Code.Entities;
using UnityEngine;

namespace Code.Players.Components
{
    public class RootMotionCompo : MonoBehaviour, IEntityComponent
    {
        private Entity _entity;
        
        public void Initialize(Entity entity)
        {
            _entity = entity;
        }
        
        public void InRootMotion(EntityAnimator animator)
        {
            animator.animator.applyRootMotion = true;
        }

        public void ExitRootMotion(EntityAnimator animator)
        {
            animator.animator.applyRootMotion = false;
        }

        public Vector3 ChangePositon()
        {
            Vector3 pos = transform.position;
            transform.localPosition = Vector3.zero;
            return pos;
        }

        public Quaternion ChangeRotation()
        {
            Quaternion rotation = transform.rotation;
            transform.localRotation = Quaternion.identity;
            return rotation;
        }
    }
}