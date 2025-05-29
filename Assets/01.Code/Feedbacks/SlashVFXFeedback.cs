using Code.Effects;
using Code.Entities;
using DG.Tweening;
using GondrLib.Dependencies;
using GondrLib.ObjectPool.Runtime;
using UnityEngine;

namespace Code.Feedbacks
{
    public class SlashVFXFeedback : Feedback
    {
        [Inject] private PoolManagerMono _poolManager;

        [SerializeField] private float effectPlayTime;
        [SerializeField] private ActionData actionData;
        [SerializeField] private PoolingItemSO slashEffect;

        
        public override void CreateFeedback()
        {
               
            PoolingEffect effect = _poolManager.Pop<PoolingEffect>(slashEffect);
            effect.PlayVFX(actionData.HitPoint, Quaternion.identity);

            DOVirtual.DelayedCall(effectPlayTime, () =>
            {
                _poolManager.Push(effect);
            });
        }

        public override void StopFeedback()
        {
        }
    }
}