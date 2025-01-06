using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code based on a tutorial posted by Dan Pos. Youtube Video - https://www.youtube.com/watch?v=sPBhDcuBuIA&list=PL-hj540P5Q1hLK7NS5fTSNYoNJpPWSL24&pp=iAQB


[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
	private CharacterController controller;
	private Vector3 playerVelocity;
	private bool groundedPlayer;
	[SerializeField]
	private float playerSpeed = 2.0f;
	private float gravityValue = -9.81f;

	[SerializeField] private Animator animator;

	private PlayerControls playerControls;

	private void Awake()
	{
		controller = gameObject.GetComponent<CharacterController>();
		playerControls = new PlayerControls();
	}

	 void OnEnable()
	{
		playerControls?.Enable();
	}

	private void OnDisable()
	{
		playerControls?.Disable();
	}

	void Update()
	{
		groundedPlayer = controller.isGrounded;
		if (groundedPlayer && playerVelocity.y < 0)
		{
			playerVelocity.y = 0f;
		}

		Vector3 move = new Vector3(playerControls.Player.Movement.ReadValue<Vector2>().x, 0, playerControls.Player.Movement.ReadValue<Vector2>().y);
		controller.Move(move * Time.deltaTime * playerSpeed);

		if (move != Vector3.zero)
		{
			gameObject.transform.forward = move;
			animator.SetBool("isWalking", true);
		}
		else
		{
			animator.SetBool("isWalking", false);
		}

		playerVelocity.y += gravityValue * Time.deltaTime;
		controller.Move(playerVelocity * Time.deltaTime);
	}
}
