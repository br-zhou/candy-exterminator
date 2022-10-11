using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class Timer_Script : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI timerText;
    public static float timerTime;
    void Start()
    {
        timerTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(ms.Paused) return;
        timerTime += Time.deltaTime;
        timerText.SetText(System.Math.Round(timerTime).ToString());
    }
}
