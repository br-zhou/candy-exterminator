using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform_script : MonoBehaviour
{
    public float speed;
    public int startingPointIndex;
    public Transform[] points;
    int index;
    GameObject Parent;
    void Start()
    {
        //transform.position = points[startingPointIndex].position;
        Parent = transform.parent.gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector2.Distance(Parent.transform.position, points[index].position) < 0.02f)
		{
            index++;
            if(index == points.Length)
			{
                index = 0;
			}
		}
        Parent.transform.position = Vector2.MoveTowards(Parent.transform.position, points[index].position, speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collider)
	{
        if(collider.transform.GetComponent<no_platform>() != null) return;
        collider.transform.SetParent(Parent.transform);
	}
    void OnCollisionExit2D(Collision2D collider)
    {
        if (collider.transform.GetComponent<no_platform>() != null) return;
        collider.transform.SetParent(null);
    }
}
