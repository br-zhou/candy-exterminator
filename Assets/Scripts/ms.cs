using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ms : MonoBehaviour
{
    public static string difficulty = "e";
    public static ms me;
    public static GameObject p;
    public static bool Paused = false;
    public static int voidDepth = -20;
    public GameObject cam;
    public GameObject vcam;
    public GameObject Canvas;
    scene_logic sl;

    // Start is called before the first frame update
    void Start()
    {
        game_over.winCondition = false;
        game_over.previousScene = SceneManager.GetActiveScene().name;
        if(Canvas != null) Canvas.SetActive(true);
        me = this;
        p = GameObject.FindGameObjectWithTag("Player");
        cam.SetActive(true);
        vcam.SetActive(true);
        sl = Canvas.GetComponent<scene_logic>();
    }

    public void EndGame(bool win)
	{
        if (player_master.dead) return;
        if (win) game_over.winCondition = true;
        else game_over.winCondition = false;
        sl.LoadScene("Game Over");
	}
}
