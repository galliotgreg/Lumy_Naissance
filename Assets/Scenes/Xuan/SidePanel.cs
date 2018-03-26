using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidePanel : MonoBehaviour
{
    public string JSON_name = "actions";

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetFile(string name)
    {
        JSON_name = name;
    }
    public string GetFile()
    {
        return JSON_name;
    }
}