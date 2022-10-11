using UnityEngine;
using UnityEngine.Events;

public class player_controller : MonoBehaviour
{
	const float k_GroundedRadius = .2f;
	const float k_CeilingRadius = .2f;

	const bool airStrafe = true;
	const float jumpVelocity = 17f;                         
	const float runSpeed = 600f;

	[Range(0, 1)] [SerializeField] private float crouchSpeedMultiplier = .36f;        
	[Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;  
	                   
	[SerializeField] private LayerMask m_WhatIsGround;                      
	[SerializeField] private Transform m_GroundCheck;                  
	[SerializeField] private Transform m_CeilingCheck;                
	[SerializeField] private Collider2D disabledCrouchCollider;       
	[SerializeField] private Collider2D disabledCrouchTrigger;

	private bool isGrounded;
	
	private Rigidbody2D rb;
	private Vector3 m_Velocity = Vector3.zero;
	bool forcedCrouch = false;

	[Header("Events")]
	[Space]
	audio_manager am;
	public UnityEvent OnLandEvent;

	float jumpTimerMax = .1f;
	float jumpTimer = 0f;

	float groundedTimerMax = .2f;
	float groundedTimer = 0;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool wasCrouching = false;


	int extraJumpsMax = 1;
	int extraJumpsLeft;

	camera_script cs;

	private void Start()
	{
		am = GetComponent<audio_manager>();
		rb = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
		{
			OnLandEvent = new UnityEvent();
		}


		if (OnCrouchEvent == null)
		{
			OnCrouchEvent = new BoolEvent();
		}

		cs = camera_script.cs;
	}

	private void Update()
	{
		jumpTimer -= Time.deltaTime;
		groundedTimer -= Time.deltaTime;

		forcedCrouch = false;
		isGrounded = IsGroundedCheck();

		player_master.velocity = rb.velocity.x;
	}

	public void Move(float moveInput, bool crouchInput, bool jumpInput)
	{
		if (player_master.dead) {
			rb.velocity = Vector3.zero;
			return;
		} 

		if (!player_master.canMove) return;
		// If crouching, check to see if the character can stand up

		forcedCrouch = false;
		if (!crouchInput)
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (ForcedCrouchCheck())
			{
				crouchInput = true;
				forcedCrouch = true;
			}
		}

		//only control the player if grounded or airControl is turned on
		if (isGrounded || airStrafe)
		{

			// If crouching
			if (crouchInput && isGrounded)
			{
				if (!wasCrouching)
				{
					wasCrouching = true;
					OnCrouchEvent.Invoke(true);
					player_master.isCrouching = true;

				}

				// Reduce the speed by the crouchSpeed multiplier
				moveInput *= crouchSpeedMultiplier;

				// Disable one of the colliders when crouching
				if (disabledCrouchCollider != null)
					disabledCrouchCollider.enabled = false;
				if (disabledCrouchTrigger != null)
					disabledCrouchTrigger.enabled = false;

			}
			else
			{
				// Enable the collider when not crouching
				if (disabledCrouchCollider != null)
					disabledCrouchCollider.enabled = true;
				if (disabledCrouchTrigger != null)
					disabledCrouchTrigger.enabled = true;


				if (wasCrouching)
				{
					wasCrouching = false;
					OnCrouchEvent.Invoke(false);
					//*Arm.SetActive(true);
					//*player_master.canShoot = true;
					player_master.isCrouching = false;
				}
			}

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(moveInput * runSpeed, rb.velocity.y);
			// And then smoothing it out and applying it to the character
			rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref m_Velocity, movementSmoothing);
		}


		//resets jump and groundedTimers
		if(jumpInput)
		{
			jumpTimer = jumpTimerMax;
		}

		if(isGrounded)
		{
			groundedTimer = groundedTimerMax;
		}

		// If the player should jump...
		if (groundedTimer > 0 && jumpTimer > 0 && !forcedCrouch)
		{
			jumpTimer = 0;
			groundedTimer = 0;
			am.Play("jump");
			isGrounded = false;
			rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);

		}

		if (extraJumpsLeft > 0 && Input.GetButtonDown("Jump") && !isGrounded)
		{
			if (!isGrounded)
			{
				extraJumpsLeft--;
			}
			am.Play("jump");

			rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);

		}
		player_master.isJumping = !isGrounded;
		cs.UpdateVCAM(moveInput);
	}


	public void FlipSprite(string dir)
	{
		// Switch the way the player is labelled as facing.
		switch (dir)
		{
			case "left":
				transform.rotation = Quaternion.Euler(0f, 180f, 0f);
				break;
			case "right":
				transform.rotation = Quaternion.Euler(0f, 0f, 0f);
				break;
			default:
				break;
		}
	}


	bool IsGroundedCheck()
	{
		bool isGrounded = false;
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);

		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				isGrounded = true;
				extraJumpsLeft = extraJumpsMax;
				OnLandEvent.Invoke();
			}
		}

		return isGrounded;
	}

	bool ForcedCrouchCheck()
	{
		return Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround);
	}

}
