using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oven_legs : MonoBehaviour
{
    SpriteRenderer sr;
    Color black = Color.black;
    float target = 1f;
    bool approachingTarget = false;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    public void SetOpacity(float num)
    {
        sr.color = new Color(0, 0, 0, num);
    }

	private void Update()
	{
        if (!approachingTarget) return;
        float smoothedTransition = Mathf.Lerp(sr.color.a, target, .05f);
        sr.color = new Color(black.r, black.g, black.b, smoothedTransition);
        if(Mathf.Abs(target- sr.color.a) < .02f)
		{
            approachingTarget = false;
		}
	}

	private void OnTriggerEnter2D(Collider2D Object)
    {
        if (Object.gameObject.CompareTag("Player"))
        {
            target = .5f;
            approachingTarget = true;
        }
    }
    private void OnTriggerExit2D(Collider2D Object)
    {
        if (Object.gameObject.CompareTag("Player"))
        {
            target = 1f;
            approachingTarget = true;
        }
    }
}
