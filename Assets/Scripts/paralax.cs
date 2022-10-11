using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paralax : MonoBehaviour
{
    GameObject player;
    public float distance = 100;
    Vector3 originalPosition;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        originalPosition = transform.position;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (player_master.dead) return;
        Vector3 change = -player.transform.position/distance;
        transform.localPosition = new Vector3(change.x, change.y, 20);
    }
}
