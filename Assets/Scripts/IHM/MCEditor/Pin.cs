using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 GameObject de branchement de transition entre deux états
 */  
public class Pin : SelectableProxyGameObject {

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
		base.Update ();
	}

	#region implemented abstract members of SelectableProxyGameObject

	protected override void select ()
	{
		if (MCEditorManager.instance.createTransition_Start_Pin() != null ) {
			MCEditorManager.instance.createTransition_setEndPin (this);

			base.unselectGameObject ();
		} else {
			MCEditorManager.instance.createTransition_setStartPin ( this );
		}
	}

	protected override void unselect ()
	{
		if (MCEditorManager.instance.createTransition_Start_Pin() == this) {
			MCEditorManager.instance.createTransition_setStartPin ( null );
		}
	}

	#endregion
}
