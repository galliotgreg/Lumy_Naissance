using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateNuee : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.gameObject.GetComponent<Button>().onClick.AddListener(CreateSpecie);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void CreateSpecie()
    {
        string defaultName = BuildDefaultName();
        AppContextManager.instance.CreateSpecie(defaultName);
    }

    private string BuildDefaultName()
    {
        int nbSpecies = AppContextManager.instance.GetSpeciesFolderNames().Length + 1;
        return AppContextManager.instance.DEFAULT_SPECIE_NAME + nbSpecies;
    }
}
