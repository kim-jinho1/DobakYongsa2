using Code.Entities;
using Code.Players.Components;
using UnityEngine;

namespace Code.Players.States
{
    public class PlayerDieState : PlayerState
    {
        private PlayerDieCompo _dieCompo;
        private RootMotionCompo _rootMotionCompo;
        
        public PlayerDieState(Entity entity, int animationHash) : base(entity, animationHash)
        {
            _dieCompo = _entity.GetCompo<PlayerDieCompo>();
            _rootMotionCompo = _entity.GetCompo<RootMotionCompo>();
        }

        public override void Enter()
        {
            base.Enter();
            _player.IsDie = true;
            _dieCompo.PlayerDie();
        }

        public override void Update()
        {
            base.Update();
            
        }
    }
}