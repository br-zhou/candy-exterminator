using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class player_master : MonoBehaviour
{
    public static bool dead;
    public static bool invulnerable;
    //public static ms o;
    float RespawnShieldDelay = 3f;
    public static float maxSpeed = 12f;
    public static Vector2 respawnPos;
    private float OriginalHealth;
    private Color originalMaterial;
    Health h;
    public TextMeshProUGUI text;
    //public GameObject armsprite;
    //private SpriteRenderer spriteRenderer;
    public static bool isCrouching,isJumping;
    public static float velocity;
    private Rigidbody2D rb;
    [SerializeField] Collider2D physcoll1;
    [SerializeField] Collider2D physcoll2;
    invincibility invin;
    public GameObject ragdoll;
    public GameObject[] SGO;
    private SpriteRenderer[] SRGO;
    static GameObject player;
    static Rigidbody2D prb;
    public static bool canMove;
    public static player_master me;
    float TerminalVelocity = 25f;
    audio_manager am;
    int lives;
    void Start()
	{
        isCrouching = false;
        isJumping = false;
        lives = 3;
        am = GetComponent<audio_manager>();
        dead = false;
        invulnerable = false;
        canMove = true;
        player = GameObject.FindGameObjectWithTag("Player");
        me = this;
        prb = rb = player.GetComponent<Rigidbody2D>();
        invin = GetComponent<invincibility>();
        respawnPos = transform.position;
        h = GetComponent<Health>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        OriginalHealth = h.health;
        //spriteRenderer = armsprite.GetComponent<SpriteRenderer>();
        //originalMaterial = spriteRenderer.color;
        SRGO = new SpriteRenderer[SGO.Length];
        for (int index = 0; index < SGO.Length; index++) //gets all children
        {
            SRGO[index] = SGO[index].GetComponent<SpriteRenderer>();
        }
    }

    private void Update()
    {
        text.SetText($"Health: {Mathf.RoundToInt(h.health)}");
		if (Input.GetKeyDown(KeyCode.Backspace))
		{
            h.deathTrigger.Invoke();
        }

    }

	private void FixedUpdate()
	{
        if (rb.velocity.y > TerminalVelocity) rb.velocity = new Vector2(rb.velocity.x, TerminalVelocity);
    }

	public static void Knockback(float angle, float force)
	{
        canMove = false;
        prb.AddForce(new Vector2(Mathf.Cos(angle) * force *prb.mass, Mathf.Sin(angle) * force*prb.mass));
        me.Invoke("ResetCanMove", force/400);
    }

    public void ResetCanMove()
	{
        canMove = true;
	}

    public void Die()
    {
        if(dead) return; //can't trigger more than once
        dead = true;
        lives--;
        if (lives <= 0 && ms.difficulty == "h")
        {
            ms.me.EndGame(false);
        }
        //rb.velocity = new Vector2(rb.velocity.x, 17f);
        //physcoll1.isTrigger = true;
        //physcoll2.isTrigger = true;
        foreach(SpriteRenderer sr in SRGO) sr.enabled = false;
        Instantiate(ragdoll, transform.position, Quaternion.identity);
        am.Play("death");
        foreach(activation a in GameObject.FindObjectsOfType<activation>())
		{
            a.Deactivate();
        }
        
        Invoke("Respawn", 2f);
    }

    void Respawn()
    {
        grenade_master.ResetGrenades();
        gun_master.Reset();
        foreach (SpriteRenderer sr in SRGO) sr.enabled = true;
        transform.SetParent(null);
        invin.InvincibleDelay(invin.respawnDelay);
        dead = false;
        //physcoll1.isTrigger = false;
        //physcoll2.isTrigger = false;
        rb.velocity = Vector2.zero;
        transform.position = respawnPos;
        h.health = OriginalHealth;
        invulnerable = true;
        Invoke("ResetInvulnerability", RespawnShieldDelay);
    }
    void ResetInvulnerability()
	{
        invulnerable = false;
    }
    
    public static void SetSpawn(Vector2 position)
    {
        respawnPos = position;
    }

}


