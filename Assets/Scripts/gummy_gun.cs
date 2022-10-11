using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class gummy_gun : MonoBehaviour
{
    private GameObject player;
    private bool facingRight = true;
    private Gummy p;

    //GUN
    public int damage, impact;
    float spread = 40f;
    public float SpreadMultiplier;
    public float fireRate, reloadTime;
    public int magazineSize;
    private int bulletsPerTap = 1;
    public bool Automaic;
    public int BurstAmount = 1;
    public float BurstDelay = 0;
    int bulletsLeft, bulletsShot;
    bool readyToShoot, reloading;

    public Transform barrelTip;
    public Transform firePoint;
    public GameObject bulletTrail;
    public LayerMask RaycastLayers;
    public GameObject bulletImpact;
    private SpriteRenderer gunflash;
    public HashSet<int> objectIDs;
    Gummy g;
    Rigidbody2D rb;
    audio_manager am;
    // Start is called before the first frame update
    void Start()
    {
        am = transform.parent.GetComponent<audio_manager>();
        p = transform.parent.GetComponent<Gummy>();
        objectIDs = new HashSet<int>();
        bulletsLeft = magazineSize;
        readyToShoot = true;
        player = GameObject.FindGameObjectWithTag("Player");
        gunflash = transform.Find("gunfire").GetComponent<SpriteRenderer>();
        g = transform.parent.GetComponent<Gummy>();
        rb = transform.parent.GetComponent<Rigidbody2D>();
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
        if(bulletsLeft <= 0 && !reloading)
		{
            Reload();
		}
		if (g.canFire)
		{
            Fire();
		}
    }


    IEnumerator Shoot()
    {
        if (player_master.dead) yield break;
        readyToShoot = false;

        for (int i = 0; i < BurstAmount; i++)
        {

            /*
            spread = WeaponSpread + Mathf.Abs(rb.velocity.x) / g.runSpeed * SpreadMultiplier;
            if (!g.isGrounded) spread *= SpreadMultiplier;
            else if (Mathf.Abs(rb.velocity.x) < .02f)
            {
                spread /= SpreadMultiplier;
            }
            print("g: "+spread);
            */
            float yspread = UnityEngine.Random.Range(-spread, spread);

            //Calculate Direction with Spread
            Vector3 shootdirection = barrelTip.right + new Vector3(0f, yspread / 100, 0f);

            bulletsLeft--;
            bulletsShot--;
            am.Play("shoot");
            if (bulletsShot > 0 && bulletsLeft > 0)
			{
                Shoot();
            }
                



            RaycastHit2D hitInfo = Physics2D.Raycast(barrelTip.position, shootdirection, 100f, RaycastLayers);

            gunflash.enabled = true; //gun flash
            Invoke("RemoveFlash", 0.05f);
            //draws bullet trail
            GameObject bt = Instantiate(bulletTrail, barrelTip.position, barrelTip.rotation);
            objectIDs.Add(bt.GetInstanceID());
            LineRenderer lineRenderer = bt.GetComponent<LineRenderer>();
            GameObject bi = null;
            if (hitInfo)
            {
                //print(hitInfo.transform.tag);

                Health enemy = hitInfo.transform.GetComponent<Health>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }


                bi = Instantiate(bulletImpact, hitInfo.point, Quaternion.identity); //Impact effect!
                objectIDs.Add(bi.GetInstanceID());
                lineRenderer.SetPosition(0, barrelTip.position);
                lineRenderer.SetPosition(1, hitInfo.point);
            }
            else
            {
                lineRenderer.SetPosition(0, barrelTip.position);
                lineRenderer.SetPosition(1, barrelTip.position + shootdirection * 20);
            }
            StartCoroutine(RemoveGunParticles(bt, bi));
            yield return new WaitForSeconds(BurstDelay);
        }
        Invoke("ResetShot", fireRate);
    }

    public void Fire()
	{
        if (readyToShoot && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            StartCoroutine(Shoot());
        }
    }

    public void RemoveTrails()
    {
        if(objectIDs == null)
		{
            Debug.Log("ObjectID is NULL");
            return;
		}
        foreach (int ID in objectIDs)
        {
            Destroy(FindObjectFromInstanceID(ID));
        }
    }
    IEnumerator RemoveGunParticles(GameObject bt, GameObject bi)
    {
        yield return new WaitForSeconds(.3f);

        Destroy(bt);
        Destroy(bi);
        if(objectIDs != null)
		{
            try
            {
                objectIDs.Remove(bt.GetInstanceID());
                objectIDs.Remove(bi.GetInstanceID());
            }
            catch (Exception e)
            {
            }
            
        }
    }
    private void ResetShot()
    {
        readyToShoot = true;
    }

    void RemoveFlash()
    {
        gunflash.enabled = false;
    }
    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }
    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
        // print("reload done");
    }

    public static UnityEngine.Object FindObjectFromInstanceID(int iid)
    {
        return (UnityEngine.Object)typeof(UnityEngine.Object)
                .GetMethod("FindObjectFromInstanceID", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                .Invoke(null, new object[] { iid });

    }

}
