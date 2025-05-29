using System.Collections.Generic;
using System.Linq;
using Code.Entities;
using UnityEngine;

namespace Code.Enemies
{
    public class RagDollCompo : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private Transform ragDollParentTrm;
        [SerializeField] private LayerMask whatIsBody;

        private List<RagDollPart> _partList;
        private Collider[] _results;

        private RagDollPart _defaultPart;
        
        private ActionData _actionData;
        public void Initialize(Entity entity)
        {
            _actionData = entity.GetCompo<ActionData>();
            _results = new Collider[1];
            _partList = ragDollParentTrm.GetComponentsInChildren<RagDollPart>().ToList();
            foreach (RagDollPart part in _partList)
            {
                part.Initialize();
            }
            _defaultPart = _partList[0];
            SetRagDollActive(false);
            SetColliderActive(false);
            
            entity.OnDeathEvent.AddListener(HandleDeathEvent);
        }

        private void HandleDeathEvent()
        {
            SetColliderActive(true);
            SetRagDollActive(true);
            const float force = -30f;
            AddForceToRagDoll(_actionData.HitNormal * force, _actionData.HitNormal);
        }

        private void SetColliderActive(bool isActive)
        {
            foreach (RagDollPart part in _partList)
            {
                part.SetRagDollActive(isActive);
            }
        }

        private void SetRagDollActive(bool isActive)
        {
            foreach (RagDollPart part in _partList)
            {
                part.SetCollider(isActive);
            }
        }

        public void AddForceToRagDoll(Vector3 force, Vector3 point)
        {
            int count = Physics.OverlapSphereNonAlloc(point, 0.5f, _results, whatIsBody);
            if (count < 0)
            {
                _results[0].GetComponent<RagDollPart>().KnockBack(force, point);
            }
            else
            {
                _defaultPart.KnockBack(force, point);
            }
        }
        
    }
}