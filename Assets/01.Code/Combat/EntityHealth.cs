using Code.Core.StatSystem;
using Code.Entities;
using UnityEngine;

namespace Code.Combat
{
    public class EntityHealth : MonoBehaviour, IEntityComponent, IDamageable, IAfterInitialize
    {
        private Entity _entity;
        private ActionData _actionData;
        private EntityStatCompo _statCompo;

        [SerializeField] private StatSO hpStat;
        [SerializeField] private float maxHealth;
        [field: SerializeField] public float CurrentHealth { get; private set; }

        public void Initialize(Entity entity)
        {
            _entity = entity;
            _actionData = entity.GetCompo<ActionData>();
            _statCompo = entity.GetCompo<EntityStatCompo>();
        }
        
        public void AfterInitialize()
        {
            StatSO target = _statCompo.GetStat(hpStat);
            Debug.Assert(target != null, $"{hpStat.statName} does not exist");
            target.OnValueChanged += HandleMaxHPChanged;
            CurrentHealth = maxHealth = target.Value;
        }

        private void OnDestroy()
        {
            StatSO target = _statCompo.GetStat(hpStat);
            Debug.Assert(target != null, $"{hpStat.statName} does not exist");
            target.OnValueChanged -= HandleMaxHPChanged;
        }

        private void HandleMaxHPChanged(StatSO stat, float currentvalue, float previousvalue)
        {
            float changed = currentvalue - previousvalue;
            maxHealth = currentvalue;
            if (changed > 0)
                CurrentHealth = Mathf.Clamp(CurrentHealth + changed, 0, maxHealth);
            else
                CurrentHealth = Mathf.Clamp(CurrentHealth, 0, maxHealth);
        }
        
        public void ResetHealth()
        {
            CurrentHealth = maxHealth;
        }

        public void ApplyDamage(DamageData damageData, Vector3 hitPoint, Vector3 hitNormal, AttackDataSO attackData, Entity dealer)
        {
            _actionData.HitNormal = hitNormal;
            _actionData.HitPoint = hitPoint;
            CurrentHealth = Mathf.Clamp(CurrentHealth - damageData.damage, 0, maxHealth);
            if (CurrentHealth <= 0)
            {
                _entity.OnDeathEvent?.Invoke();
            }
            
            _entity.OnHitEvent?.Invoke();
        }
    }
}