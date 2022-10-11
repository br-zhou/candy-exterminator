using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class popup_script : MonoBehaviour
{
    // Start is called before the first frame update
    TextMeshProUGUI score;
    float fade = 1f;
    float timeMultiplier = 1f;
    void Start()
    {
        score = transform.Find("text").GetComponent<TextMeshProUGUI>();
        score.color = new Color(1, 1, 1, fade);
        Invoke("Destroy", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        if(fade > 0)
		{
            fade -= Time.deltaTime * timeMultiplier;
            score.color = new Color(1, 1, 1, fade);
        }
    }

    void Destroy()
	{
        Destroy(this);
	}
}
