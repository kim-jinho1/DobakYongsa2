using Code.CameraSetting;
using UnityEngine;

public class EnterTrigger : MonoBehaviour
{
    [SerializeField] private GameObject uiPanel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CameraRotation.IsUI = true;
            uiPanel.SetActive(true);
        }
    }
}