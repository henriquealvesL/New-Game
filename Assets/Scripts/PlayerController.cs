using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private InputSystem_Actions controls;
    private CharacterController characterController;

    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private float turnSpeed = 10f;
    [SerializeField]
    private Transform cameraTransform;

    [SerializeField]
    private float gravity = -9.81f;

    [SerializeField]
    private float jumpHeight = 2f;

    private float verticalVelocity;
    private Vector2 moveInput;

    void Awake()
    {
        controls = new InputSystem_Actions();
        characterController = GetComponent<CharacterController>();

        controls.Player.Enable();
    }
    void Update()
    {
        moveInput = controls.Player.Move.ReadValue<Vector2>();
        HandleGravity();
        HandleJump();
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector3 cameraForward = new Vector3(cameraTransform.forward.x, 0f, cameraTransform.forward.z).normalized;
        Vector3 cameraRight = new Vector3(cameraTransform.right.x, 0f, cameraTransform.right.z).normalized;

        Vector3 movement = (cameraRight * moveInput.x + cameraForward * moveInput.y).normalized;
        movement.y = verticalVelocity;

        float cameraYaw = cameraTransform.eulerAngles.y;

        Quaternion targetRotation = Quaternion.Euler(0f, cameraYaw, 0f);

        transform.rotation = targetRotation;

        characterController.Move(movement * moveSpeed * Time.deltaTime);
    }

    private void HandleGravity()
    {
        if (characterController.isGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = -2f;
        }

        verticalVelocity += gravity * Time.deltaTime;
    }

    private void HandleJump()
    {
        if (controls.Player.Jump.triggered && characterController.isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

}
