using UnityEngine;

namespace Code.Combat
{
    [CreateAssetMenu(fileName = "AttackData", menuName = "SO/Combat/AttackData", order = 0)]
    public class AttackDataSO : ScriptableObject
    {
        public string attackName;
        public float movementPower;
        public float damageMultiplier = 1f;
        public float damageIncrease = 0;
        public bool isPowerAttack;
        public float knockBackForce;
        public float knockBackDuration;
        private void OnEnable()
        {
            attackName = this.name;
        }
    }
}