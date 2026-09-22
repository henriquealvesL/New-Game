using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject focalPoint;
    [SerializeField] private InputController inputController;

    private float yaw;
    private float pitch;
    private float mouseSensitivity = 0.5f;

    private Vector3 offset;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        offset = transform.position - focalPoint.transform.position;

        Vector3 angles = transform.eulerAngles;

        yaw = angles.y;
        pitch = angles.x;
    }

    void LateUpdate()
    {
        RotateCamera();
    }

    private void RotateCamera()
    {
        Vector2 look = inputController.Controls.Player.Look.ReadValue<Vector2>();

        yaw += look.x * mouseSensitivity;
        pitch -= look.y * mouseSensitivity;

        pitch = Mathf.Clamp(pitch, -80f, 80f);

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);

        transform.position = focalPoint.gameObject.transform.position + rotation * offset;
        transform.rotation = rotation;
    }

}
