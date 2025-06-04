using Code.Gold;
using UnityEngine;

namespace Code.Managers
{
    public class DataManager : MonoBehaviour
    {
        [SerializeField] private GoldUI goldUI;
        public bool OnBattle { get; set; }
        public int Gold { get; set; }
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            goldUI.UpdateGold(Gold);
        }

        public void UpGold(int goldAmount)
        {
            Gold += goldAmount;
            goldUI.UpdateGold(Gold);
        }
    }
}