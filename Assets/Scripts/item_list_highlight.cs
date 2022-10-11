using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class item_list_highlight : MonoBehaviour
{

    public string[] Name;
    public GameObject[] Button;

    Dictionary<string, GameObject> ItemButtonDict = new Dictionary<string, GameObject>();
    void Start()
    {

        for (int i = 0; i < Name.Length; i++)
        {
            ItemButtonDict.Add(Name[i], Button[i]);
        }

        
        EventSystem.current.SetSelectedGameObject(null); //clear any previous selection (best practice)
        EventSystem.current.SetSelectedGameObject(ItemButtonDict[game.weapon]); //selection the button you dropped in the inspector
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
