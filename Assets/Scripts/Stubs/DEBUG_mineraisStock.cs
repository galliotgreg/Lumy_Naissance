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

       /* if (DEBUG_Manager.instance.debugMineraiStock == true) {

            gameObject.GetComponentInChildren<Text>().gameObject.SetActive(true);

            string stock = GetComponentInParent<ResourceScript>().Stock.ToString();
            Text stockText = gameObject.GetComponentInChildren<Text>();
            stockText.text = stock;
        }
        else
        {
            gameObject.GetComponentInChildren<Text>().gameObject.SetActive(false);
        }*/

        if(OptionManager.instance == null)
        {
            return;
        }


        if (OptionManager.instance.gisementsBool == true)
        {

            minerais = OptionManager.instance.GetMinerals();
            foreach(GameObject ressource in minerais)
            {
                ressource.GetComponentInChildren<Canvas>().gameObject.SetActive(true);

            }
        }
        else
        {
            Debug.Log("FAUX");
            minerais = OptionManager.instance.GetMinerals();
            foreach (GameObject ressource in minerais)
            {
                ressource.GetComponentInChildren<Canvas>().gameObject.SetActive(false);

            }
        }

    }

    
}
