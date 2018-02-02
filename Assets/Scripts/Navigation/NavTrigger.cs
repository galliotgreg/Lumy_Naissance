using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.A))
        {
            NavigationManager.instance.SwapScenes("TitleScene", "CampaignScene", 2, Vector3.zero);
        }
	}
}
