using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 GameObject de branchement de transition entre deux états
 */  
public class Pin : DragSelectableProxyGameObject {

	public enum PinType{
		Condition,		// Transition : condition
		TransitionIn,	// Action/State : transition in
		TransitionOut,	// State : transition out
		ActionParam,	// Action : param (gateOperator)
		OperatorIn,		// Operator : param
		OperatorOut,	// Operator : result
		Value			// Constant/Reference
	};

	[SerializeField]
	PinType pin_Type;

	[SerializeField]
	/// <summary>
	/// Prefab that implements a Transition
	/// </summary>
	GameObject transitionPrefab;

	#region PROPERTIES
	public PinType Pin_Type {
		get {
			return pin_Type;
		}
		set {
			pin_Type = value;
		}
	}
	#endregion

    // Use this for initialization
    void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		base.Update ();

		handleSelectedState ();
	}

	#region implemented abstract members of SelectableProxyGameObject
	protected override void select ()
	{
		if (MCEditorManager.instance.Transition_Pin_Start != null ) {
			MCEditorManager.instance.createTransition_setEndPin (this);
		} else {
			MCEditorManager.instance.createTransition_setStartPin ( this );
		}
	}

	protected override void unselect ()
	{
		if (MCEditorManager.instance.Transition_Pin_Start == this) {
			MCEditorManager.instance.createTransition_setStartPin ( null );
		}
	}

	ProxyABTransition auxTransition;
	bool selectNow = false;

	protected void handleSelectedState(){
		if (Selected) {
			if (!selectNow) {
				auxTransition = Instantiate (transitionPrefab).GetComponent<ProxyABTransition> ();
				auxTransition.Collider.enabled = false;

				selectNow = true;
			}

			if (auxTransition != null) {
				// Move
				auxTransition.LineRenderer.SetPosition( 0, this.transform.position );
				Vector3 mouse = Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
				//mouse.z = this.transform.position.z;

				auxTransition.LineRenderer.SetPosition( 1, mouse );
			}
		} else {
			selectNow = false;
			if (auxTransition != null) {
				Destroy (auxTransition.gameObject);
			}
		}
	}
	#endregion
}
