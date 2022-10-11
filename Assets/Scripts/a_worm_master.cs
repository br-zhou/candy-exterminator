using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class a_worm_master : MonoBehaviour
{
    float delay = .4f; //.2f
    [HideInInspector] public float overallAngle = 0;
    public int damage = 10;
    public float wiggleRange = 0;
    public bool hasRestingDirection = false;
    public float restingDirection = 0f;
    [HideInInspector] public float LerpFloat = .3f;
    public Transform pf;
    public float frequency = 6;
    public float Force = 100;
    [HideInInspector] public bool dead = false;
    [HideInInspector] public bool Awaken;
    GameObject[] Segments;
    GameObject player;
    activation a;
    audio_manager am;
    void Start()
    {
        am = GetComponent<audio_manager>();
        Segments = new GameObject[transform.childCount - 1];
        for (int index = 1; index < transform.childCount; index++) //gets all children
        {
            Segments[index - 1] = transform.GetChild(index).gameObject;
        }
        a = GetComponent<activation>();
        player = GameObject.FindGameObjectWithTag("Player");
        for (int childIndex = 0; childIndex < transform.childCount; childIndex++)
        {
            GameObject segment = transform.GetChild(childIndex).gameObject;
            a_worm w = segment.GetComponent<a_worm>();
            if (w != null)
            {
                w.delay = delay * childIndex;
            }

        }
    }

	private void FixedUpdate()
	{
        Awaken = a.Awaken;
        if (!Awaken) return;
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        pf.right = player.transform.position - transform.position;
        overallAngle = pf.transform.rotation.eulerAngles.z - 90;
    }


    public void Deconstruct()
    {
        dead = true;
        foreach (Transform child in transform)
        {
            a_worm w = child.GetComponent<a_worm>();
            if(w != null) StartCoroutine(w.Decay());
        }
        am.Play("death");
        Invoke("DeleteSelf", 3f);
    }

    void DeleteSelf()
    {
        
        Destroy(gameObject);
    }
}


