using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class tut_targets : MonoBehaviour
{

    [SerializeField] public UnityEvent finishTrigger;
    public int targetsLeft = 3;

    public void TargetDestroyed()
	{
        targetsLeft--;
        if (targetsLeft == 0) finishTrigger.Invoke();
	}
}
