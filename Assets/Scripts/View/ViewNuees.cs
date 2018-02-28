using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewNuees : MonoBehaviour {

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

    }
}