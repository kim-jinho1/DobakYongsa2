using Code.Combat;
using Code.Core.StatSystem;
using Code.Entities;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Enemies
{
    public class NavMovement : MonoBehaviour, IEntityComponent, IKnockBackable, IAfterInitialize
    {
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private StatSO moveSpeedStat; 
        [SerializeField] private float stopOffset = 0.05f;
        [SerializeField] private float rotateSpeed = 10f;
        
        private Entity _entity;
        private EntityStatCompo _statCompo;
        
        public bool IsArrived => !agent.pathPending && agent.remainingDistance < agent.stoppingDistance + stopOffset;
        public float RemainDistance => agent.pathPending ? -1 : agent.remainingDistance;
        
        public void Initialize(Entity entity)
        {
            _entity = entity;
            _statCompo = entity.GetCompo<EntityStatCompo>();
            
        }
        
        public void AfterInitialize()
        {
            StatSO targetSO = _statCompo.GetStat(moveSpeedStat);
            Debug.Assert(targetSO != null, $"{moveSpeedStat.statName} cannot found in statCompo");
            targetSO.OnValueChanged += HandleMoveSpeedChange;
        }

        private void OnDestroy()
        {
            _entity.transform.DOKill();
            StatSO targetSO = _statCompo.GetStat(moveSpeedStat);
            Debug.Assert(targetSO != null, $"{moveSpeedStat.statName} cannot found in statCompo");
            targetSO.OnValueChanged -= HandleMoveSpeedChange;
        }

        private void HandleMoveSpeedChange(StatSO stat, float currentvalue, float previousvalue)
        {
            agent.speed = currentvalue;
        }

        private void Update()
        {
            if (agent.hasPath && agent.isStopped == false && agent.path.corners.Length > 0)
            {
                LookAtTarget(agent.steeringTarget);
            }
        }
        
        /// <summary>
        /// 바라봐야할 최종 로테이션을 반환합니다.
        /// </summary>
        /// <param name="target">바라볼 목표지점을 넣습니다. y축은 무시</param>
        /// <param name="isSmooth">부드럽게 돌아갈 것인지 결정합니다.</param>
        /// <returns></returns>
        public Quaternion LookAtTarget(Vector3 target, bool isSmooth = true)
        {
            Vector3 direction = target - _entity.transform.position;
            direction.y = 0;
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            if (isSmooth)
            {
                _entity.transform.rotation = Quaternion.Slerp(_entity.transform.rotation, 
                                                lookRotation, Time.deltaTime * rotateSpeed);
            }
            else
            {
                _entity.transform.rotation = lookRotation;
            }

            return lookRotation;
        }

        public void SetStop(bool isStop) => agent.isStopped = isStop;
        public void SetVelocity(Vector3 velocity) => agent.velocity = velocity; 
        public void SetSpeed(float speed) => agent.speed = speed;
        public void SetDestination(Vector3 destination) => agent.SetDestination(destination);
        
        public void KnockBack(Vector3 force, float duration)
        {
            SetStop(true);
            Vector3 destination = GetKnockBackEndPoint(force);
            Vector3 delta = destination - _entity.transform.position;
            float kbDuration = delta.magnitude * duration / force.magnitude; 

            _entity.transform.DOMove(destination, kbDuration).SetEase(Ease.OutCirc)
                .OnComplete(() =>
                {
                    agent.Warp(transform.position); 
                    SetStop(false);
                });
        }

        private Vector3 GetKnockBackEndPoint(Vector3 force)
        {
            Vector3 startPosition = _entity.transform.position + new Vector3(0, 0.5f); 
            if (Physics.Raycast(startPosition, force.normalized, out RaycastHit hit, force.magnitude))
            {
                Vector3 hitPoint = hit.point;
                hitPoint.y = _entity.transform.position.y;
                return hitPoint;
            }

            return _entity.transform.position + force;
        }
    }
}