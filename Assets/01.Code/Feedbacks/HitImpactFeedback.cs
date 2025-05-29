using Code.Effects;
using Code.Entities;
using DG.Tweening;
using GondrLib.Dependencies;
using GondrLib.ObjectPool.Runtime;
using UnityEngine;

namespace Code.Feedbacks
{
    public class HitImpactFeedback : Feedback
    {
        [SerializeField] private PoolingItemSO hitImpactItem;
        [SerializeField] private float playDuration = 0.5f;
        [SerializeField] private ActionData actionData;

        [Inject] private PoolManagerMono _poolManager;
        
        public override void CreateFeedback()
        {
            PoolingEffect effect = _poolManager.Pop<PoolingEffect>(hitImpactItem);
            
            Quaternion rotation = Quaternion.LookRotation(actionData.HitNormal * -1);
            effect.PlayVFX(actionData.HitPoint, rotation);

            DOVirtual.DelayedCall(playDuration, ()=>
            {
                  _poolManager.Push(effect);
            });
        }

        public override void StopFeedback()
        {
        }
    }
}