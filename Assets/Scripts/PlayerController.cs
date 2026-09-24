using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private InputController inputController;
    private CharacterController characterController;
    private Animator animator;
    private CombatController combatController;

    [Header("Speed control")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 12f;

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
        combatController = GetComponent<CombatController>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        moveInput = inputController.Controls.Player.Move.ReadValue<Vector2>();
        animator.SetFloat("Speed", Mathf.Clamp01(moveInput.magnitude), 0.1f, Time.deltaTime);

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

        Vector3 horizontal = GetCameraRelativeDirection() * moveSpeed;
        Vector3 velocity = horizontal + Vector3.up * verticalVelocity;

        if (moveInput.sqrMagnitude > 0.01f)
        {
            Quaternion target = Quaternion.LookRotation(horizontal.normalized, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, rotationSpeed * Time.deltaTime);
        }

        characterController.Move(velocity * Time.deltaTime);
    }

    private void HandleGravity()
    {
        if (isDashing) return;

        if (characterController.isGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = -2f;
        }

        verticalVelocity += gravity * Time.deltaTime;
    }

    private void HandleJump()
    {
        if (isDashing) return;

        if (inputController.Controls.Player.Jump.triggered && characterController.isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    private void HandleDash()
    {
        dashTimer -= Time.deltaTime;

        Vector3 velocity = dashDirection * dashSpeed + Vector3.up * verticalVelocity;
        characterController.Move(velocity * Time.deltaTime);

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

            dashDirection = moveInput.sqrMagnitude > 0.01f ? GetCameraRelativeDirection() : transform.forward;
        }
    }

    private Vector3 GetCameraRelativeDirection()
    {
        Vector3 cameraForward = new Vector3(cameraTransform.forward.x, 0f, cameraTransform.forward.z).normalized;
        Vector3 cameraRight = new Vector3(cameraTransform.right.x, 0f, cameraTransform.right.z).normalized;

        return (cameraRight * moveInput.x + cameraForward * moveInput.y).normalized;
    }

}
