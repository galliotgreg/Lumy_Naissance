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
		if (mouseOver) {
			this.GetComponent<MeshRenderer> ().material.color = hoverColor;
		} else {
			if (selected) {
				this.GetComponent<MeshRenderer> ().material.color = selectedColor;
			} else {
				this.GetComponent<MeshRenderer> ().material.color = regularColor;
			}
		}
	}

	[SerializeField]
	Color regularColor;
	[SerializeField]
	Color hoverColor;
	[SerializeField]
	Color selectedColor;

	bool selected = false;
	bool mouseOver = false;

	void selectPin(){
		if (MCEditorManager.instance.createTransition_Started ()) {
			MCEditorManager.instance.createTransition_setEndPin (this);
		} else {
			MCEditorManager.instance.createTransition_setStartPin ( this );
		}

		selected = true;
	}

	void unselectPin(){
		selected = false;
	}

	void OnMouseDown(){
		selectPin ();
	}

	void OnMouseEnter(){
		mouseOver = true;
	}

	void OnMouseExit(){
		mouseOver = false;
	}
}
