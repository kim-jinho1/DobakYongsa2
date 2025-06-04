using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Maps
{
    public class MapUI : MonoBehaviour
    {
        [SerializeField] private GameObject mapUI;
        [SerializeField] private string nextMapName;
        
        private void Awake()
        {
            mapUI.SetActive(false);
        }

        public void ExitButton()
        {
            mapUI.SetActive(false);
        }

        public void InButton()
        {
            SceneManager.LoadScene(nextMapName);
        }
    }
}