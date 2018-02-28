using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CallSwapScene : MonoBehaviour {

    public bool isReturnButton;
    public bool disableZoom;
    public string target;
    public string panel;

	// Use this for initialization
	void Start () {
        this.gameObject.GetComponent<Button>().onClick.AddListener(SwapScene);
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    void SwapScene()
    {
        if (!isReturnButton && (panel == null || panel == ""))
        {
            if (!disableZoom)
            {
                NavigationManager.instance.SwapScenes(target, this.gameObject.transform.position);
            } else
            {
                NavigationManager.instance.SwapScenesWithoutZoom(target);
            }

        } else if (isReturnButton)
        {
            NavigationManager.instance.GoBack(this.gameObject.transform.position);
        } else
        {
            NavigationManager.instance.SwapScenesWithPanel(target, panel, this.gameObject.transform.position);
        }
        
    }
}
