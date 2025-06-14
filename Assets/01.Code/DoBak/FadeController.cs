using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeTime = 1f;

    public IEnumerator FadeAndLoadScene(string sceneName)
    {
        fadeImage.gameObject.SetActive(true);
        yield return StartCoroutine(FadeOut());
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator FadeOut()
    {
        Color color = fadeImage.color;
        float time = 0;

        while (time < fadeTime)
        {
            time += Time.deltaTime;
            color.a = Mathf.Clamp01(time / fadeTime);
            fadeImage.color = color;
            yield return null;
        }
    }
}