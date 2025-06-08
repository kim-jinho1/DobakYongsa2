using Code.Entities;
using UnityEngine;
using Code.Players.Components;
using UnityEngine.XR;

namespace Code.Players.States
{
    public class PlayerKickState : PlayerState
    {
        private MovementCompo _movementCompo;
        private bool _isRolling;
        private Vector3 _rollingDirection;
        
        public PlayerKickState(Entity entity, int animationHash) : base(entity, animationHash)
        {
            _movementCompo = entity.GetCompo<MovementCompo>();
        }

        public override void Enter()
        {
            base.Enter();
            _animatorTrigger.OnAnimationEndTrigger += ChangeState;
            _movementCompo.CanManualMovement = false;
        }

        private void ChangeState()
        {
            _player.ChangeState("IDLE");
        }

        public override void Exit()
        {
            _animatorTrigger.OnAnimationEndTrigger -= ChangeState;
            _movementCompo.CanManualMovement = true;
            _movementCompo.StopImmediately();
            base.Exit();
        }
    }
}