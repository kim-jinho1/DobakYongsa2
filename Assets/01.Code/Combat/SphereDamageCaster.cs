using UnityEngine;
using System.Collections;

namespace Code.Combat
{
    public class SphereDamageCaster : DamageCaster
    {
        [SerializeField, Range(0.5f, 3f)] private float castRadius = 1f;
        [SerializeField, Range(0, 1f)] private float castInterpolation = 0.5f;
        [SerializeField, Range(0, 3f)] private float castingRange = 1f;

        public float slowFactor = 0.05f;
        public float slowLength = 4f;
        public float freezeDuration = 0.5f; // ⬅️ 새로 추가됨

        private bool isInSlowMotion;

        public override void CastDamage(DamageData damageData, Vector3 position, Vector3 direction, AttackDataSO attackData)
        {
            Vector3 startPos = position + direction * -castInterpolation * 2;
            Vector3 endPos = startPos + transform.forward * castingRange;
            Vector3 center = (startPos + endPos) * 0.5f;

            Collider[] hits = Physics.OverlapSphere(center, castRadius, whatIsEnemy);

            foreach (var collider in hits)
            {
                if (collider.TryGetComponent(out IDamageable damageable))
                {
                    float damage = 5f;
                    Vector3 hitPoint = collider.ClosestPoint(center);
                    Vector3 hitNormal = (hitPoint - center).normalized;
                    damageable.ApplyDamage(damageData, hitPoint, hitNormal, attackData, _owner);
                    if (!isInSlowMotion) StartCoroutine(DoSlowMotion());
                }

                if (collider.TryGetComponent(out IKnockBackable kb))
                {
                    Vector3 force = transform.forward * attackData.knockBackForce;
                    kb.KnockBack(force, attackData.knockBackDuration);
                }
            }
        }

        private IEnumerator DoSlowMotion()
        {
            isInSlowMotion = true;
            
            Time.timeScale = 0f;
            Time.fixedDeltaTime = 0f;
            

            yield return new WaitForSecondsRealtime(freezeDuration);
            
            CameraShake.Instance.Shake(0.3f, 0.2f);
            Time.timeScale = slowFactor;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;

            float elapsed = 0f;
            while (elapsed < slowLength)
            {
                elapsed += Time.unscaledDeltaTime;
                float t = elapsed / slowLength;
                Time.timeScale = Mathf.Lerp(slowFactor, 1f, t);
                Time.fixedDeltaTime = Time.timeScale * 0.02f;
                yield return null;
            }

            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02f;
            isInSlowMotion = false;
        }
        
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Vector3 startPos = transform.position + transform.forward * -castInterpolation * 2;

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(startPos, castRadius);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(startPos + transform.forward * castingRange, castRadius);
        }
#endif
    }
}
