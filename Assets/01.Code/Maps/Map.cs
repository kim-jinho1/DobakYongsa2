using System;
using Code.CameraSetting;
using Code.Players;
using UnityEngine;

namespace Code.Maps
{
    public class Map : MonoBehaviour
    {
        public Player player; 
        public CameraRotation cameraRotation;
        public event Action OnSceneLoaded;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                OnSceneLoaded?.Invoke();
            }
        }
    }
}