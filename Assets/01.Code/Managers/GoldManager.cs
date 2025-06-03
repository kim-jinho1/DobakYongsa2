using _01.Code.Gold;
using UnityEngine;

namespace Code.Managers
{
    public class GoldManager : MonoBehaviour
    {
        [SerializeField] private GoldUI goldUI;

        private int _gold;
        
        public  void UpGold(int gold)
        {
            _gold += gold;
            goldUI.UpdateGold(_gold);
        }
    }
}