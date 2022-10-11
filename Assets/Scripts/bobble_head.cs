using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class bobble_head : MonoBehaviour
{
    Vector3 originalLocalPosition;
    SpriteRenderer sr;
    public string link;
    Sprite defaultHead;
    Sprite jumpingHead;
    // Start is called before the first frame update
    void Start()
    {
        originalLocalPosition = transform.localPosition;
        sr = GetComponent<SpriteRenderer>();
		try
        {
            defaultHead = image_import.WebImgDict["Head"];
            jumpingHead = image_import.WebImgDict["Jump"];
        }
        catch (KeyNotFoundException e)
		{
            Debug.LogWarning(e);
		}

    }

    
    void Update()
    {
        if (player_master.isJumping)
        {
            sr.sprite = jumpingHead;
        }
        else
        {
            sr.sprite = defaultHead;
        }
        if (player_master.isCrouching)
        {
            transform.localPosition = originalLocalPosition + new Vector3(0f, -.22f, 0f);
        }
        else
        {
            transform.localPosition = originalLocalPosition;
        }
    }

}
