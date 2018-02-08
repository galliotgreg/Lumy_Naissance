using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ViewSubMenu : MonoBehaviour {

    public GameObject subMenu;
    public GameObject subMenuBis;

    // Use this for initialization
    void Start () {
        this.gameObject.GetComponent<Button>().onClick.AddListener(SwitchMenu);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void SwitchMenu()
    {
        /*
        subMenu.SetActive(!subMenu.activeSelf);

        if (subMenuBis.activeSelf) subMenuBis.SetActive(!subMenuBis.activeSelf);
        */
        foreach( Transform child in transform.parent.parent)
        {
            if (child.gameObject == subMenu || child.gameObject == subMenuBis)
            {
                child.gameObject.SetActive(!child.gameObject.activeSelf);
            }
            else
            {
                if (child.gameObject.activeSelf && child != transform.parent) child.gameObject.SetActive(!child.gameObject.activeSelf);
            }
            

        }
    }
}
