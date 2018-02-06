using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CallSwapScene : MonoBehaviour {

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
        NavigationManager.instance.SwapScenes(target, this.gameObject.transform.position);
    }
}
