using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class sub_text : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject scoreo;
    public GameObject timeo;
    TextMeshProUGUI s;
    TextMeshProUGUI t;

    void Start()
    {
        t = timeo.GetComponent<TextMeshProUGUI>();
        s = scoreo.GetComponent<TextMeshProUGUI>();
        s.SetText("Score: "+score.GetScore());
        t.SetText("Time: " + System.Math.Round(Timer_Script.timerTime, 2));
    }
}
