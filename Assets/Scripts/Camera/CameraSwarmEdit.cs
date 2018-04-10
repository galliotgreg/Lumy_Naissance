using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwarmEdit : MonoBehaviour {
    private GameObject target;
    private GameObject hearth;

    public GameObject Target
    {
        get
        {
            return target;
        }

        set
        {
            target = value;
            hearth = target.transform.Find("Hearth").gameObject;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (hearth != null)
        {
            this.transform.position = new Vector3(
                hearth.transform.position.x, 
                this.transform.position.y,
                hearth.transform.position.z);
        }
	}
}
