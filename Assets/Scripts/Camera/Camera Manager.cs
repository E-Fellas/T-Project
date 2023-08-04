using UnityEngine;

public class CameraManager : MonoBehaviour
{
    #region Variables

    [SerializeField] private Transform target;
    public Camera mainCamera;
    [SerializeField] public float minSize = 2f;
    [SerializeField] public float maxSize = 7f;
    [SerializeField] private float smoothTime;
    [SerializeField] private float sensitivity = 1.0f;

    private Vector3 _offset;
    private Vector3 _currentVelocity = Vector3.zero;

    #endregion
    private void Update()
    {
        // Get the value of the mouse scroll wheel movement
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        // Use the scroll value to change the size of the camera
        mainCamera.orthographicSize -= scroll * sensitivity;

        // Clamp the size of the camera between minSize and maxSize
        mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, minSize, maxSize);
    }
    private void Awake()
    {
        _offset = transform.position - target.position;
    }
    private void LateUpdate()
    {
        var targetPosition = target.position;
        //var targetPosition = target.position + _offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, smoothTime);
    }
}