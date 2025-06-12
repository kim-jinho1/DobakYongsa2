using System;
using UnityEngine;

namespace Code.Entities
{
    public class EntityAnimatorTrigger : MonoBehaviour, IEntityComponent
    {
        public Action OnAnimationEndTrigger;
        public event Action OnAttackVFXTrigger;
        public event Action<bool> OnManualRotationTrigger;
        public event Action OnDamageCastTrigger;
        public event Action OnKickTrigger;
        public event Action OnEnemyAttack;
        private Entity _entity;
        
        public void Initialize(Entity entity)
        {
            _entity = entity;
        }

        private void AnimationEnd()
        {
            OnAnimationEndTrigger?.Invoke();
        }
        
        private void KickCast() => OnKickTrigger?.Invoke();
        private void PlayAttackVFX() => OnAttackVFXTrigger?.Invoke();
        private void StartManualRotation() => OnManualRotationTrigger?.Invoke(true);
        private void StopManualRotation() => OnManualRotationTrigger?.Invoke(false);
        private void DamageCast() => OnDamageCastTrigger?.Invoke();

        private void EnemyAttackCast() => OnEnemyAttack?.Invoke();
    }
}