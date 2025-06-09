using UnityEngine;
using UnityEngine.SceneManagement;

namespace _01.Code.Core
{
    public class MainUISetting : MonoBehaviour
    {
        [SerializeField] private GameObject menu;
        [SerializeField] private string sceneName1;
        [SerializeField] private string sceneName2;
        public void GameStart()
        {
            SceneManager.LoadScene(sceneName1);
        }

        public void ExitGame()
        {
            SceneManager.LoadScene(sceneName2);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void ContinueGame()
        {
            menu.SetActive(false);
        }
    }
}