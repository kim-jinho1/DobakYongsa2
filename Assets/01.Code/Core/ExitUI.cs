using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitUI : MonoBehaviour
{
    [SerializeField] private GameObject exitUI;
    [SerializeField] private string sceneName;
    
    public void Exit()
    {
        exitUI.SetActive(false);
    }

    public void NextScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            exitUI.SetActive(true);
        }
    }
}
