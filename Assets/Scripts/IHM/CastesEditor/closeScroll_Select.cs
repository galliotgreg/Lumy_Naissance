using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class closeScroll_Select : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.gameObject.GetComponent<Button>().onClick.AddListener(ClosePanel);
    }

    void ClosePanel()
    {
        GameObject panel = transform.parent.parent.parent.gameObject;
        panel.SetActive(!panel.activeSelf);
    }
}
