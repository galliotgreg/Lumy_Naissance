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

		if (!mouseOver) {
			if (Input.GetMouseButton (0)) {
				unselectPin ();
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
		if (MCEditorManager.instance.createTransition_Start_Pin() != null ) {
			MCEditorManager.instance.createTransition_setEndPin (this);

			unselectPin ();
		} else {
			MCEditorManager.instance.createTransition_setStartPin ( this );

			selected = true;
		}
	}

	void unselectPin(){
		if (MCEditorManager.instance.createTransition_Start_Pin() == this) {
			MCEditorManager.instance.createTransition_setStartPin ( null );
		}

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
