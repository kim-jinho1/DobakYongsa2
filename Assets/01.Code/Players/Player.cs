using Code.Combat;
using Code.Entities;
using Code.FSM;
using GondrLib.Dependencies;
using UnityEngine;

namespace Code.Players
{
    public class Player : Entity, IDependencyProvider , IDamageable, IKnockBackable 
    {
        [field:SerializeField] public PlayerInputSO PlayerInput { get; private set; }

        [SerializeField] private EntityHealth entityHealth;

        [SerializeField] private GameObject sword;

        [SerializeField] private StateDataSO[] stateDataList;

        public GameObject button;

        public bool IsHit { get; set; } = false;
        [field:SerializeField] public LayerMask WhatIsDoBak { get; private set; }
        
        private EntityStateMachine _stateMachine;

        [field: SerializeField] public bool OnBattle  { get; private set; }

        [Provide]
        public Player ProvidePlayer() => this;
        
        #region Temp region
        public float rollingVelocity = 12f;
        #endregion
        
        protected override void Awake()
        {
            base.Awake();
            _stateMachine = new EntityStateMachine(this, stateDataList);
            PlayerInput.OnRollingPressed += HandleRollingPressed;
        }

        private void OnDestroy()
        {
            PlayerInput.OnRollingPressed -= HandleRollingPressed;
        }

        private void HandleRollingPressed()
        {
            if (OnBattle)
                ChangeState("KICK");
        }

        private void Start()
        {
            _stateMachine.ChangeState("IDLE");
        }

        private void Update()
        {
            _stateMachine.UpdateStateMachine();
        }

        public void ChangeState(string newStateName) 
            => _stateMachine.ChangeState(newStateName);


        public void ApplyDamage(DamageData damageData, Vector3 hitPoint, Vector3 hitNormal, AttackDataSO attackData, Entity dealer)
        {
            if(IsHit)
                return;
            _stateMachine.ChangeState("HIT");
            entityHealth.ApplyDamage(damageData, hitPoint, hitNormal, attackData, dealer);
        }

        public void KnockBack(Vector3 force, float duration)
        {
            
        }
    }
}