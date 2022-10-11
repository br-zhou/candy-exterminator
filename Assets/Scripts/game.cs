using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game : MonoBehaviour
{
    public static string weapon = "rifle";
    public static string difficulty = "easy";

    void Start()
    {
        
    }

    // Update is called once per frame
    public void UpdateWeapon(string Name)
    {
        weapon = Name;
    }
}
