using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Maps
{
    public class MapUI : MonoBehaviour
    {
        [SerializeField] private GameObject mapUI;
        [SerializeField] private string nextMapName;
        [SerializeField] private bool onBattle;
        public static event Action<bool> OnSceneLoaded;
        
        private void Awake()
        {
            mapUI.SetActive(false);
        }

        public void ExitButton()
        {
            mapUI.SetActive(false);
            OnSceneLoaded?.Invoke(onBattle);
        }

        public void InButton()
        {
            SceneManager.LoadScene(nextMapName);
        }

        private void Update()
        {
            ReSpawn();
        }

        private void ReSpawn()
        {
            if (Input.GetKeyDown(KeyCode.P) && onBattle)
                SceneManager.LoadScene(nextMapName);
        }
    }
}