using System;
using UnityEngine;

namespace Code.Maps
{
    public class Map : MonoBehaviour
    {
        public event Action OnSceneLoaded;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("aaa");
                OnSceneLoaded?.Invoke();
            }
        }
    }
}