using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_script : MonoBehaviour
{
    public Transform cam_follow;
    public Transform fire_point;
    public static camera_script cs;
	float sensitivity = 100;

	private void Awake()
	{
		cs = transform.gameObject.GetComponent<camera_script>();
	}

	// Update is called once per frame
	public void UpdateVCAM(float speed)
    {
		if (!player_master.dead)
		{
            //cam_follow.position = fire_point.position + new Vector3(speed*sensitivity,0,0);
            cam_follow.position = fire_point.position;
		}
    }
}
