using UnityEngine;

namespace Code.CameraSetting
{
    public class CameraRotation : MonoBehaviour
    {
        [SerializeField] private Transform objectToFollow;
        [SerializeField] private float followSpeed = 10f;
        [SerializeField] private float sensitivity = 100f;
        [SerializeField] private float clampAngle = 70f;

        private float _rotX;
        private float _rotY;

        [SerializeField] private Transform realCamera;
        [SerializeField] private Vector3 dirNormalized;
        [SerializeField] private Vector3 finalDir;
        [SerializeField] private float minDistance;
        [SerializeField] private float maxDistance;
        [SerializeField] private float finalDistance;
        [SerializeField] private float smoothness = 10f;

        [SerializeField] private Vector3 cameraOffset = new Vector3(0.3f, 0, 0);

        private void Start()
        {
            _rotX = transform.localRotation.eulerAngles.x;
            _rotY = transform.localRotation.eulerAngles.y;

            dirNormalized = (realCamera.localPosition + cameraOffset).normalized;
            finalDistance = realCamera.localPosition.magnitude;
        }

        private void Update()
        {
            _rotX += -(Input.GetAxis("Mouse Y")) * sensitivity * Time.deltaTime;
            _rotY += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

            _rotX = Mathf.Clamp(_rotX, -clampAngle, clampAngle);
            Quaternion rotation = Quaternion.Euler(_rotX, _rotY, 0);
            transform.rotation = rotation;
        }

        private void LateUpdate()
        {
            transform.position = Vector3.MoveTowards(transform.position, objectToFollow.position, followSpeed * Time.deltaTime);
        
            finalDir = transform.TransformPoint((dirNormalized * maxDistance) + cameraOffset);

            finalDistance = Physics.Linecast(transform.position, finalDir, out var hit) ? Mathf.Clamp(hit.distance, minDistance, maxDistance) : maxDistance;

            Vector3 targetPos = dirNormalized * finalDistance + cameraOffset;
            realCamera.localPosition = Vector3.Lerp(realCamera.localPosition, targetPos, Time.deltaTime * smoothness);
        }
    }
}
