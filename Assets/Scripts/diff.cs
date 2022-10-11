using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class diff : MonoBehaviour
{
	public TextMeshProUGUI buttonText;
	Button b;
	string dif;
	private void Start()
	{
		SetText();
	}
	public void Togglediff()
	{
		if (ms.difficulty == "e")
		{
			ms.difficulty = "h";
		}
		else
		{
			ms.difficulty = "e";
		}
		SetText();
		print(ms.difficulty);
	}

	void SetText()
	{
		if (ms.difficulty == "e")
		{
			buttonText.SetText("EASY");
		}
		else
		{
			buttonText.SetText("HARD");
		}
	}
}
