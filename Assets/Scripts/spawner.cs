using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public int groupNum;
    spawner_master sm;
    public GameObject prevGroup;
    public GameObject nextGroup;
    public GameObject deleteGroup;
    GameObject player;
    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        sm = transform.parent.GetComponent<spawner_master>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D Object)
    {
        if (sm.GroupIndex >= groupNum) return;
        if (Object.gameObject.CompareTag("Player"))
        {
            if(prevGroup != null)
			{
                foreach(Transform obj in prevGroup.transform)
				{
                    float distance = Vector2.Distance(obj.transform.position, player.transform.position);
                    if(distance > 10f)
					{
                        Destroy(obj.gameObject);
					}
				}
			}
            sm.GroupIndex = groupNum;
            if(nextGroup != null) nextGroup.SetActive(true);
            if (deleteGroup != null) Destroy(deleteGroup);
        }
    }
}
