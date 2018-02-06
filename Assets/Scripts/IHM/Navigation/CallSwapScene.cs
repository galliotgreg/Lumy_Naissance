using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CallSwapScene : MonoBehaviour {

    public bool isReturnButton;
    public string target;

	// Use this for initialization
	void Start () {
        this.gameObject.GetComponent<Button>().onClick.AddListener(SwapScene);
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    void SwapScene()
    {
        if (!isReturnButton)
        {
            NavigationManager.instance.SwapScenes(target, this.gameObject.transform.position);
        } else
        {
            NavigationManager.instance.GoBack(this.gameObject.transform.position);
        }
        
    }
}
