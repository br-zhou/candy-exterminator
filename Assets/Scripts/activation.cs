using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class activation : MonoBehaviour
{
    [HideInInspector] public bool Awaken = false;
    [SerializeField] float ActivationRange = 8f;
    [SerializeField] float DeactivationRange = 12f;
    [SerializeField] private UnityEvent activationTrigger;
    bool canDeactivate = false;
    float distanceToPlayer;
    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (!Awaken)
        {
            if (distanceToPlayer > ActivationRange) return;
            Activate();
		}
		else
		{
            if (distanceToPlayer > DeactivationRange && canDeactivate) Deactivate();
            else if (distanceToPlayer < ActivationRange && !canDeactivate) canDeactivate = true;
        }
    }

    
    public void Activate()
    {
        if (Awaken) return;
        if (distanceToPlayer > ActivationRange + 1) canDeactivate = false; //basically, if shot at a distance, must run up to player
        Awaken = true;
        activationTrigger.Invoke();
    }

    public void Deactivate()
    {
        if (!Awaken) return;
        Awaken = false;
        canDeactivate = true;
    }
}
