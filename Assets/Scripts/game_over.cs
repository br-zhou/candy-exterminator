using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class game_over : MonoBehaviour
{

    [HideInInspector] public static string previousScene = "Menu";
    [HideInInspector] public static bool winCondition;
    public static string customTitle;
    public TextMeshProUGUI titletext;
    public TextMeshProUGUI subtext;
    public GameObject Panel;
    Color LostColor = Color.black;
    //Color WinColor = new Color(1, .619f, 0);
    Color WinColor;
    Image panelImage;
    scene_logic sl;

    void Start()
    {
        sl = GetComponent<scene_logic>();
        panelImage = Panel.GetComponent<Image>();
        WinColor = panelImage.color;
        if (winCondition) panelImage.color = WinColor;
        else panelImage.color = LostColor;

        if(customTitle != null)
		{
            titletext.text = customTitle;
            customTitle = null;
		}
		else
		{
            if (winCondition) titletext.text = "MISSION COMPLETE";
            else titletext.text = "GAME OVER";
		}
    }

    public void Retry()
	{
        sl.LoadScene(previousScene);
	}
}
