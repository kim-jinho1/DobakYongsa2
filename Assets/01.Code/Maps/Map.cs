using System;
using Code.CameraSetting;
using Code.Players;
using UnityEngine;

namespace Code.Maps
{
    public class Map : MonoBehaviour
    {
        public Player player; 
        public event Action OnSceneLoaded;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                CameraRotation.IsUI = true;
                OnSceneLoaded?.Invoke();
            }
        }
    }
}