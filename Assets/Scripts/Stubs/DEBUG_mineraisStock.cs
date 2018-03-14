using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DEBUG_mineraisStock : MonoBehaviour {


    GameObject[] minerais = null;

	// Use this for initialization
	void Start () {

    }

    // Update is called once per frame
    void Update() {
 

        //if(OptionManager.instance == null)
        //{
        //    return;
        //}

        //if (OptionManager.instance.gisementsBool == true)
        //{
        //    minerais = OptionManager.instance.GetMinerals();
        //    foreach(GameObject ressource in minerais)
        //    {
        //        ressource.transform.GetChild(0).gameObject.SetActive(true);
        //        string stock = ressource.GetComponent<ResourceScript>().Stock.ToString();
        //        Text stockText = ressource.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<Text>();
        //        stockText.text = stock;
        //    }
        //}
        //else
        //{
        //    minerais = OptionManager.instance.GetMinerals();
        //    foreach (GameObject ressource in minerais)
        //    {
        //        ressource.transform.GetChild(0).gameObject.SetActive(false);
        //    }
        //}
    }  
}
