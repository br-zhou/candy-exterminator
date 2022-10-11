using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class m_rotation_a : MonoBehaviour
{
    Quaternion originalRotation;
    public float rotationRange = 3f;
    public float Frequency = 1f;
    public float delay = 0;
    float time = 0;
    [SerializeField] bool flipped = false;
    void Start()
    {
        
        if(flipped) originalRotation = Quaternion.Euler(-180,0, transform.rotation.eulerAngles.z+180);
		else originalRotation = transform.rotation;
        

    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(originalRotation.eulerAngles.x, originalRotation.eulerAngles.y, originalRotation.eulerAngles.z + Mathf.Sin(time*Frequency-delay)*rotationRange);
        
        time += Time.deltaTime;
    }
}
