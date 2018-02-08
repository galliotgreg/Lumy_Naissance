using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastDropDownBinding : MonoBehaviour {

    private Dropdown castDropDown;

	// Use this for initialization
	void Start () {
        castDropDown = GetComponent<Dropdown>();
	}
	
	// Update is called once per frame
	void Update () {
        string[] castNames = AppContextManager.instance.GetCastFileNames();
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
        foreach (string castName in castNames)
        {
            Dropdown.OptionData option = new Dropdown.OptionData(castName);
            options.Add(option);
        }
        castDropDown.options = options;
    }
}
