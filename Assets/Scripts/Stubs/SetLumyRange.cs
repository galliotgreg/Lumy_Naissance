using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLumyRange : MonoBehaviour {

    // Use this for initialization
    private float atkRange;
    private float pickRange;
    private float visionRange;
    void Start () {
        GameObject go = gameObject;
        atkRange = GetComponentInChildren<AgentScript>().AtkRange;
        pickRange = GetComponentInChildren<AgentScript>().PickRange;
        visionRange = GetComponentInChildren<AgentScript>().VisionRange;

        GameObject[] atkRangeObject = GameObject.FindGameObjectsWithTag("atkRange");
        GameObject[] pickRangeObject = GameObject.FindGameObjectsWithTag("pickRange");
        GameObject[] visionRangeObject = GameObject.FindGameObjectsWithTag("visionRange");

        foreach(GameObject atkObj in atkRangeObject) {
            atkObj.transform.localScale = new Vector3(atkRange*2, 0.01f, atkRange*2);
        }

        foreach (GameObject pickObj in pickRangeObject) {
            pickObj.transform.localScale = new Vector3(pickRange*2, 0.01f, pickRange*2);
        }

        foreach (GameObject visionObj in visionRangeObject) {
            visionObj.transform.localScale = new Vector3(visionRange*2, 0.01f, visionRange*2);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
