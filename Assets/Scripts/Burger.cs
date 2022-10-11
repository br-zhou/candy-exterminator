using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burger : MonoBehaviour
{
    float distanceToPlayer;
    private GameObject player;

    bool Awaken = false;
    [SerializeField] private Transform m_GroundCheck;
    const float checkRadius = .2f;
    private Rigidbody2D rb;
    [SerializeField] private LayerMask m_WhatIsGround;
    private Vector3 m_Velocity = Vector3.zero;
    private float m_MovementSmoothing = .05f;
    public float runSpeed = 15f;
    public int attack_damage = 5;
    public float jumpForce = 1300f;
    private bool m_Grounded;
    private float move;
    private float prevx;
    bool touchingPlayer = false;
    bool wasGrounded = false;
    public Animator animator;
    [SerializeField] private GameObject explosion;
    private GameObject alert;
    activation a;
    audio_manager am;
    float attackCool = .5f; 
    void Start()
    {
        am = GetComponent<audio_manager>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        alert = gameObject.transform.Find("alert").gameObject;
        a = GetComponent<activation>();
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        attackCool -= Time.deltaTime;
        Awaken = a.Awaken;
        if (!Awaken) return; //sleep

        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        //awake loop
        m_Grounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, checkRadius, m_WhatIsGround);
        foreach (Collider2D x in colliders)
        {
            if (x.gameObject != gameObject)
            {
                m_Grounded = true;

                if (!wasGrounded)
                {
                    animator.SetBool("inAir", false);
                    //am.Play("land");                
                }
            }
        }
        move = Mathf.Clamp(player.transform.position.x - transform.position.x, -1f, 1f);
        //float distance = Mathf.Abs(player.transform.position.x - transform.position.x);
        Vector3 targetVelocity = new Vector2(move * runSpeed, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

        if (Mathf.Round(transform.position.x * 100) == Mathf.Round(prevx * 100) && !touchingPlayer && m_Grounded && wasGrounded)
        {
            Jump();
        }

        //PUT AT END
        prevx = transform.position.x;
        wasGrounded = m_Grounded;
    }

    public void Death()
    {
        if (explosion != null)
        {
            Destroy(Instantiate(explosion, transform.position, Quaternion.identity), .5f);
        }

        Destroy(gameObject);
    }

    public void Alert()
    {
        StartCoroutine(AlertIE());
        rb.AddForce(new Vector2(0f, 500f));
    }

    public void Jump()
    {
        if (m_Grounded)
        {
            am.Play("jump");
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(new Vector2(0f, jumpForce * rb.mass));
            animator.SetBool("inAir", true);
        }
    }


    public void Recoil()
    {
        //recoil??
    }

    private void OnTriggerStay2D(Collider2D Object)
    {
        if (player_master.dead) return;
        if (Object.gameObject.CompareTag("Player"))
        {
            if(attackCool <= 0)
			{
                Health hs = Object.GetComponent<Health>();
                hs.TakeDamage(attack_damage);
                am.Play("attack");
                Recoil();
                attackCool = 1f;
                Jump();
            }
            

        }
    }

    IEnumerator AlertIE()
    {
        alert.SetActive(true);

        yield return new WaitForSeconds(.3f);

        alert.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D Object)
    {
        if (Object.gameObject.CompareTag("Player"))
        {
            touchingPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D Object)
    {
        if (Object.gameObject.CompareTag("Player"))
        {
            touchingPlayer = false;
        }
    }
}
