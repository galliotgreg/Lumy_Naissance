using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmMerge : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.gameObject.GetComponent<Button>().onClick.AddListener(OpenPanel);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OpenPanel()
    {
        Transform panel = transform.Find("PanelMerge");
        panel.position = Vector3.zero;
        panel.gameObject.SetActive(true);
    }
}
