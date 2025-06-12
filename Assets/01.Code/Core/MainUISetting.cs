using Code.CameraSetting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class SceneFadeManager : MonoBehaviour
{
    public static SceneFadeManager Instance;
    [SerializeField] private Image fadeImage;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 씬 로드 후 페이드 인
        fadeImage.gameObject.SetActive(true);
        fadeImage.color = new Color(0, 0, 0, 1);
        fadeImage.DOFade(0f, 1f).OnComplete(() =>
        {
            fadeImage.gameObject.SetActive(false);
        });
    }

    public void Exit(GameObject game)
    {
        game.SetActive(false);
        CameraRotation.IsUI = false;
    }

    public void FadeOutAndLoadScene(string sceneName)
    {
        fadeImage.gameObject.SetActive(true);
        fadeImage.color = new Color(0, 0, 0, 0);
        fadeImage.DOFade(1f, 1f).OnComplete(() =>
        {
            SceneManager.LoadScene(sceneName);
        });
    }

    public void GameQuit()
    {
        Application.Quit();
    }
}