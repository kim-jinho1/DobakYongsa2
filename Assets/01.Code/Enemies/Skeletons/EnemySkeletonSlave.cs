using Code.BT.Events;
using Code.Combat;
using UnityEngine;
using UnityEngine.Events;

namespace Code.Enemies.Skeletons
{
    public class EnemySkeletonSlave : Enemy, IKnockBackable
    {
        public UnityEvent<Vector3, float> OnKnockBackEvent;
        
        private StateChange _stateChangeChannel;
        private CapsuleCollider _collider;

        protected override void Awake()
        {
            base.Awake();
            _collider = GetComponent<CapsuleCollider>();
            OnDeathEvent.AddListener(HandleDeathEvent);
            OnHitEvent.AddListener(EnemyHit);
        }

        protected override void Start()
        {
            base.Start();
            _stateChangeChannel = GetBlackboardVariable<StateChange>("StateChannel").Value;
        }

        private void OnDestroy()
        {
            OnDeathEvent.RemoveListener(HandleDeathEvent);
            OnHitEvent.RemoveListener(EnemyHit);
        }

        private void HandleDeathEvent()
        {
            if(IsDead)
                return;
            IsDead = true;
            _collider.enabled = false;
            _stateChangeChannel.SendEventMessage(EnemyState.DEAD);
        }

        public void EnemyHit()
        {
            _stateChangeChannel.SendEventMessage(EnemyState.HIT);
        }

        public void KnockBack(Vector3 force, float duration)
        {
            OnKnockBackEvent?.Invoke(force, duration);
        }
    }
}