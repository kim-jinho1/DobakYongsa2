using System;
using Code.Gold;
using Code.Maps;
using UnityEngine;

namespace Code.Managers
{
    public class DataManager : MonoBehaviour
    {
        [SerializeField] private GoldUI goldUI;

        public static bool OnBattle { get; private set; }
        public int Gold { get; private set; }

        private const string GOLD_KEY = "PlayerGold";
        private const string BATTLE_KEY = "IsBattle";

        private void Awake()
        {
            LoadData(); // PlayerPrefs에서 데이터 불러오기
            goldUI?.UpdateGold(Gold); // UI 초기화
            MapUI.OnSceneLoaded += Battle;
        }

        private void OnDestroy()
        {
            SaveData(); // 앱 종료 시 데이터 저장
            MapUI.OnSceneLoaded -= Battle;
        }

        private void Battle(bool isBattle)
        {
            OnBattle = isBattle;
            PlayerPrefs.SetInt(BATTLE_KEY, isBattle ? 1 : 0); // bool -> int
            PlayerPrefs.Save();
        }

        public void UpGold(int goldAmount)
        {
            Gold += goldAmount;
            PlayerPrefs.SetInt(GOLD_KEY, Gold); // 저장
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
    }
}