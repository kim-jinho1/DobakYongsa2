using Code.CameraSetting;
using Code.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterUI : MonoBehaviour
{
    [SerializeField] private DataManager dataManager;
    [SerializeField] private GameObject uiPanel;
    [SerializeField] private string sceneName = "House2";
    [SerializeField] private FadeController fadeController; // 페이드용 스크립트

    public void OnClickYes()
    {
        if (dataManager.Gold >= 10000)
        {
            StartCoroutine(fadeController.FadeAndLoadScene(sceneName));
        }
    }

    public void OnClickNo()
    {
        uiPanel.SetActive(false);
        CameraRotation.IsUI = false;
    }
}