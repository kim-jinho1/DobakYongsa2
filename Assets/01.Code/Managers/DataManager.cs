using Code.Gold;
using Code.Maps;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Managers
{
    public class DataManager : MonoBehaviour
    {
        [SerializeField] private GoldUI goldUI;

        public bool OnBattle { get; private set; }
        public int Gold { get; set; }

        private const string GOLD_KEY = "PlayerGold";
        private const string BATTLE_KEY = "IsBattle";

        private void Awake()
        {
            LoadData();
            goldUI?.UpdateGold(Gold);
            MapUI.OnSceneLoaded += Battle;
        }

        private void OnDestroy()
        {
            SaveData();
            MapUI.OnSceneLoaded -= Battle;
        }

        private void Battle(bool isBattle)
        {
            OnBattle = isBattle;
            PlayerPrefs.SetInt(BATTLE_KEY, isBattle ? 1 : 0);
            PlayerPrefs.Save();
        }

        public void UpGold(int goldAmount)
        {
            Gold += goldAmount;
            PlayerPrefs.SetInt(GOLD_KEY, Gold);
            PlayerPrefs.Save();

            goldUI?.UpdateGold(Gold);
        }

        private void LoadData()
        {
            Gold = PlayerPrefs.GetInt(GOLD_KEY, 0);
            OnBattle = PlayerPrefs.GetInt(BATTLE_KEY, 0) == 1;
        }

        private void SaveData()
        {
            PlayerPrefs.SetInt(GOLD_KEY, Gold);
            PlayerPrefs.SetInt(BATTLE_KEY, OnBattle ? 1 : 0);
            PlayerPrefs.Save();
        }

        public void UpdateGold()
        {
            goldUI.UpdateGold(Gold);
        }

        public void Update()
        {
            if (Gold >= 1000)
            {
                SceneManager.LoadScene("EndingScene");
            }
        }
    }
}