using Code.Entities;
using Code.FSM;
using GondrLib.Dependencies;
using UnityEngine;

namespace Code.Players
{
    public class Player : Entity, IDependencyProvider
    {
        [field:SerializeField] public PlayerInputSO PlayerInput { get; private set; }

        [SerializeField] private GameObject sword;

        [SerializeField] private StateDataSO[] stateDataList;
        
        private EntityStateMachine _stateMachine;

        [field: SerializeField] public bool OnBattle  { get; private set; }

        [Provide]
        public Player ProvidePlayer() => this;
        
        #region Temp region
        public float rollingVelocity = 12f;
        #endregion
        
        protected override void Awake()
        {
            base.Awake();
            _stateMachine = new EntityStateMachine(this, stateDataList);
            PlayerInput.OnRollingPressed += HandleRollingPressed;
        }

        private void OnDestroy()
        {
            PlayerInput.OnRollingPressed -= HandleRollingPressed;
        }

        private void HandleRollingPressed()
        {
            if (OnBattle)
                ChangeState("KICK");
        }

        private void Start()
        {
            _stateMachine.ChangeState("IDLE");
        }

        private void Update()
        {
            _stateMachine.UpdateStateMachine();
        }

        public void ChangeState(string newStateName) 
            => _stateMachine.ChangeState(newStateName);
        
    }
}