using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSwap : MonoBehaviour
{
    SpriteRenderer sr;
    public string Target;
    public float scale = 1;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
		try
		{
            sr.sprite = image_import.WebImgDict[Target];
        }
        catch (KeyNotFoundException e)
        {
            Debug.LogWarning(e);
        }
        
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
