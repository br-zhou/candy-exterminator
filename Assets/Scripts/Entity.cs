using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{


    public void Death()
    {
        Destroy(gameObject);
    }
}
