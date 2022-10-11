using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class a_worm : MonoBehaviour
{
    public bool dead = false;
    public float delay = 0;
    float LerpFloat;
    float wiggleRange;
    float time = 0;
    public bool inverted = false;
    int damage;
    float frequency;
    Rigidbody2D rb;
    a_worm_master wm;
    float decaySpeed = 2f;
    SpriteRenderer sr;
    Color originalColor;
    audio_manager am;
    float attackTimer = 0f;
    void Start()
    {
        am = transform.parent.GetComponent<audio_manager>();
        rb = GetComponent<Rigidbody2D>();
        wm = transform.parent.GetComponent<a_worm_master>();
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
        damage = wm.damage;
        wiggleRange = wm.wiggleRange;
        LerpFloat = wm.LerpFloat;
        frequency = wm.frequency;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!wm.Awaken)
        {
            if(wm.hasRestingDirection) rb.MoveRotation(Mathf.LerpAngle(rb.rotation, wm.restingDirection, LerpFloat));
            return;
        }
		if (!wm.dead)
		{
            float overallAngle = wm.overallAngle;
            if (inverted) overallAngle += 180;
            float targetAngle = overallAngle + Mathf.Sin(frequency * time + delay) * wiggleRange;
            
            rb.MoveRotation(Mathf.LerpAngle(rb.rotation, targetAngle, LerpFloat));
            time += Time.deltaTime;
        } 
        if(attackTimer > 0)
		{
            attackTimer -= Time.deltaTime;
		}
    }

    private void OnTriggerStay2D(Collider2D Object)
    {
		if (!wm.dead)
		{
            if (Object.gameObject.CompareTag("Player"))
            {
                Health hs = Object.GetComponent<Health>();
                hs.TakeDamage(damage);
                if(attackTimer <= 0)
				{
                    am.Play("attack");
                    attackTimer = 2f;
                }
                
                player_master.Knockback(transform.rotation.eulerAngles.z,100);
            }
        }
    }

    public void Die()
	{
        Destroy(gameObject);
	}

    public IEnumerator Decay()
    {
        float Transparency = 1;
        //yield return new WaitForSeconds(5);
        while (Transparency > 0)
        {
            if (sr == null) yield break;
            
            Transparency -= decaySpeed * Time.deltaTime;

            sr.color = new Color(originalColor.r, originalColor.g, originalColor.b, Transparency);
            
            yield return new WaitForSeconds(0.02f);
        }
    }
}
