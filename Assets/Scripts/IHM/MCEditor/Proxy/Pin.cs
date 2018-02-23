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
		Param			// Constant/Reference
	};

	[SerializeField]
	PinType pin_Type;

	[SerializeField]
	/// <summary>
	/// Prefab that implements a Transition
	/// </summary>
	GameObject transitionPrefab;

	Camera trackingCamera;

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
		trackingCamera = ( MCEditor_BringToFront_Camera.CanvasCamera != null ? MCEditor_BringToFront_Camera.CanvasCamera : Camera.main );
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
				Vector3 mouse = trackingCamera.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y, -trackingCamera.transform.position.z));
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

	#region INSTANTIATE
	public static Pin instantiate( Pin.PinType pinType, Vector3 position, Transform parent ){
		Pin result = Instantiate<Pin> (MCEditor_Proxy_Factory.instance.PinPrefab, parent);
		result.Pin_Type = pinType;
		result.transform.position = position;

		return result;
	}

	// Pin : Action : fixed number of pins
	public static Vector3 calculatePinPosition( Pin.PinType pinType, ProxyABAction parent ){
		float radius = parent.transform.localScale.y / 2;
		if (pinType == Pin.PinType.TransitionIn) {
			// Income
			return new Vector3 (parent.transform.position.x, parent.transform.position.y + radius, parent.transform.position.z);
		} else {
			// Param
			// +1 income
			int childCount = parent.AllPins.Count + 1;
			int totalPins = parent.AbState.Action.Parameters.Length + 1;
			float angle = childCount * (2 * Mathf.PI) / (float)totalPins;
			return new Vector3 (
				parent.transform.position.x + (radius * Mathf.Sin ( angle ) ),
				parent.transform.position.y + (radius * Mathf.Cos ( angle ) ),
				parent.transform.position.z
			);
		}
	}

	// Pin : State : Outcome pins is variant
	public static Vector3 calculatePinPosition( ProxyABState parent ){
		float radius = parent.transform.localScale.y / 2;
		return new Vector3 (parent.transform.position.x, parent.transform.position.y + radius, parent.transform.position.z);
	}

	public static Vector3 calculatePinPosition( ABState state, GameObject stateParent, bool transitionOut, int curTransition = 0 ){
		float radius = stateParent.transform.localScale.y / 2;
		if (transitionOut) {
			return new Vector3 (
				stateParent.transform.position.x + (radius * Mathf.Cos (curTransition * (2 * Mathf.PI) / Mathf.Max (1, state.Outcomes.Count))),
				stateParent.transform.position.y + (radius * Mathf.Sin (curTransition * (2 * Mathf.PI) / Mathf.Max (1, state.Outcomes.Count))),
				stateParent.transform.position.z
			);
		} else {
			return new Vector3(
				stateParent.transform.position.x + (radius * Mathf.Cos (curTransition * (2 * Mathf.PI) / Mathf.Max (1, state.Outcomes.Count))),
				stateParent.transform.position.y + (radius * Mathf.Sin (curTransition * (2 * Mathf.PI) / Mathf.Max (1, state.Outcomes.Count))),
				stateParent.transform.position.z
			);
		}
	}

	public static Vector3 calculatePinPosition( ProxyABTransition parent ){
		return parent.transform.position;
	}

	public static Vector3 calculatePinPosition( ProxyABOperator parent ){
		int childCount = parent.transform.childCount;
		float radius = parent.transform.localScale.y / 2;
		return new Vector3(parent.transform.position.x + (radius * Mathf.Cos(childCount * (2 * Mathf.PI) / 4)),
			parent.transform.position.y + (radius* Mathf.Sin(childCount * (2 * Mathf.PI) / 4)),
			parent.transform.position.z
		);
	}

	// Pin : Param : fixed number of pins
	public static Vector3 calculatePinPosition( ProxyABParam parent ){
		float radius = parent.transform.localScale.y / 2;
		return new Vector3 (parent.transform.position.x, parent.transform.position.y - radius, parent.transform.position.z);
	}
	#endregion
}
