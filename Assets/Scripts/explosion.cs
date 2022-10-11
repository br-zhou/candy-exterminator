using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour
{
    //[SerializeField]
    public float Damage = 15;
    public float Knockback = 1000f;
    [SerializeField] bool IndiscriminateMass = false;
    // Start is called before the first frame update
    private HashSet<int> objectIDs = new HashSet<int>();
    float mass;
    audio_manager am;

    private void Start()
	{
        am = GameObject.FindGameObjectWithTag("AM").GetComponent<audio_manager>();
        am.Play("explosion");
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, transform.localScale.x*2);
        foreach (Collider2D x in colliders)
        {
            Health health = x.gameObject.GetComponent<Health>();
            if (health != null)
            {
                int ID = x.gameObject.GetInstanceID();
                if (objectIDs.Contains(ID)) continue;
                objectIDs.Add(ID);
                if (x.gameObject.CompareTag("Player") && player_master.invulnerable) continue; //special script for player (bc invulnerabiltiy())

                //float TrueDamage = Damage * (transform.localScale.x - .5f) * (transform.localScale.x - .5f);
                health.TakeDamage(Mathf.RoundToInt(Damage));



            }
            Rigidbody2D rb = x.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                if (IndiscriminateMass) mass = rb.mass;
                else mass = 1;

                Vector2 positionDiff = x.gameObject.transform.position - transform.position;
                float angle = Mathf.Atan2(positionDiff.y, positionDiff.x);
                rb.velocity = Vector2.zero;
                rb.AddForce(new Vector2(Mathf.Cos(angle)*Knockback, Mathf.Sin(angle) * Knockback * mass));
            }
        }
        Destroy(gameObject, 0.5f);
    }
}
