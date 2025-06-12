using Code.Entities;
using Code.Players.Components;

namespace Code.Players.States
{
    public class PlayerAttackState : PlayerState
    {
        private PlayerAttackCompo _attackCompo;
        private MovementCompo _movementCompo;
        private RootMotionCompo _rootMotion;
        
        public PlayerAttackState(Entity entity, int animationHash) : base(entity, animationHash)
        {
            _movementCompo = entity.GetCompo<MovementCompo>();
            _attackCompo = entity.GetCompo<PlayerAttackCompo>();
            _rootMotion = entity.GetCompo<RootMotionCompo>();
        }
        
        public override void Enter()
        {
            _entityAnimator.SetParam(_animationHash, true);
            _isTriggerCall = false;
            _animatorTrigger.OnAnimationEndTrigger += AnimationEndTrigger;
            _attackCompo.Attack();
            _movementCompo.CanManualMovement = false;
        }

        public override void Exit()
        {
            _attackCompo.EndAttack();
            _movementCompo.CanManualMovement = true;
            _movementCompo.StopImmediately();
            _entityAnimator.SetParam(_animationHash, false);
            _animatorTrigger.OnAnimationEndTrigger -= AnimationEndTrigger;
        }
        public override void Update()
        {
            base.Update();
            if(_isTriggerCall)
                _player.ChangeState("IDLE");
        }
    }
}