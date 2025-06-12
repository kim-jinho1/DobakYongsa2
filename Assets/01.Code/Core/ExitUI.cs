using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitUI : MonoBehaviour
{
    [SerializeField] private GameObject exitUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            exitUI.SetActive(true);
        }
    }
}
