using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class image_import : MonoBehaviour
{
    // Start is called before the first frame update
    public string[] Name;
    public string[] Link;
    public static bool alreadyRunning;
    //Dictionary<string, string> NameLinkDict = new Dictionary<string, string>();
    public static Dictionary<string, Sprite> WebImgDict = new Dictionary<string, Sprite>();

    void Awake(){
        if(alreadyRunning){
            print("death");
            Destroy(this);
        } else {
            alreadyRunning = true;
            print("me");
        }
    }

    void Start()
    {
        for(int i = 0; i < Name.Length; i++)
        {
            StartCoroutine(PushDict(Name[i],Link[i]));
        }
        
    }

    IEnumerator PushDict(string name, string url)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture2D myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            Sprite s = Sprite.Create(myTexture, new Rect(0, 0, myTexture.width, myTexture.height), new Vector2(0.5f, 0.5f), myTexture.width);
            WebImgDict.Add(name, s);
            //print($"{name} was successfully added!");
        }
    }
}
