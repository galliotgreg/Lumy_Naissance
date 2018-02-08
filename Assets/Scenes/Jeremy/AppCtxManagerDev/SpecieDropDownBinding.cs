using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecieDropDownBinding : MonoBehaviour {

    private Dropdown specieDropDown;

	// Use this for initialization
	void Start () {
        specieDropDown = GetComponent<Dropdown>();
	}
	
	// Update is called once per frame
	void Update () {
        string[] specieNames = AppContextManager.instance.GetSpeciesFolderNames();
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
        foreach (string specieName in specieNames)
        {
            Dropdown.OptionData option = new Dropdown.OptionData(specieName);
            options.Add(option);
        }
        specieDropDown.options = options;
    }
}
