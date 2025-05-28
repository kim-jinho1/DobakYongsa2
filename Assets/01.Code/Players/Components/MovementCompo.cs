using System;
using Code.Core.StatSystem;
using Code.Entities;
using UnityEngine;

namespace Code.Players.Components
{
    public class MovementCompo : MonoBehaviour, IEntityComponent, IAfterInitialize
    {
        [Header("Move Setting")]
        [SerializeField] private StatSO moveSpeedStat;
        [SerializeField] private float gravity = -9.81f;
        [SerializeField] private float rotationSpeed = 8f;
        [SerializeField] private CharacterController characterController;
        [Header("Camera Setting")]
        [SerializeField] private Camera mainCamera;

        [SerializeField] private float smoothness= 10f;
        public bool CanManualMovement { get; set; } = true;
        
        private float _moveSpeed = 8f;
        private Vector3 _autoMovement;
        public bool IsGround => characterController.isGrounded;

        private Vector3 _velocity;
        public Vector3 Velocity => _velocity;
        
        private float _verticalVelocity;
        private Vector3 _movementDirection;

        private bool _toggleCameraRotation = true;

        private Entity _entity;
        private EntityStatCompo _statCompo;
        public void Initialize(Entity entity)
        {
            _entity = entity;
            _statCompo = entity.GetCompo<EntityStatCompo>();
        }
        
        public void AfterInitialize()
        {
            StatSO targetStat = _statCompo.GetStat(moveSpeedStat);
            Debug.Assert(targetStat != null, $"{moveSpeedStat.statName} stat could not found");
            targetStat.OnValueChanged += HandleMoveSpeedChange;
            _moveSpeed = targetStat.Value;
        }

        private void OnDestroy()
        {
            StatSO targetStat = _statCompo.GetStat(moveSpeedStat);
            Debug.Assert(targetStat != null, $"{moveSpeedStat.statName} stat could not found");
            targetStat.OnValueChanged -= HandleMoveSpeedChange;
        }

        private void HandleMoveSpeedChange(StatSO stat, float currentvalue, float previousvalue)
        {
            _moveSpeed = currentvalue;
        }

        public void SetMovementDirection(Vector2 input)
        {
            Transform cam = mainCamera.transform;

            Vector3 forward = cam.forward;
            Vector3 right = cam.right;

            forward.y = 0;
            right.y = 0;

            _movementDirection = (forward.normalized * input.y + right.normalized * input.x).normalized;
        }
        
        private void FixedUpdate()
        {
            CalculateMovement();
            ApplyGravity();
            Move();
        }

        private void CalculateMovement()
        {
            if (CanManualMovement)
            {
                _velocity = _movementDirection * (_moveSpeed * Time.fixedDeltaTime);
            }
            else
            {
                _velocity = _autoMovement * Time.fixedDeltaTime;
            }

            if (_velocity.sqrMagnitude > 0.0001f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(new Vector3(_velocity.x, 0, _velocity.z));
                Transform parent = _entity.transform;
                parent.rotation = Quaternion.Lerp(parent.rotation, targetRotation, Time.fixedDeltaTime * rotationSpeed);
            }
        }

        
        private void ApplyGravity()
        {
            if (IsGround && _verticalVelocity < 0)
                _verticalVelocity = -0.03f;
            else
                _verticalVelocity += gravity * Time.fixedDeltaTime;
            
            _velocity.y = _verticalVelocity;
        }
        
        private void Move()
        {
            characterController.Move(_velocity);
        }
        
        public void StopImmediately()
        {
            _movementDirection = Vector3.zero;
        }

        public void SetAutoMovement(Vector3 autoMovement) => _autoMovement = autoMovement;

        private void LateUpdate() {
            if (_toggleCameraRotation != true) {
                Vector3 playerRotate = Vector3.Scale(mainCamera.transform.forward, new Vector3(1, 0, 1));
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRotate), Time.deltaTime * smoothness);
            }
        }
    }
}