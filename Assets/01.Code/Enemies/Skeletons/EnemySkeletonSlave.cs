using System;
using System.Collections;
using Code.BT.Events;
using Code.Combat;
using Code.Entities;
using UnityEngine;
using UnityEngine.Events;

namespace Code.Enemies.Skeletons
{
    public class EnemySkeletonSlave : Enemy, IKnockBackable
    {
        [Header("Components")]
        [SerializeField] private GameObject visual;
        [SerializeField] private GameObject respawnPos;
        [SerializeField] private ParticleSystem particleSystem;
        [SerializeField] private ParticleSystem respawnParticles;
        [SerializeField] private EntityAnimatorTrigger entityAnimatorTrigger;

        [Header("Combat")]
        [SerializeField] private float enemyAttackRange = 1.5f;
        [SerializeField] private float attackRadius = 0.8f;
        [SerializeField] private LayerMask whatIsPlayer;
        [SerializeField] private float attackDamage = 5f;
        [SerializeField] private AttackDataSO attackData;

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
            entityAnimatorTrigger.OnEnemyAttack += PerformAttack;
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
            entityAnimatorTrigger.OnEnemyAttack -= PerformAttack;

        }

        private void HandleDeathEvent()
        {
            if (IsDead) return;
            IsDead = true;
            _collider.enabled = false;
            _stateChangeChannel.SendEventMessage(EnemyState.DEAD);
            StartCoroutine(RespawnRoutine());
        }

        private IEnumerator RespawnRoutine()
        {
            yield return new WaitForSeconds(3f);
            respawnParticles.Play();
            visual.SetActive(false);

            yield return new WaitForSeconds(5f);
            Respawn();
        }

        private void Respawn()
        {
            _health.ResetHealth();
            _collider.enabled = true;
            IsDead = false;
            visual.SetActive(true);
            transform.position = respawnPos.transform.position;
            respawnParticles.Play();
            _stateChangeChannel.SendEventMessage(EnemyState.RESPAWN);
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

        private void PerformAttack()
        {
            Collider[] hits = Physics.OverlapSphere(
                transform.position + transform.forward * 0.5f,
                attackRadius,
                whatIsPlayer);

            if (hits.Length == 0) return;

            var playerCollider = hits[0];

            Vector3 center = transform.position + transform.forward * 1f;
            Vector3 hitPoint = playerCollider.ClosestPoint(center);
            Vector3 hitNormal = (hitPoint - center).normalized;

            if (playerCollider.TryGetComponent(out IDamageable damageable))
            {
                DamageData damageData = new DamageData
                {
                    damage = attackDamage,
                    source = gameObject
                };

                damageable.ApplyDamage(damageData, hitPoint, hitNormal, attackData, this);
            }

            if (playerCollider.TryGetComponent(out IKnockBackable knockBackable))
            {
                Vector3 force = transform.forward * attackData.knockBackForce;
                knockBackable.KnockBack(force, attackData.knockBackDuration);
            }
        }


#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Vector3 startPos = transform.position + transform.forward * 0.5f;
            Vector3 endPos = startPos + transform.forward * enemyAttackRange;
            Vector3 center = (startPos + endPos) * 0.5f;

            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(center, attackRadius);
        }
#endif
    }
    
}
