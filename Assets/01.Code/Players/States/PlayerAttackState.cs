using Code.Combat;
using Code.Entities;
using Code.Players.Components;
using UnityEngine;

namespace Code.Players.States
{
    public class PlayerAttackState : PlayerState
    {
        private PlayerAttackCompo _attackCompo;
        private MovementCompo _movementCompo;
        
        public PlayerAttackState(Entity entity, int animationHash) : base(entity, animationHash)
        {
            _movementCompo = entity.GetCompo<MovementCompo>();
            _attackCompo = entity.GetCompo<PlayerAttackCompo>();
        }
        public override void Enter()
        {
            base.Enter();
            _attackCompo.Attack();

            _movementCompo.CanManualMovement = false;
            ApplyAttackData();
        }

        private void ApplyAttackData()
        {
            AttackDataSO currentAtkData = _attackCompo.GetCurrentAttackData();
            Vector3 playerDirection = GetPlayerDirection();
            _player.transform.rotation = Quaternion.LookRotation(playerDirection);

            Vector3 movement = playerDirection * currentAtkData.movementPower;
            _movementCompo.SetAutoMovement(movement);
        }

        private Vector3 GetPlayerDirection()
        {
            if(_attackCompo.useMouseDirection == false)
                return _player.transform.forward;
            
            Vector3 targetPos = _player.PlayerInput.GetWorldPosition();
            Vector3 direction = targetPos - _player.transform.position;
            direction.y = 0;
            return direction.normalized;
        }

        public override void Exit()
        {
            _attackCompo.EndAttack();
            _movementCompo.CanManualMovement = true;
            _movementCompo.StopImmediately();
            base.Exit();
        }
        public override void Update()
        {
            base.Update();
            if(_isTriggerCall)
                _player.ChangeState("IDLE");
        }
    }
}