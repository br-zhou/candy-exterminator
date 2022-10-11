using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class m_transparency_a : MonoBehaviour
{
    public float baseLine = .5f;
    public float Range = .5f;
    public float Frequency = 1f;
    public float delay = 0;
    float time = 0;
    [SerializeField] bool Binary = false;
    Color original;
    SpriteRenderer sr;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        original = sr.color;
    }

    // Update is called once per frame
    void Update()
    {
        float alpha = baseLine + Mathf.Sin(time * Frequency - delay) * Range;
		if (Binary)
		{
            if(alpha > .75)
			{
                alpha = 1;
			}
			else
			{
                alpha = 0;
			}
		}
        sr.color = new Color(original.r, original.g, original.b, alpha);

        time += Time.deltaTime;
    }
}
