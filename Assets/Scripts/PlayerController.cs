using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private InputController inputController;
    private CharacterController characterController;

    [Header("Speed control")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float turnSpeed = 10f;

    [Header("Camera")]
    [SerializeField] private Transform cameraTransform;

    [Header("Gravity")]
    [SerializeField] private float gravity = -9.81f;
    [Header("Jump height")]
    [SerializeField] private float jumpHeight = 2f;

    [Header("Dash configs")]
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashCooldown = 0.5f;
    [SerializeField] private float dashDuration = 0.15f;


    private float verticalVelocity;
    private float dashTimer;
    private float nextDashTime;
    private bool isDashing;
    private Vector2 moveInput;
    private Vector3 dashDirection;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        inputController = GetComponent<InputController>();
    }
    void Update()
    {
        moveInput = inputController.Controls.Player.Move.ReadValue<Vector2>();

        HandleDashInput();
        HandleGravity();
        HandleJump();
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (isDashing)
        {
            HandleDash();
            return;
        }

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
        if (inputController.Controls.Player.Jump.triggered && characterController.isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    private void HandleDash()
    {
        dashTimer -= Time.deltaTime;
        characterController.Move(dashDirection * Time.deltaTime * dashSpeed);

        if (dashTimer <= 0)
        {
            isDashing = false;
        }
    }

    private void HandleDashInput()
    {
        if (inputController.Controls.Player.Dash.triggered && Time.time >= nextDashTime)
        {
            isDashing = true;
            dashTimer = dashDuration;
            nextDashTime = Time.time + dashCooldown;

            Vector3 input = transform.right * moveInput.x + transform.forward * moveInput.y;
            dashDirection = input.sqrMagnitude > 0.01f ? input.normalized : transform.forward;
        }
    }

}
