using Code.Entities;
using UnityEngine;
using Code.Players.Components;

namespace Code.Players.States
{
    public class PlayerMoveState : PlayerCanAttackState
    {
        private MovementCompo _movementCompo;
        private EntityVFX _vfxCompo;
        private readonly string _footStepEffectName = "FootStep";
        public PlayerMoveState(Entity entity, int animationHash) : base(entity, animationHash)
        {
            _movementCompo = entity.GetCompo<MovementCompo>();
            _vfxCompo = entity.GetCompo<EntityVFX>();
        }

        public override void Enter()
        {
            base.Enter();
            //_vfxCompo.PlayVfx(_footStepEffectName, Vector3.zero, Quaternion.identity);
        }

        public override void Exit()
        {
            //_vfxCompo.StopVfx(_footStepEffectName);
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
            Vector2 movementKey = _player.PlayerInput.MovementKey;
            _movementCompo.SetMovementDirection(movementKey);
            if(movementKey.magnitude < _inputThreshold)
                            _player.ChangeState("IDLE");
            
            bool isHit = Physics.SphereCast(
                _player.transform.position, 1, 
                _player.transform.forward, 
                out RaycastHit hit, 
                1,_player.WhatIsDoBak);
            
            if(isHit)
                _player.ChangeState("DOBAK");
            
        }
    }
}