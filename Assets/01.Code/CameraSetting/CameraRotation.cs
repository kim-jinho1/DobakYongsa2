using UnityEngine;

namespace Code.CameraSetting
{
    public class CameraRotation : MonoBehaviour
    {
        [Header("Follow Settings")]
        [Tooltip("Camera will follow this object (usually the player)")]
        [SerializeField] private Transform objectToFollow;
        [SerializeField] private Vector3 cameraOffset = new Vector3(0f, 1.4f, 0f);
        [SerializeField] private float followSpeed = 10f;

        [Header("Rotation Settings")]
        [SerializeField] private float sensitivity = 100f;
        [SerializeField] private float topClamp = 70f;
        [SerializeField] private float bottomClamp = -30f;
        [SerializeField] private float rotationLerpSpeed = 10f;

        [Header("Collision Settings")]
        [SerializeField] private Transform realCamera;
        [SerializeField] private LayerMask collisionLayers;
        [SerializeField] private float minDistance = 1f;
        [SerializeField] private float maxDistance = 5f;
        [SerializeField] private float smoothness = 10f;

        private float _yaw;
        private float _pitch;
        private Vector3 _defaultCameraDirection;

        private void Start()
        {
            Vector3 cameraLocalPosition = realCamera.localPosition + cameraOffset;
            _defaultCameraDirection = cameraLocalPosition.normalized;
        }

        private void Update()
        {
            _yaw += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            _pitch += -Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
            _pitch = Mathf.Clamp(_pitch, bottomClamp, topClamp);

            Quaternion targetRotation = Quaternion.Euler(_pitch, _yaw, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationLerpSpeed);
        }

        private void LateUpdate()
        {
            if (objectToFollow == null || realCamera == null) return;
            
            Vector3 followTarget = objectToFollow.position + cameraOffset;
            transform.position = Vector3.Lerp(transform.position, followTarget, followSpeed * Time.deltaTime);
            
            Vector3 desiredCameraPosWorld = transform.TransformPoint(_defaultCameraDirection * maxDistance);
            float targetDistance = maxDistance;

            if (Physics.Linecast(transform.position, desiredCameraPosWorld, out RaycastHit hit, collisionLayers))
            {
                targetDistance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
            }

            Vector3 finalCameraLocalPos = _defaultCameraDirection * targetDistance;
            realCamera.localPosition = Vector3.Lerp(realCamera.localPosition, finalCameraLocalPos, Time.deltaTime * smoothness);
        }
    }
}
