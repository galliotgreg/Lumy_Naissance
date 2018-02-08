using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveCastTextBinding : MonoBehaviour {

    private Text activeCastText;

    // Use this for initialization
    void Start () {
        activeCastText = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        activeCastText.text = AppContextManager.instance.ActiveBehaviorPath;
    }
}
