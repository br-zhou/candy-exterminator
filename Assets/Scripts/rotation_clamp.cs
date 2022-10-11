using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation_clamp : MonoBehaviour
{
    public float Range = 70f;
    bool flipping = false;
    float smoothedRotation;
    float lerpSpeed = .2f;
    Rigidbody2D rb;
    void Start()
	{
        rb = GetComponent<Rigidbody2D>();
       
    }
    void FixedUpdate()
    {
        float rotation = transform.rotation.eulerAngles.z;
  
        float clamp = Mathf.Clamp(transform.rotation.eulerAngles.z, 0, 40);

        int roundedRotation = Mathf.RoundToInt(rotation);

        if (roundedRotation % 90 == 0 && roundedRotation != 0 && roundedRotation != 360 && !flipping)
		{
            flipping = true;
		}
		if (flipping)
		{
            if(roundedRotation >= 260)
			{
                smoothedRotation = Mathf.Lerp(rotation, 360, lerpSpeed);
			}
			else
			{
                smoothedRotation = Mathf.Lerp(rotation, 0, lerpSpeed);
            }
            
            transform.rotation = Quaternion.Euler(0, 0, smoothedRotation);
            if (Mathf.RoundToInt(transform.rotation.eulerAngles.z) == 0)
			{
                flipping = false;
			}
        }

    }

    void FlipUpright()
	{
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
