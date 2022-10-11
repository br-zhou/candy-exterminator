using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{
    public int CheckpointNum;
    checkpoint_master cpm;
    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        cpm = transform.parent.GetComponent<checkpoint_master>();
    }

    private void OnTriggerEnter2D(Collider2D Object)
    {
        if (cpm.CheckpointIndex >= CheckpointNum) return;
            if (Object.gameObject.CompareTag("Player")) {
            cpm.CheckpointIndex = CheckpointNum;
            player_master.SetSpawn(new Vector2(transform.position.x, transform.position.y));
            cp_text.me.Popup();
        }
    }
}
