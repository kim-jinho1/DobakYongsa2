using Code.Entities;
using UnityEngine;
using Code.Players.Components;

namespace Code.Players.States
{
    public class PlayerRollingState : PlayerState
    {
        private MovementCompo _movementCompo;
        private bool _isRolling;
        private Vector3 _rollingDirection;
        
        public PlayerRollingState(Entity entity, int animationHash) : base(entity, animationHash)
        {
            _movementCompo = entity.GetCompo<MovementCompo>();
        }

        public override void Enter()
        {
            base.Enter();
            _movementCompo.CanManualMovement = false;
            _isRolling = false;

            _animatorTrigger.OnRollingStatusChange += HandleRollingStatusChange;
            _rollingDirection = _player.transform.forward;
        }

        public override void Exit()
        {
            _movementCompo.CanManualMovement = true;
            _animatorTrigger.OnRollingStatusChange -= HandleRollingStatusChange;
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
            
            if(_isTriggerCall)
                _player.ChangeState("IDLE");
        }

        private void HandleRollingStatusChange(bool isActive)
        {
            if (_isRolling != isActive && isActive)
            {
                _movementCompo.SetAutoMovement(_rollingDirection * _player.rollingVelocity);
            }
            else if(isActive == false)
            {
                _movementCompo.SetAutoMovement(_rollingDirection * (_player.rollingVelocity * 0.2f));
            }
            _isRolling = isActive;   
        }
    }
}