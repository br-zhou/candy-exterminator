using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_input : MonoBehaviour {

	public player_controller controller;
	public Animator animator;
	public static float speed;
	float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;
	Rigidbody2D rb;


	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}
	// Update is called once per frame
	void Update () {

		if (!ms.Paused)
		{
			horizontalMove = Input.GetAxisRaw("Horizontal");
			animator.SetFloat("Speed", Mathf.Abs(horizontalMove * rb.velocity.x));

			if (Input.GetButton("Jump"))
			{
				jump = true;
				animator.SetBool("IsJumping", true);
			}
			else
			{
				jump = false;
			}


			if (Input.GetButton("Crouch"))
			{
				crouch = true;
			}
			else
			{
				crouch = false;
			}

		}

		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
	}

	public void OnLanding()
	{
		 animator.SetBool("IsJumping", false);
	}

	public void OnCrouching(bool isCrouching)
	{
		animator.SetBool("IsCrouching", isCrouching);
	}

}