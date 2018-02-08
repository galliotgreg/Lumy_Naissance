using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveSpecieTextBinding : MonoBehaviour {

    private Text activeSpecieText;

    // Use this for initialization
    void Start () {
        activeSpecieText = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        activeSpecieText.text = AppContextManager.instance.ActiveSpeciePath;
    }
}
