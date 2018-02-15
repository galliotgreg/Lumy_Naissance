using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 GameObject de branchement de transition entre deux états
 */  
public class Pin : MonoBehaviour {

    bool isGateOperator = false;
    bool isActionChild = false;

    public bool IsGateOperator {
        get {
            return isGateOperator;
        }

        set {
            isGateOperator = value;
        }
    }

    public bool IsActionChild
    {
        get
        {
            return isActionChild;
        }

        set
        {
            isActionChild = value;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown(){
		if (MCEditorManager.instance.createTransition_Started ()) {
			MCEditorManager.instance.createTransition_setEndPin (this);
		} else {
			MCEditorManager.instance.createTransition_setStartPin ( this );
		}
	}

	void OnMouseOver(){
		this.GetComponent<MeshRenderer> ().material.color = Color.red;
	}
}
