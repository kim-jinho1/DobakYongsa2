using System.Collections;
using Code.CameraSetting;
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
        
        [SerializeField] private HealthBar healthBar;
        
        [SerializeField] private ParticleSystem particleSystem;

        [SerializeField] private EntityHealth entityHealth;

        [SerializeField] private GameObject sword;

        [SerializeField] private StateDataSO[] stateDataList;
        
        [SerializeField] private CharacterController characterController;

        public GameObject button;
        
        public Animator animator;
        
        public bool IsHit { get; set; } = false;
        public bool IsDie { get; set; } = false;
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
            if (healthBar is not null)
                healthBar.SetHealth(entityHealth.CurrentHealth, 100);
            _stateMachine.UpdateStateMachine();
        }

        public void ChangeState(string newStateName) 
            => _stateMachine.ChangeState(newStateName);


        public void ApplyDamage(DamageData damageData, Vector3 hitPoint, Vector3 hitNormal, AttackDataSO attackData, Entity dealer)
        {
            if (IsHit || IsDie)
                return;

            entityHealth.ApplyDamage(damageData, hitPoint, hitNormal, attackData, dealer);
            
            healthBar.SetHealth(entityHealth.CurrentHealth, 100); // 여기서 호출

            if (entityHealth.CurrentHealth <= 0)
            {
                IsDie = true;
                characterController.center += Vector3.up * 0.5f;
                particleSystem.Play();
                _stateMachine.ChangeState("DIE");
                CameraRotation.IsUI = true;
            }
            else
            {
                particleSystem.Play();
                _stateMachine.ChangeState("HIT");
            }
        }


        public void KnockBack(Vector3 force, float duration)
        {
            
        }
    }
}