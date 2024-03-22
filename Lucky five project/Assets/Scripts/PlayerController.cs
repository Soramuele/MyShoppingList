using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
	#region Variables: Movement

	private Vector2 _input;
	private CharacterController _characterController;
	private Vector3 _direction;

	[SerializeField] private float speed;
    [SerializeField] private float sprintModifier = 5f;
	private float playerVelocity;

	#endregion
	#region Variables: Rotation

	[SerializeField] private float rotationSpeed = 500f;
	private Camera _mainCamera;

	#endregion
	#region Variables: Gravity

	private float _gravity = -9.81f;
	[SerializeField] private float gravityMultiplier = 3.0f;
	private float _velocity;

	#endregion
	#region Variables: Jumping

	[SerializeField] private float jumpPower;
	private int _numberOfJumps;
	[SerializeField] private int maxNumberOfJumps = 2;

	#endregion
	//private Animator animator;
	[SerializeField] private GameObject shoppingList;
	private bool toggle = false;

	private void Awake()
	{
		_characterController = GetComponent<CharacterController>();
		//animator = GetComponentInChildren<Animator>();
		_mainCamera = Camera.main;
		playerVelocity = speed;

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	private void Update()
	{
		ApplyRotation();
		ApplyGravity();
		ApplyMovement();

		if(Input.GetKey(KeyCode.Escape) && Cursor.visible == false)
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		} else {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}

	private void ApplyGravity()
	{
		if (IsGrounded() && _velocity < 0.0f)
		{
			_velocity = -1.0f;
		}
		else
		{
			_velocity += _gravity * gravityMultiplier * Time.deltaTime;
		}
		
		_direction.y = _velocity;
	}
	
	private void ApplyRotation()
	{
		if (_input.sqrMagnitude == 0) return;

		_direction = Quaternion.Euler(0.0f, _mainCamera.transform.eulerAngles.y, 0.0f) * new Vector3(_input.x, 0.0f, _input.y);
		var targetRotation = Quaternion.LookRotation(_direction, Vector3.up);

		transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
	}

	private void ApplyMovement()
	{
		_characterController.Move(_direction * playerVelocity * Time.deltaTime);
	}

	public void Move(InputAction.CallbackContext context)
	{
		_input = context.ReadValue<Vector2>();
		//if(_input != Vector2.zero)	animator.SetBool("isWalking", true);
		//	else	animator.SetBool("isWalking", false);
		_direction = new Vector3(_input.x, 0.0f, _input.y);
	}

	public void Jump(InputAction.CallbackContext context)
	{
		if (!context.started) return;
		if (!IsGrounded() && _numberOfJumps >= maxNumberOfJumps) return;
		if (_numberOfJumps == 0) StartCoroutine(WaitForLanding());
		
		_numberOfJumps++;
		_velocity = jumpPower;
	}

	public void Sprint(InputAction.CallbackContext context)
	{
        if (context.started || context.performed) {
			playerVelocity = speed + sprintModifier;
			//animator.SetBool("isRunning", true);
		} else if (context.canceled) {
			playerVelocity = speed;
			//animator.SetBool("isRunning", false);
		}
	}

	public void UseList(InputAction.CallbackContext context)
	{
		if (context.started) {
			Debug.Log(toggle);
			toggle = !toggle;
			shoppingList.SetActive(toggle);
		}
	}

	private IEnumerator WaitForLanding()
	{
		yield return new WaitUntil(() => !IsGrounded());
		yield return new WaitUntil(IsGrounded);

		_numberOfJumps = 0;
	}

	private bool IsGrounded() => _characterController.isGrounded;
}