using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEBUG_SetLumyRange : MonoBehaviour {

    // Use this for initialization
    private float atkRange;
    private float pickRange;
    private float visionRange;
    void Start () {

        if(DEBUG_Manager.instance.debug == true) {

            gameObject.SetActive(true);

            GameObject go = gameObject;
            atkRange = GetComponentInParent<AgentScript>().AtkRange;
            pickRange = GetComponentInParent<AgentScript>().PickRange;
            visionRange = GetComponentInParent<AgentScript>().VisionRange;

            GameObject[] atkRangeObject = GameObject.FindGameObjectsWithTag("atkRange");
            GameObject[] pickRangeObject = GameObject.FindGameObjectsWithTag("pickRange");
            GameObject[] visionRangeObject = GameObject.FindGameObjectsWithTag("visionRange");

            foreach (GameObject atkObj in atkRangeObject) {
                atkObj.transform.localScale = new Vector3(atkRange * 2, 0.01f, atkRange * 2);
            }

            foreach (GameObject pickObj in pickRangeObject) {
                pickObj.transform.localScale = new Vector3(pickRange * 2, 0.01f, pickRange * 2);
            }

            foreach (GameObject visionObj in visionRangeObject) {
                visionObj.transform.localScale = new Vector3(visionRange * 2, 0.01f, visionRange * 2);
            }
        }
        else {
            gameObject.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
