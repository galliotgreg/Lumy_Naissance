using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ViewActionLumy : MonoBehaviour {

    public GameObject subMenu;

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
        subMenu.SetActive(!subMenu.activeSelf);

        foreach (Transform child in transform.parent.parent)
        {
            if (child.Find("PanelAction").gameObject.activeSelf && child != transform.parent) child.Find("PanelAction").gameObject.SetActive(!child.Find("PanelAction").gameObject.activeSelf);
        }

        //Set Active cast
        Text txt = transform.Find("Text").gameObject.GetComponent<Text>();
        AppContextManager.instance.SwitchActiveCast(txt.text);
        CastesUIController.instance.LoadEditedLumy();
    }
}