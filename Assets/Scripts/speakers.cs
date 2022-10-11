using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speakers : MonoBehaviour
{
    audio_manager am;
    void Start()
    {
        am = GetComponent<audio_manager>();
        am.Play("Theme");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
