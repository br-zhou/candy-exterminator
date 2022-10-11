using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gummy : MonoBehaviour
{
    float distanceToPlayer;
    private GameObject player;
    public bool Awaken;
    [SerializeField] private Transform m_GroundCheck;
    const float checkRadius = .2f;
    private Rigidbody2D rb;
    [SerializeField] private LayerMask m_WhatIsGround;
    private Vector3 m_Velocity = Vector3.zero;
    private float m_MovementSmoothing = .05f;
    public float runSpeed = 7f;
    [SerializeField] private float attackRange = 9;
    [SerializeField] private float desiredRange = 6;
    public bool isGrounded;
    private float move;
    private float prevx;
    bool touchingPlayer = false;
    bool wasGrounded = false;
    private GameObject alert;
    public bool canFire = false;
    public bool approaching = true;
    public bool canMove = true;
    float mass;
    activation a;
    audio_manager am;
    void Start()
    {
        am = GetComponent<audio_manager>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        alert = gameObject.transform.Find("alert").gameObject;
        a = GetComponent<activation>();
        mass = rb.mass;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Awaken = a.Awaken;
        if (!Awaken) return; //sleep

        //awake loop
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        isGrounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, checkRadius, m_WhatIsGround);
        foreach (Collider2D x in colliders)
        {
            if (x.gameObject != gameObject)
            {
                isGrounded = true;
            }
        }
        if (distanceToPlayer > attackRange && !approaching)
        {
            canFire = false;
            approaching = true;
            moveTowardsPlayer();
        }
        else if (approaching)
        {
            if(distanceToPlayer > desiredRange)
			{
                moveTowardsPlayer();
			}
			else
			{
                approaching = false;
			}
            
        }
        else
        {
            canFire = true;
        }



        //PUT AT END
        prevx = transform.position.x;
        wasGrounded = isGrounded;
    }

    void moveTowardsPlayer()
	{
		if (canMove)
		{
            move = Mathf.Clamp(player.transform.position.x - transform.position.x, -1f, 1f);
            Vector3 targetVelocity = new Vector2(move * runSpeed, rb.velocity.y);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
            if (Mathf.Round(transform.position.x * 100) == Mathf.Round(prevx * 100) && !touchingPlayer && isGrounded && wasGrounded) //if stuck against wall
            {
                Jump();
            }
        } 
        
    }
    public void Death()
    {
        score.Popup(300, transform.position);
        Destroy(gameObject);
    }

    public void Alert()  
    {
        StartCoroutine(AlertIE());
        rb.AddForce(new Vector2(0f, 300f* mass));
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(new Vector2(0f, 1000f* mass));
            am.Play("jump");
            //animator.SetBool("inAir", true);
        }
    }

    public void Flip(string dir)
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
    public void Recoil()
    {
        //recoil??
    }

    IEnumerator AlertIE()
    {
        alert.SetActive(true);

        yield return new WaitForSeconds(.3f);

        alert.SetActive(false);
    }
}
