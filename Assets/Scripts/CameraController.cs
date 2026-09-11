using UnityEngine;

public class CameraController : MonoBehaviour
{
    InputSystem_Actions controls;

    [SerializeField]
    private GameObject focalPoint;

    private float yaw;
    private float pitch;
    private float mouseSensitivity = 0.5f;

    private Vector3 offset;

    void Awake()
    {
        controls = new InputSystem_Actions();
        controls.Player.Look.Enable();

        offset = transform.position - focalPoint.transform.position;

        Vector3 angles = transform.eulerAngles;

        yaw = angles.x;
        pitch = angles.y;
    }

    void LateUpdate()
    {
        RotateCamera();
    }

    private void RotateCamera()
    {
        Vector2 look = controls.Player.Look.ReadValue<Vector2>();

        yaw += look.x * mouseSensitivity;
        pitch -= look.y * mouseSensitivity;

        pitch = Mathf.Clamp(pitch, -80f, 80f);

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);

        transform.position = focalPoint.gameObject.transform.position + rotation * offset;
        transform.rotation = rotation;
    }

}
