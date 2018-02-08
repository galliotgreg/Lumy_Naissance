using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class ViewActionLumy : MonoBehaviour {

    GameObject subMenu;

    // Use this for initialization
    void Start()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(SwitchMenu);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SwitchMenu()
    {
    /*    subMenu.SetActive(!subMenu.activeSelf);

        foreach (Transform child in transform.parent.parent)
        {
            if (child.Find("PanelAction").activeSelf && child != transform.parent) child.gameObject.SetActive(!child.gameObject.activeSelf);
        }*/
    }
}