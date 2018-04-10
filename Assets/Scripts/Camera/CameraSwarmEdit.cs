using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwarmEdit : MonoBehaviour {
    private GameObject target;

    public GameObject Target
    {
        get
        {
            return target;
        }

        set
        {
            target = value;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (target != null)
        {
            this.transform.position = new Vector3(
                target.transform.position.x, 
                this.transform.position.y, 
                target.transform.position.z);
        }
	}
}
