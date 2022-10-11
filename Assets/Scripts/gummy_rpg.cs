using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class gummy_rpg : MonoBehaviour
{
    private GameObject player;
    private bool facingRight = true;
    private bool readyToShoot = true;
    bool reloading = false;
    private Gummy p;
    float Recoil = 1000f;
    //GUN
    public float fireRate,reloadTime;
    float mass;
    public Transform firePoint;
    public GameObject rocket;
    Gummy g;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        p = transform.parent.GetComponent<Gummy>();
        readyToShoot = true;
        player = GameObject.FindGameObjectWithTag("Player");
        g = transform.parent.GetComponent<Gummy>();
        rb = transform.parent.GetComponent<Rigidbody2D>();
        mass = rb.mass;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!p.Awaken) return;
        transform.right = player.transform.position - transform.position;
        float armAngle = transform.rotation.eulerAngles.z;

        if (armAngle > 90f && armAngle < 270f)
        {
            if (facingRight)
                p.Flip("left");
            transform.rotation = Quaternion.Euler(0f, 180f, -armAngle + 180f);
            facingRight = false;
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else
        {
            if (!facingRight)
                p.Flip("right");
            transform.rotation = Quaternion.Euler(0f, 0f, armAngle);
            facingRight = true;
        }

        //SHOT AT ENEMY AI

        if (g.canFire && readyToShoot)
        {
            g.Jump();
            Invoke("Shoot", .3f);
        }
    }


    void Shoot()
    {
        if (!reloading)
        {
            if (player_master.dead) return;
            readyToShoot = false;

            Vector3 shootdirection = firePoint.right;

            Instantiate(rocket, firePoint.position, firePoint.rotation);
            rb.AddForce(new Vector2(Mathf.Cos(firePoint.rotation.z + 90) * Recoil* mass, Mathf.Sin(firePoint.rotation.z +90) * Recoil* mass));
            g.canMove = false;
            Invoke("ResetCanMove", 0.5f);
            Reload();
        }
    }

    void ResetCanMove()
	{
        g.canMove = true;
    }
    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        reloading = false;
        readyToShoot = true;
    }

}
