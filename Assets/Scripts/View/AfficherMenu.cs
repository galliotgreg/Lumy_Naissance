using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfficherMenu : MonoBehaviour {

    public GameObject afficherSiTouche;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            afficherSiTouche.SetActive(!afficherSiTouche.activeSelf);
        }
	}
}
