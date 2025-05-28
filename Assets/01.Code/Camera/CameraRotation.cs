using UnityEngine;

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

    private void Start()
    {
        _rotX = transform.localRotation.eulerAngles.x;
        _rotY = transform.localRotation.eulerAngles.y;
        
        dirNormalized = realCamera.localPosition.normalized;
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
        
        finalDir = transform.TransformPoint(dirNormalized * maxDistance);
        
        RaycastHit hit;

        if (Physics.Linecast(transform.position, finalDir, out hit))
        {
            finalDistance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
        }
        else
        {
            finalDistance = maxDistance;
        }
        realCamera.localPosition = Vector3.Lerp(realCamera.localPosition, dirNormalized * finalDistance,Time.deltaTime * smoothness);
    }
}
