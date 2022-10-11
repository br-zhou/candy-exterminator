using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenade : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    bool detonated = false;
    bool canDetonate = false;
    bool mustDetonate = false;
    Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("CanDetonate", .5f);
        Invoke("Detonate", 3f);
    }

    // Update is called once per frame


    void OnTriggerEnter2D(Collider2D x)
    {
        if (x.GetComponent<player_master>()) return;
        if (x.GetComponent<grenade>()) return;
        if (x.transform.CompareTag("bg")) return;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.SetParent(x.transform);
        if (x.GetComponent<Health>())
        {
            //print("boom");
            mustDetonate = true;
            Detonate();
        }
    }
    
    void CanDetonate()
	{
        canDetonate = true;
	}

    public void Detonate()
    {
        if (detonated) return;
        if (!canDetonate && !mustDetonate) return;
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
        detonated = true;
    }
}

