using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class scene_logic : MonoBehaviour
{
    public Animator transtion;
    string Scene;

    public void LoadScene(string s)
	{
        
        transtion.SetTrigger("Start");
        Invoke("ActuallyLoadScene", .5f);
        Scene = s;
        
    }

    void ActuallyLoadScene()
	{
        SceneManager.LoadScene(Scene);   
	}


    public void Reset()
	{
        /*
        transtion.SetTrigger("Start");
        Invoke("ActuallyLoadScene", .5f);
        */
        Scene = SceneManager.GetActiveScene().name;
        ActuallyLoadScene();
    }

	public void ExitGame()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
