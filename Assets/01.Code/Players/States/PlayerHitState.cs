using Code.Entities;

namespace Code.Players.States
{
    public class PlayerHitState : PlayerState
    {
        private EntityAnimatorTrigger _entityAnimatorTrigger;
        public PlayerHitState(Entity entity, int animationHash) : base(entity, animationHash)
        {
            _entityAnimatorTrigger = _entity.GetCompo<EntityAnimatorTrigger>();
        }

        public override void Enter()
        {
            base.Enter();
            _entityAnimatorTrigger.OnAnimationEndTrigger += ChangeState;
            _player.IsHit = true;
        }

        private void ChangeState()
        {
            _player.ChangeState("IDLE");
        }

        public override void Exit()
        {
            _player.IsHit = false;
            _entityAnimatorTrigger.OnAnimationEndTrigger -= ChangeState;
            base.Exit();
        }
    }
}