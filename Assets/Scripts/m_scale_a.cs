using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class m_scale_a : MonoBehaviour
{
    public float baseLine = 1f;
    public float Range = .5f;
    public float Frequency = 1f;
    public float delay = 0;
    float time = 0;



    // Update is called once per frame
    void Update()
    {
        float scale = baseLine + Mathf.Sin(time * Frequency - delay) * Range;
        transform.localScale = new Vector3(scale, scale, scale);

        time += Time.deltaTime;
    }
}
