using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class no_player_collision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Collider2D[] boxc = GameObject.FindGameObjectWithTag("Player").GetComponents<Collider2D>();
        foreach (Collider2D x in boxc)
        {
            if(!x.isTrigger) Physics2D.IgnoreCollision(x, GetComponent<Collider2D>());
        }
            
    }

    // Update is called once per frame
}
