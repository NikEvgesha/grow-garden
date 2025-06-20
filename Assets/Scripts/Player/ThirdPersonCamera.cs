
// ThirdPersonCamera.cs
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("References")]
    public Transform target;
    public Vector3 offset = new Vector3(0f, 2f, -4f);
    public float smoothSpeed = 0.1f;
    public float mouseSensitivity = 2f;

    private float yaw = 0f;
    private float pitch = 10f;
    private bool isRotating = false;

    private void Start()
    {
        Cursor.lockState = isRotating ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !isRotating;
    }
    void Update()
    {
        // Toggle camera rotation on Tab
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isRotating = !isRotating;
            Cursor.lockState = isRotating ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !isRotating;
        }

        if (Input.GetMouseButtonDown(1) && !isRotating)
        {
            isRotating = true;
        }

        if (Input.GetMouseButtonUp(1) && isRotating)
        {
            isRotating = false;
        }


        // Mouse rotation
        if (isRotating)
        {
            yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
            pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
            pitch = Mathf.Clamp(pitch, -20f, 80f);
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Calculate desired position
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 desiredPosition = target.position + rotation * offset;

        // Smooth transition
        Vector3 smoothedPosition = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed
        );
        transform.position = smoothedPosition;

        // Look at target
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}
