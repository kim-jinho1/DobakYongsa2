using UnityEngine;

namespace Code.Entities
{
    public class EntityAnimator : MonoBehaviour, IEntityComponent
    {
        [field: SerializeField] public Animator animator { get; private set; }

        private Entity _entity;

        public void Initialize(Entity entity)
        {
            _entity = entity;
        }

        public void SetParam(int hash, float value) => animator.SetFloat(hash, value);
        public void SetParam(int hash, bool value) => animator.SetBool(hash, value);
        public void SetParam(int hash, int value) => animator.SetInteger(hash, value);
        public void SetParam(int hash) => animator.SetTrigger(hash);

        public void SetAnimatorOff()
        {
            animator.enabled = false;
        }

        public void CrossFadeToState(int stateHash, float duration = 0.2f, int layer = -1, float normalizedTime = 0f)
        {
            animator.CrossFade(stateHash, duration, layer, normalizedTime);
        }

    }
}