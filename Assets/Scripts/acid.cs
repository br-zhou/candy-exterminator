using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class acid : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D Object)
    {
        if (Object.gameObject.CompareTag("Player"))
        {
            player_master.me.Die();
            return;
        }

        Health h = Object.gameObject.GetComponent<Health>();
        if (h != null)
        {
            StartCoroutine(KillAfterDelay(.1f, h));
        }
    }

    private IEnumerator KillAfterDelay(float waitTime, Health h)
    {
        yield return new WaitForSeconds(waitTime);
        h.Kill();
    }

}
