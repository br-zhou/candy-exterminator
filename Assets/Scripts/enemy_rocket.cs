using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_rocket : MonoBehaviour
{
    GameObject player;
    Rigidbody2D rb;
    [SerializeField] GameObject explosion;
    float velocity = 0;
    float maxSpeed = 40f;
    float accleration = 30f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        Invoke("Delete", 3f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (velocity < maxSpeed)
        {
            velocity += accleration * Time.deltaTime;
            if (velocity > maxSpeed) velocity = maxSpeed;
        }
        rb.velocity = transform.right * velocity;
    }

    void OnTriggerEnter2D(Collider2D x)
    {
        if (x.GetComponent<Gummy>()) return;
        if (x.transform.CompareTag("bg")) return;
        Detonate(); 
    }

    public void Detonate()
	{
        Instantiate(explosion, transform.position, Quaternion.identity);
        Delete();


    }
    void Delete()
	{
        Destroy(gameObject);
    }
    
}
