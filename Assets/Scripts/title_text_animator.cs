using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class title_text_animator : MonoBehaviour
{
    TextMeshProUGUI title;
    float gradient = 0f;
    int step = 1;
    float time = 0;
    void Start()
    {
        title = transform.GetComponent<TextMeshProUGUI>();
        title.color = new Color(255, 255, 255, 0);
    }
	private void Update()
	{
		switch (step)
		{
            case 1:
                if (gradient < 1)
                {
                    gradient += .5f * Time.deltaTime;
                    title.color = new Color(255, 255, 255, gradient);
				}
				else
				{
                    step++;
                    title.color = Color.white;
                    gradient = 1;
				}
                break;
            case 2:
                if(time < 2)
				{
                    time += Time.deltaTime;
				}
				else
				{
                    time = 0;
                    step++;
				}
                break;
            case 3:
                if (gradient > 0)
                {
                    gradient -= 1f * Time.deltaTime;
                    title.color = new Color(255, 255, 255, gradient);
                }
                break;
            default:
                Destroy(gameObject);
                break;
		}
	}
}
