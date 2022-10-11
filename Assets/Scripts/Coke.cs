using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coke : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    float multiplier = 1.5f;
    public void Death()
    {
        GameObject e = Instantiate(explosion, transform.position, Quaternion.identity);
        e.transform.localScale = new Vector3(transform.localScale.x* multiplier, transform.localScale.y* multiplier, transform.localScale.z * multiplier);
        Destroy(gameObject);
    }
}
