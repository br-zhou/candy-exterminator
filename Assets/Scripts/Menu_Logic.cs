using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Logic : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] tabs;

    public void LoadMenu(int index)
	{
		DisableAll();
		tabs[index].SetActive(true);
	}

    void DisableAll()
	{
        foreach(GameObject o in tabs)
		{
            o.SetActive(false);
		}
	}

	private void Start()
	{
		LoadMenu(0);
	}
}
