using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class gun_master : MonoBehaviour
{
    //Gun stats
    public int damage;
    float spread;
    public float WeaponSpread;
    public float SpreadMultiplier;
    public float fireRate, reloadTime;
    public int magazineSize;
    public bool Automaic;
    public int BurstAmount = 1;
    public float BurstDelay = 0;
    int bulletsLeft;
    bool shooting, readyToShoot, reloading;
    SpriteRenderer armSprite;
    public TextMeshProUGUI text;
    public Transform barrelTip;
    public Transform firePoint;
    public GameObject bulletTrail;
    //public LineRenderer bulletTrail;
    [SerializeField] private Camera cam;
    private player_controller pc;
    private Vector2 mousePos;
    private bool facingRight = true;
    public LayerMask RaycastLayers;
    Vector3 originalLocalPosition;
    public GameObject bulletImpact;
    private SpriteRenderer gunflash;
    static gun_master gm;
    audio_manager am;
    public Sprite assault;
    public Sprite shotgun;
    public Sprite lmg;


    void Start()
    {
        if (gm == null) gm = this;
        bulletsLeft = magazineSize;
        readyToShoot = true;
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<player_controller>();
        originalLocalPosition = transform.localPosition;
        gunflash = transform.Find("gunfire").GetComponent<SpriteRenderer>();
        am = GameObject.FindGameObjectWithTag("Player").GetComponent<audio_manager>();
        armSprite = transform.Find("arm sprite").GetComponent<SpriteRenderer>();

		switch (game.weapon)
		{
            case "shotgun":
                print("shotty selected");
                damage = 30;
                WeaponSpread = 12;
                SpreadMultiplier = 1;
                fireRate = .75f;
                reloadTime = 1.7f;
                magazineSize = 40;
                Automaic = false;
                BurstAmount = 5;
                BurstDelay = 0;

                armSprite.sprite = shotgun;
                break;
            case "lmg":
                print("lmg selected");
                damage = 50;
                WeaponSpread = 14;
                SpreadMultiplier = 10;
                fireRate = .175f;
                reloadTime = 2.5f;
                magazineSize = 100;
                Automaic = true;
                BurstAmount = 1;
                BurstDelay = 0;

                armSprite.sprite = lmg;
                break;
            case "assault":
            default:
                print("rifle selected");
                damage = 30;
                WeaponSpread = 14;
                SpreadMultiplier = 7;
                fireRate = .1f;
                reloadTime = 1.5f;
                magazineSize = 30;
                Automaic = true;
                BurstAmount = 1;
                BurstDelay = 0;

                armSprite.sprite = assault;
                break;
		}
       
        bulletsLeft = magazineSize;
    }

    private void Update()
    {

        if (!ms.Paused)
        {
            //arm follows player
            if (player_master.isCrouching) 
            {
                transform.localPosition = originalLocalPosition + new Vector3(0f, -.22f, 0f);
            }
            else
            {
                transform.localPosition = originalLocalPosition;
            }

            //get left click
            if (Automaic) shooting = Input.GetMouseButton(0);
            else shooting = Input.GetMouseButtonDown(0);

            if (((Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize) || Input.GetMouseButtonDown(0) && bulletsLeft ==0) && !reloading) Reload();

            //Shoot
            if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
            {
                StartCoroutine(Shoot());
            }

            if (game.weapon == "shotgun")
			{
                text.SetText(bulletsLeft/BurstAmount + " / " + magazineSize/BurstAmount);
            }
			else
			{
                text.SetText(bulletsLeft + " / " + magazineSize);
            }
            
        }
    }

    void FixedUpdate()
    {
        if (player_master.dead) return;
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDif = mousePos - new Vector2(transform.position.x, transform.position.y);
        float armAngle = Mathf.Atan2(lookDif.y, lookDif.x) * Mathf.Rad2Deg;
        
        if (armAngle > 90f || armAngle < -90f)
        {
            //if(armAngle > 0) armAngle = Mathf.Clamp(armAngle, 90, 181);
            //if (armAngle < 0) armAngle = Mathf.Clamp(armAngle, -181, -150);
            if (facingRight)
                pc.FlipSprite("left");
            transform.rotation = Quaternion.Euler(0f, 180f, -armAngle + 180f);
            facingRight = false;
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else
        {
            //if (armAngle < 0) armAngle = Mathf.Clamp(armAngle, -30, 91);
            if (!facingRight)
                pc.FlipSprite("right");
            transform.rotation = Quaternion.Euler(0f, 0f, armAngle);
            facingRight = true;
        }

        //transform.rotation = Quaternion.Euler(0f, 0f, armAngle);
    }

    IEnumerator Shoot()
    {
        if (player_master.dead) yield break;
        readyToShoot = false;
        for (int i = 0; i < BurstAmount; i++)
        {

            spread = WeaponSpread + Mathf.Abs(player_master.velocity) / player_master.maxSpeed * SpreadMultiplier;
            if (player_master.isJumping) spread *= SpreadMultiplier;
            else if (player_master.isCrouching)
            {
                spread /= SpreadMultiplier;
                spread += Mathf.Abs(player_master.velocity) / player_master.maxSpeed * SpreadMultiplier;
            }
            float yspread = Random.Range(-spread, spread);

            //Calculate Direction with Spread
            Vector3 shootdirection = barrelTip.right + new Vector3(0f, yspread / 100, 0f);

            bulletsLeft--;

            if (bulletsLeft > 0)
			{
                //Shoot();
                am.Play("fire");
            }
            



            RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, shootdirection, 100f, RaycastLayers);

            gunflash.enabled = true; //gun flash
            Invoke("RemoveFlash", 0.05f);
            //draws bullet trail
            GameObject bt = Instantiate(bulletTrail, barrelTip.position, barrelTip.rotation);
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

                lineRenderer.SetPosition(0, barrelTip.position);
                lineRenderer.SetPosition(1, hitInfo.point);
            }
            else
            {
                lineRenderer.SetPosition(0, barrelTip.position);
                lineRenderer.SetPosition(1, barrelTip.position + shootdirection * 20);
            }
            StartCoroutine(RemoveGunParticles(bt,bi));
            yield return new WaitForSeconds(BurstDelay);
        }
        Invoke("ResetShot", fireRate);
    }

    IEnumerator RemoveGunParticles(GameObject bt,GameObject bi)
	{
        yield return new WaitForSeconds(.3f);

        Destroy(bt);
        Destroy(bi);
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
        am.Play("reload");
        Invoke("ReloadFinished", reloadTime);
    }
    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
       // print("reload done");
    }

    public static void Reset()
	{
        gm.bulletsLeft = gm.magazineSize;
	}
}