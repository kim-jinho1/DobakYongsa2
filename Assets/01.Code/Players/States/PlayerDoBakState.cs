using Code.Entities;
using Code.Players.Components;
using UnityEngine;

namespace Code.Players.States
{
    public class PlayerDoBakState : PlayerState
    {
        public GameObject doBakButton;
        private DoBakCompo _doBak;
        private MovementCompo _movementCompo;
        
        public PlayerDoBakState(Entity entity, int animationHash) : base(entity, animationHash)
        {
            _doBak = _entity.GetCompo<DoBakCompo>();
            _movementCompo = entity.GetCompo<MovementCompo>();
        }

        public override void Enter()
        {
            base.Enter();
            _player.transform.position = _doBak.Position.position;
            _player.button.SetActive(true);
            _movementCompo.CanManualMovement = false;
        }

        public override void Exit()
        {
            _movementCompo.CanManualMovement = true;
            base.Exit();
        }
    }
}