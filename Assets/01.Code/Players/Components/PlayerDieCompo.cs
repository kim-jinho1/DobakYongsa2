using System;
using Code.Entities;
using Code.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Players.Components
{
    public class PlayerDieCompo : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private GameObject gameOverUI;
        [SerializeField] private DataManager dataManager;
        
        private Entity _entity;
        public void ReSpawn()
        {
            gameOverUI.SetActive(false);
            dataManager.Gold /= 2;
            dataManager.UpdateGold();
            SceneManager.LoadScene("MainScene");
        }

        public void PlayerDie()
        {
            gameOverUI.SetActive(true);
        }

        public void Initialize(Entity entity)
        {
            _entity = entity;
        }
    }
}