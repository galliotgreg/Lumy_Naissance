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
        subMenu.active = !subMenu.active;

        if (subMenuBis.active) subMenuBis.active = !subMenuBis.active;
    }
}
