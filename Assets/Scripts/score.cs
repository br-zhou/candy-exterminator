using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class score : MonoBehaviour
{
    public GameObject ScoreUI;
    static GameObject ScoreGO;
    public int Score = 0;
    static score me;
    public TextMeshProUGUI text;

    void Start()
    {
        me = this;
        if (ScoreUI != null) ScoreGO = ScoreUI;
    }

    void Update()
    {
        text.SetText("Score: " + Score);
    }

    public static void Popup(int num, Vector3 pos)
	{
        addPoints(num);
        GameObject s = Instantiate(ScoreGO, pos, Quaternion.identity);
        TextMeshProUGUI t = s.transform.Find("text").GetComponent<TextMeshProUGUI>();
        t.SetText("+"+num);
    }
    public static void addPoints(int num)
	{
        me.Score += num;
	}
    public static int GetScore()
	{
        return me.Score;
	}
}
