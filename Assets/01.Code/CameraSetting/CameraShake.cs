using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float shakeDuration = 0.3f;
    [SerializeField] private float shakeMagnitude = 0.3f;
    [SerializeField] private float dampingSpeed = 1.0f;

    private float currentShakeTime;
    private Vector3 originalLocalPos;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if (cameraTransform == null)
        {
            Debug.LogError("CameraTransform is not assigned to CameraShake.");
        }

        originalLocalPos = cameraTransform.localPosition;
    }

    private void OnEnable()
    {
        originalLocalPos = cameraTransform.localPosition;
    }

    private void Update()
    {
        if (currentShakeTime > 0)
        {
            cameraTransform.localPosition = originalLocalPos + Random.insideUnitSphere * shakeMagnitude;
            currentShakeTime -= Time.unscaledDeltaTime * dampingSpeed;
        }
        else if (cameraTransform.localPosition != originalLocalPos)
        {
            cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, originalLocalPos, Time.unscaledDeltaTime * dampingSpeed);
            
            if (Vector3.Distance(cameraTransform.localPosition, originalLocalPos) < 0.001f)
            {
                cameraTransform.localPosition = originalLocalPos;
            }
        }
    }

    public void Shake(float duration, float magnitude)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
        currentShakeTime = shakeDuration;
    }
}