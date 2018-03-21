using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour {
    private Help help;
    private string data;
    private GameObject tooltip;

	// Use this for initialization
	void Start () {
        tooltip = GameObject.Find("Tooltip");
        tooltip.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (tooltip.activeSelf)
        {
            Debug.Log(Input.mousePosition);
            //tooltip.transform.position = Input.mousePosition;
        }
	}

    public void Activate (Help help)
    {
        this.help = help;
        ConstructDataString();
        tooltip.SetActive(true);
    }
    public void Deactivate()
    {
        tooltip.SetActive(false);

    }
    public void ConstructDataString()
    {
        data = help.Title + help.Content;
        tooltip.transform.GetChild(0).GetComponent<Text>().text = data;
        Debug.Log(data);

    }
}
