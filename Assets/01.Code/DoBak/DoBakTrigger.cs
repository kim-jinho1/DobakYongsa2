using UnityEngine;
using UnityEngine.Events;

public class DoBakTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent onPlayerEnter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onPlayerEnter.Invoke();
        }
    }
}