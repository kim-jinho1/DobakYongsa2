using Code.Entities;
using UnityEngine;

namespace Code.Players.Components
{
    public class RootMotionCompo : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private Animator animator;
        private Entity _entity;
        private CharacterController controller;

         private Vector3 gravity = new Vector3(0, -9.81f, 0);

        public void Initialize(Entity entity)
        {
            _entity = entity;
            controller = transform.parent.GetComponent<CharacterController>();
            animator.applyRootMotion = true;
        }

        private void OnAnimatorMove()
        {
            Vector3 motion = animator.deltaPosition + gravity * Time.deltaTime;
            controller.Move(motion);
            transform.parent.rotation *= animator.deltaRotation;
        }
    }
}