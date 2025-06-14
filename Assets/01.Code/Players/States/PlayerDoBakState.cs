// PlayerDoBakState.cs
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
        private Vector3 _originalCamPos;
        private Quaternion _originalCamRot;

        public override void Enter()
        {
            base.Enter();

            _player.GetComponent<CharacterController>().enabled = false;
            _player.transform.position = _doBak.Position.position;
            _player.GetComponent<CharacterController>().enabled = true;

            _player.IsDoingDoBak = true;

            _player.transform.forward = Vector3.forward;
            
            _originalCamPos = _player.playerCamera.transform.position;
            _originalCamRot = _player.playerCamera.transform.rotation;
            
            _player.playerCamera.transform.position = _doBak.CameraTarget.position;
            _player.playerCamera.transform.rotation = Quaternion.Euler(40f, _doBak.CameraTarget.eulerAngles.y, 0f);
        }

        public override void Exit()
        {
            _player.playerCamera.transform.position = _originalCamPos;
            _player.playerCamera.transform.rotation = _originalCamRot;

            _player.IsDoingDoBak = false;
            base.Exit();
        }
    }
}