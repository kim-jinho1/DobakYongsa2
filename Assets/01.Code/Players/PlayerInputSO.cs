using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Players
{
    [CreateAssetMenu(fileName = "PlayerInput", menuName = "SO/PlayerInput", order = 0)]
    public class PlayerInputSO : ScriptableObject, Controls.IPlayerActions
    {
        [SerializeField] private LayerMask whatIsGround;
        
        public event Action OnAttackPressed;
        public event Action OnRollingPressed;

        public Vector2 MovementKey { get; private set; }
        private Controls _controls;

        private Vector3 _worldPosition;
        private Vector2 _screenPosition;

        private void OnEnable()
        {
            if (_controls == null)
            {
                _controls = new Controls();
                _controls.Player.SetCallbacks(this);
            }
            _controls.Player.Enable();
        }

        private void OnDisable()
        {
            _controls.Player.Disable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            Vector2 movementKey = context.ReadValue<Vector2>();
            MovementKey = movementKey;
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if(context.performed)
                OnAttackPressed?.Invoke();
        }

        public void OnRolling(InputAction.CallbackContext context)
        {
            if(context.performed)
                OnRollingPressed?.Invoke();
        }

        public Vector3 GetWorldPosition()
        {
            Camera mainCam = Camera.main;
            Debug.Assert(mainCam != null, "No main camera in this scene");
            
            Ray cameraRay = mainCam.ScreenPointToRay(_screenPosition);
            if (Physics.Raycast(cameraRay, out RaycastHit hit, mainCam.farClipPlane, whatIsGround))
            {
                _worldPosition = hit.point;
            }

            return _worldPosition;
        }
        
        public void OnPointer(InputAction.CallbackContext context)
        {
            _screenPosition = context.ReadValue<Vector2>();
        }
    }
}