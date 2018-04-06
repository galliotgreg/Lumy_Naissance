using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Accueil : MonoBehaviour {
    [SerializeField]
    Button btn_ok;
    [SerializeField]
    string keyTuto;
	// Use this for initialization
	void Start () {
        //Check View
        this.gameObject.SetActive(false);
        
        //Check Key
        if (keyTuto.Length == 0)
        {
            Debug.LogError("Error : keyTuto Not Initialized");
            return;
        }

        //Open Welcome Panel
        //Enable Tuto 
        if (SwapManager.instance.getTutorielState(keyTuto))
        {
            this.gameObject.SetActive(true);

            SwapManager.instance.SetTutorielKey(!SwapManager.instance.getTutorielState(keyTuto), keyTuto);
        }
            //Close Welcome Panel
            btn_ok.onClick.AddListener(CloseAccueil);
    }


    private void CloseAccueil()
    {
        this.gameObject.SetActive(false);
    }

}
