using Code.Combat;
using Code.Core.StatSystem;
using Code.Entities;
using UnityEngine;

namespace Code.Players.Components
{
    public class PlayerKickCompo : MonoBehaviour, IEntityComponent, IAfterInitialize
    {
        [SerializeField] private AttackDataSO attackStat;
        [SerializeField] private StatSO attackSpeedStat;
        [SerializeField] private StatSO physicalDamageStat;
        [SerializeField] private DamageCaster damageCaster;
        private Entity _entity;
        public AudioSource kickSoundID;
        private EntityAnimatorTrigger _animatorTrigger;
        private EntityStatCompo _statCompo;
        private DamageCompo _damageCompo;

        public void Initialize(Entity entity)
        {
            _entity = entity;
            _animatorTrigger = entity.GetCompo<EntityAnimatorTrigger>();
            _statCompo = entity.GetCompo<EntityStatCompo>();
            _damageCompo = entity.GetCompo<DamageCompo>();
        }

        public void AfterInitialize()
        {
            _animatorTrigger.OnKickTrigger += HandleDamageCasterTrigger;
        }

        private void OnDestroy()
        {
            _animatorTrigger.OnKickTrigger -= HandleDamageCasterTrigger;
        }

        private void HandleDamageCasterTrigger()
        {
            AttackDataSO attackData = GetCurrentAttackData();
            DamageData damageData = _damageCompo.CalculateDamage(physicalDamageStat, attackData);
            
            Vector3 position = damageCaster.transform.position;
            damageCaster.CastDamage(damageData, position, _entity.transform.forward, attackData);
            kickSoundID.Play();
        }

        private AttackDataSO GetCurrentAttackData()
        {
            return attackStat;
        }
    }
}