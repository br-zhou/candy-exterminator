using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pause_script : MonoBehaviour
{
    public static bool IsPaused = false;
    public Image crossFade;
    public GameObject PauseMenuUI;
    public GameObject GameMenuUI;
    bool canPause = true;
    scene_logic sl;

    void Start(){
        sl = GetComponent<scene_logic>();
        PauseMenuUI.SetActive(false);
        GameMenuUI.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return)) {
            PauseToggle();
		}

        if (IsPaused)
		{
            if(Input.GetKeyDown(KeyCode.R))
			{
                sl.Reset();
                DisablePause();
			}
		}
	}

	public void ExitGame()
	{
        Debug.Log("Exit");
        Application.Quit();
    }

    public void DisablePause()
	{
        canPause = false;
        PauseToggle();

    }

    public void PauseToggle()
    {

        if (IsPaused)
        {
            //play
            IsPaused = false;
            crossFade.enabled = true;
            PauseMenuUI.SetActive(false);
            GameMenuUI.SetActive(true);
            Time.timeScale = 1f;
            ms.Paused = false;
        }
        else
        {
            if (!canPause) return;
            //pauses
            IsPaused = true;
            crossFade.enabled = false;
            PauseMenuUI.SetActive(true);
            GameMenuUI.SetActive(false);
            Time.timeScale = 0f;
            ms.Paused = true;
        }
    }

}
