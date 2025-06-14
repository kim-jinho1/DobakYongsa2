using Code.Core.StatSystem;
using Code.Entities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Combat
{
    public class DamageCompo : MonoBehaviour, IEntityComponent, IAfterInitialize
    {
        [SerializeField] private StatSO criticalStat, criticalDamageStat;

        private EntityStatCompo _statCompo;

        private float _critical, _criticalDamage;
        public void Initialize(Entity entity)
        {
            _statCompo = entity.GetCompo<EntityStatCompo>();
        }

        public void AfterInitialize()
        {
            if (criticalStat == null)
                _critical = 0;
            else
            {
                StatSO target = _statCompo.GetStat(criticalStat);
                if (target == null) return;
                target.OnValueChanged += HandleCriticalChange;
                _critical = target.Value;
            }
            
            if (criticalDamageStat == null)
                _criticalDamage = 1f;
            else
            {
                StatSO target = _statCompo.GetStat(criticalDamageStat);
                if (target == null) return;
                target.OnValueChanged += HandleCriticalDamageChange;
                _criticalDamage = target.Value;
            }
        }

        private void OnDestroy()
        {
            StatSO criticalTarget = _statCompo.GetStat(criticalStat);
            if(criticalTarget != null)
                criticalTarget.OnValueChanged -= HandleCriticalChange;
            
            StatSO criticalDamageTarget = _statCompo.GetStat(criticalDamageStat);
            if(criticalDamageTarget != null)
                criticalDamageTarget.OnValueChanged -= HandleCriticalDamageChange;
        }

        private void HandleCriticalDamageChange(StatSO stat, float currentvalue, float previousvalue)
            => _criticalDamage = currentvalue;


        private void HandleCriticalChange(StatSO stat, float currentvalue, float previousvalue)
            => _critical = currentvalue;

        public DamageData CalculateDamage(StatSO majorStat, AttackDataSO attackData, float multiplier = 1f)
        {
            DamageData data = new DamageData();

            var stat = _statCompo.GetStat(majorStat);
            if (stat == null)
            {
                data.damage = 0;
                data.isCritical = false;
                return data;
            }

            data.damage = stat.Value * attackData.damageMultiplier + attackData.damageIncrease * multiplier;

            if (Random.value < _critical)
            {
                data.damage *= _criticalDamage;
                data.isCritical = true;
            }
            else
            {
                data.isCritical = false;
            }

            return data;
        }

    }
}