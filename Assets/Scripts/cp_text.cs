using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class cp_text : MonoBehaviour
{
    TextMeshProUGUI text;
    Coroutine pa;
    audio_manager am;
    public static cp_text me;
    void Start()
    {
        me = this;
        am = GameObject.FindGameObjectWithTag("AM").GetComponent<audio_manager>();
        text = transform.GetComponent<TextMeshProUGUI>();
        text.color = new Color(1, 1, 1, 0);
    }
    IEnumerator PlayAnimation() {
        int step = 1;
        float gradient = 0f;
        float time = 0;
        am.Play("checkpoint");
        while (step <= 3)
		{
            switch (step)
            {
                case 1:
                    if (gradient < 1)
                    {
                        gradient += 5f*Time.deltaTime;
                    }
                    else
                    {
                        step++;
                        gradient = 1;
                    }
                    text.color = new Color(1, 1, 1, gradient);
                    break;
                case 2:
                    if (time < .5f)
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
                        gradient -= 2f * Time.deltaTime;
					}
					else
					{
                        gradient = 0f;
                        step++;
                    }
                    text.color = new Color(1, 1, 1, gradient);
                    break;
                default:
                    break;
            }
            yield return 0;
        }
    }

    public void Popup()
	{
        if (pa != null) StopCoroutine(pa);
        pa = StartCoroutine(PlayAnimation());
    }
}
