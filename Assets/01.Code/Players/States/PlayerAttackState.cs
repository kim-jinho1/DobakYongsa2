using Code.Entities;
using Code.Players.Components;

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