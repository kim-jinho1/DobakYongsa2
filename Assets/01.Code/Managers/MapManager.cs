using UnityEngine;
using Code.Maps;

namespace Code.Managers
{
    public class MapManager : MonoBehaviour
    {
        [SerializeField] private Map map;
        [SerializeField] private GameObject mapUI;
        
        private void Awake()
        {
            map.OnSceneLoaded += HandleMap;
        }
        
        private void OnDestroy()
        {
            map.OnSceneLoaded -= HandleMap;
        }

        private void HandleMap()
        {
            mapUI.SetActive(true);
        }
    }
}