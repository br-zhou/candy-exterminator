using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class tutorial_instructions : MonoBehaviour
{
    int totalSteps;
    public int currentStep = 1;
    bool Delay = true;
    tutorial_instructions me;
    [SerializeField] GameObject s8Targets;
    [SerializeField] GameObject s11Dummy;
    [SerializeField] GameObject AmmoText;
    GameObject player;
    bool stepStartLoop = true;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        me = this;
        totalSteps = transform.childCount;
        foreach(Transform child in transform)
		{
            child.gameObject.SetActive(false);
		}
        s8Targets.SetActive(false);
        s11Dummy.SetActive(false);
        showStep(currentStep);
        Invoke("ClearDelay", 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Delay) return;
		switch (currentStep)
		{
            case 2:
                if (Input.GetButton("Jump")) NextStep();
                break;
            case 4: //crouch
                if (Input.GetButton("Crouch")) NextStep();
                break;
            case 6:
                if (Mathf.Abs(Input.GetAxis("Mouse X")) > .2f) NextStep();
                break;
            case 7:
				if (stepStartLoop)
				{
                    AmmoText.SetActive(true);
                    stepStartLoop = false;
                }
                if (Input.GetMouseButtonDown(0)) NextStep();
                break;
            case 8:
                if (Input.GetKey(KeyCode.R)) NextStep();
                break;
            case 9:
				if (stepStartLoop)
				{
                    s8Targets.SetActive(true);
                    stepStartLoop = false;
                }
                break;
            case 10:
                if (stepStartLoop)
                {
                    Invoke("NextStep", 2f);
                    stepStartLoop = false;
                }
                break;
            case 11:
                if (stepStartLoop)
                {
                    grenade_master gm = player.transform.Find("arm").GetComponent<grenade_master>();
                    gm.enabled = true;
                    stepStartLoop = false;
                }
                if (Input.GetMouseButtonDown(1)) NextStep();
                break;
            case 12:
                if (stepStartLoop)
                {
                    s11Dummy.SetActive(true);
                    stepStartLoop = false;
                }
                break;
            case 13:
				if (stepStartLoop)
				{
                    game_over.customTitle = "TUTORIAL COMPLETE";
                    stepStartLoop = false;
                }

                break;
            default:
                break;
		}
    }


    void ClearDelay()
	{
        Delay = false;
	}

    void NextStep()
	{
        currentStep++;
        showStep(currentStep);
        hideStep(currentStep - 1);
        stepStartLoop = true;
    }

    void showStep(int step)
    {
        try
        {
            GameObject target = transform.Find($"{step}").gameObject;
            target.SetActive(true);
        }
        catch (Exception e)
        {
            print("no more steps!");
        }
    }
    void hideStep(int step)
    {
        GameObject target = transform.Find($"{step}").gameObject;
        target.SetActive(false);
    }

    public void DoStep(int step)
	{
        if (currentStep != step - 1) return;
        NextStep();
	}
    
    /*
IEnumerator showStep(int step)
{
    GameObject target = transform.Find($"{step}").gameObject;
    target.SetActive(true);
    SpriteRenderer targetSR = target.GetComponent<SpriteRenderer>();
    Color white = Color.white;
    float alpha = 0;

    while (alpha < 1)
    {
        alpha += transitionTime * Time.deltaTime;
        targetSR.color = new Color(white.r, white.g, white.b, alpha);
        yield return 0;
    }
    targetSR.color = white;
}
    */
}
