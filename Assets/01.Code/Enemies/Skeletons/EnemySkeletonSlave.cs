using Code.BT.Events;
using Code.Combat;
using UnityEngine;
using UnityEngine.Events;

namespace Code.Enemies.Skeletons
{
    public class EnemySkeletonSlave : Enemy, IKnockBackable
    {
        [SerializeField] private ParticleSystem particleSystem;
        public UnityEvent<Vector3, float> OnKnockBackEvent;
        
        private StateChange _stateChangeChannel;
        private CapsuleCollider _collider;
        private EntityHealth _health;

        protected override void Awake()
        {
            base.Awake();
            _collider = GetComponent<CapsuleCollider>();
            _health = GetComponent<EntityHealth>();
            OnDeathEvent.AddListener(HandleDeathEvent);
            OnHitEvent.AddListener(HandleHitEvent);
        }

        protected override void Start()
        {
            base.Start();
            _stateChangeChannel = GetBlackboardVariable<StateChange>("StateChannel").Value;
        }

        private void OnDestroy()
        {
            OnDeathEvent.RemoveListener(HandleDeathEvent);
            OnHitEvent.RemoveListener(HandleHitEvent);
        }

        private void HandleDeathEvent()
        {
            if(IsDead)
                return;
            IsDead = true;
            _collider.enabled = false;
            _stateChangeChannel.SendEventMessage(EnemyState.DEAD);
        }

        private void HandleHitEvent()
        {
            if (_health.CurrentHealth <= 0)
            {
                particleSystem.Play();
                _stateChangeChannel.SendEventMessage(EnemyState.DEAD);
            }
            else
            {
                particleSystem.Play();
                _stateChangeChannel.SendEventMessage(EnemyState.HIT);
            }
        }

        public void KnockBack(Vector3 force, float duration)
        {
            OnKnockBackEvent?.Invoke(force, duration);
        }
    }
}