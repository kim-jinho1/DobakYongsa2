using Code.Entities;
using Code.Players.Components;
using UnityEngine;

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
            base.Enter();
            _attackCompo.Attack();
            _rootMotion.InRootMotion(_attackCompo._entityAnimator);
            _movementCompo.CanManualMovement = false;
        }

        public override void Exit()
        {
            _attackCompo.EndAttack();
            _movementCompo.CanManualMovement = true;
            _rootMotion.ExitRootMotion(_attackCompo._entityAnimator);
            _player.transform.position = _rootMotion.ChangePositon();
            _player.transform.rotation = _rootMotion.ChangeRotation();
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