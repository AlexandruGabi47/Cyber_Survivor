using System;
using UnityEngine;
using UnityEngine.InputSystem;
using CAUnityFramework;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class TopDown2DCharacterController : MonoBehaviour
{
	[Header("Movement")]
	[SerializeField] private float baseSpeed = 5f;
	[SerializeField] private float sprintSpeedMultiplier = 2f;
	[SerializeField] private float crouchSpeedMultiplier = 0.5f;

	[Header("Dashing")]
	[SerializeField] private float dashSpeed = 20f;
	[SerializeField] private float dashDuration = 0.2f;
	[SerializeField] private float dashCooldown = 1f;
	
	private bool isDashing = false;
	private float dashTimer = 0f;
	private float dashCooldownTimer = 0f;

	private Vector2 dashDirection;

	private Vector2 moveInput;
	private Rigidbody2D rb;

	[Header("Controls")]
	[SerializeField] private InputActionReference moveAction;
	[SerializeField] private InputActionReference sprintAction;
	[SerializeField] private InputActionReference crouchAction;
	[SerializeField] private InputActionReference dashAction;

	private Vector2 lastMoveInput;

	private Animator animator;
	private const string movingParam = "isMoving";
	private const string runningParam = "isRunning";
	private const string crouchingParam = "isCrouching";
	private const string dashingParam = "isDashing";

	public UnityEvent<float> OnDashUpdated;
	public UnityEvent<MovementStatus> OnMovementStatusUpdate;

	public float DashCooldown => this.dashCooldown;

	void Awake()
	{
		this.rb = this.GetComponent<Rigidbody2D>();
		this.animator = this.GetComponent<Animator>();
	}

	private void OnEnable()
	{
		// Move
		this.moveAction.action.performed += this.UpdateMovementVector;
		this.moveAction.action.canceled += this.UpdateMovementVector;
		this.moveAction.action.Enable();

		this.sprintAction.action.Enable();
		this.crouchAction.action.Enable();

		this.dashAction.action.performed += this.OnDash;
		this.dashAction.action.Enable();
	}

	private void OnDisable()
	{
		// Move
		this.moveAction.action.performed -= this.UpdateMovementVector;
		this.moveAction.action.canceled -= this.UpdateMovementVector;
		this.moveAction.action.Disable();
		
		this.sprintAction.action.Disable();
		this.crouchAction.action.Disable();

		this.dashAction.action.performed -= this.OnDash;
		this.dashAction.action.Disable();
	}

	void FixedUpdate()
	{
		this.UpdateDashCooldownTimer(Time.fixedDeltaTime);

		this.ClearAnimatorFlags();

		this.UpdateCharacterMovement(Time.fixedDeltaTime);
	}

	private void ClearAnimatorFlags()
	{
		this.animator.SetBool(movingParam, false);
		this.animator.SetBool(runningParam, false);
		this.animator.SetBool(crouchingParam, false);
	}

	private void UpdateCharacterMovement(float deltaTime)
	{
		if (this.isDashing)
		{
			this.UpdateDashMovement(deltaTime);

			if (this.IsDashComplete())
			{
				this.StopDashing();
			}
			this.OnMovementStatusUpdate?.Invoke(MovementStatus.Dashing);
		}
		else if (this.moveInput.sqrMagnitude > 0.01f)
		{
			float speedMultiplier = 1f;
			this.OnMovementStatusUpdate?.Invoke(MovementStatus.Walking);

			if (this.sprintAction.action.IsPressed())
			{
				speedMultiplier = this.sprintSpeedMultiplier;
				this.OnMovementStatusUpdate?.Invoke(MovementStatus.Running);
				this.animator.SetBool(runningParam, true);
			}
			else if (this.crouchAction.action.IsPressed())
			{
				speedMultiplier = this.crouchSpeedMultiplier;
				this.OnMovementStatusUpdate?.Invoke(MovementStatus.Crouching);
				this.animator.SetBool(crouchingParam, true);
			}

			Vector2 movement = (this.baseSpeed * speedMultiplier) * deltaTime * this.moveInput;
			this.rb.MovePosition(this.rb.position + movement);
			this.animator.SetBool(movingParam, true);

			this.lastMoveInput = this.moveInput;
		}
		else
		{
			this.OnMovementStatusUpdate?.Invoke(MovementStatus.Idle);
		}
	}

	private void UpdateDashCooldownTimer(float deltaTime)
	{
		if (this.dashCooldownTimer > 0f)
		{
			this.dashCooldownTimer -= deltaTime;
			this.OnDashUpdated?.Invoke(this.dashCooldownTimer);
		}

	}

	private void UpdateDashMovement(float deltaTime)
	{
		this.dashTimer -= deltaTime;
		this.rb.MovePosition(
			this.rb.position + this.dashSpeed * deltaTime * this.dashDirection);
	}

	private Boolean IsDashComplete()
	{
		return this.dashTimer <= 0f;
	}

	private void StopDashing()
	{
		this.isDashing = false;
		this.rb.linearVelocity = Vector2.zero;
	}

	private void UpdateMovementVector(InputAction.CallbackContext ctx)
	{
		this.moveInput = ctx.ReadValue<Vector2>();
	}

	private void OnDash(InputAction.CallbackContext ctx)
	{
		if (this.isDashing || this.dashCooldownTimer > 0f || this.moveInput == Vector2.zero)
			return;

		this.animator.SetBool(movingParam, false);
		this.isDashing = true;
		this.dashTimer = this.dashDuration;
		this.dashCooldownTimer = this.dashCooldown;
		this.dashDirection = this.moveInput.normalized;
	}
}
