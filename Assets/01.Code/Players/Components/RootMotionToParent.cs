using UnityEngine;

namespace Code.Players.Components
{
    public class RootMotionToParent : MonoBehaviour
    {
        public Transform parent;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnAnimatorMove()
        {
            if (_animator.applyRootMotion == false || parent == null)
                return;

            Vector3 deltaPos = _animator.deltaPosition;
            Quaternion deltaRot = _animator.deltaRotation;
            

            parent.position += deltaPos;
            parent.rotation *= deltaRot;

            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }

    }
}