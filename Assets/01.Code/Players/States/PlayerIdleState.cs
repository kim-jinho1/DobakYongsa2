using Code.Entities;
using UnityEngine;
using Code.Players.Components;

namespace Code.Players.States
{
    public class PlayerIdleState : PlayerCanAttackState
    {
        private MovementCompo _movementCompo;
        public PlayerIdleState(Entity entity, int animationHash) : base(entity, animationHash)
        {
            _movementCompo = entity.GetCompo<MovementCompo>();
        }

        public override void Update()
        {
            base.Update();
            Vector2 movementKey = _player.PlayerInput.MovementKey;
            _movementCompo.SetMovementDirection(movementKey);
            if (movementKey.magnitude > _inputThreshold)
            {
                _player.ChangeState("MOVE");
            }
        }
    }
}