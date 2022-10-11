using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class player_collider_trigger : MonoBehaviour
{
    [SerializeField] public UnityEvent enterTrigger;
    [SerializeField] public UnityEvent exitTrigger;
    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D Object)
    {
        if (Object.gameObject.CompareTag("Player"))
        {
            enterTrigger.Invoke();
        }
    }
    private void OnTriggerExit2D(Collider2D Object)
    {
        if (Object.gameObject.CompareTag("Player"))
        {
            exitTrigger.Invoke();
        }
    }
}
