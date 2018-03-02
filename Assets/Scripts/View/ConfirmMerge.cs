using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmMerge : MonoBehaviour {

    GameObject canvas;
    GameObject panel;
    Transform origine;

    // Use this for initialization
    void Start () {
        this.gameObject.GetComponent<Button>().onClick.AddListener(OpenPanel);

        origine = transform.parent;
        while (origine.name != "SceneRoot")
        {
            origine = origine.parent;
        }
        panel = origine.Find("CanvasConfirmMerge").Find("PanelMerge").gameObject;
        canvas = origine.Find("CastesSceneCanvas").gameObject;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OpenPanel()
    {
        canvas.SetActive(false);
        panel.SetActive(true);
    }
}
