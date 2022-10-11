using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class a_worm_force : MonoBehaviour
{
    Rigidbody2D rb;
    public Transform player_seeker;
    a_worm_master wm;
    GameObject player;
    float Force;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        wm = transform.parent.GetComponent<a_worm_master>();
        Force = wm.Force;
        //Force = wm.Force;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		if (!wm.dead && wm.Awaken)
		{
            //float angle = player_seeker.transform.rotation.eulerAngles.z;
            Vector2 positionDiff = player.transform.position - transform.position;
            float distance = Vector2.Distance(player.transform.position, transform.position);
            float angle = Mathf.Atan2(positionDiff.y, positionDiff.x);
            if(distance > 1)
			{
                rb.AddForce(new Vector2(Mathf.Cos(angle) * Force * rb.mass, Mathf.Sin(angle) * Force * rb.mass));
            }
            
        }
    }
}
