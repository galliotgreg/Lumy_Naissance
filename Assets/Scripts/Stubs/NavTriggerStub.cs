using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavTriggerStub : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.A))
        {

            NavigationManager.instance.SwapScenes(
                "TitleScene", "CampaignScene", new Vector3(-35f, 0f, 0f));
        }
	}
}
