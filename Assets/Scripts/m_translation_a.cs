using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class m_translation_a : MonoBehaviour
{
    Vector3 originalPosition;
    public float movementRangeX = 0f;
    public float movementRangeY = 0f;
    public float FrequencyX = 1f;
    public float FrequencyY = 1f;
    public float delayX = 0;
    public float delayY = 0;
    float time = 0;
    void Start()
    {
        originalPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(originalPosition.x + Mathf.Sin(time * FrequencyX - delayX) * movementRangeX, originalPosition.y + Mathf.Sin(time * FrequencyY - delayY) * movementRangeY, originalPosition.z);
        time += Time.deltaTime;
    }
}
