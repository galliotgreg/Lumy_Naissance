using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour {
	
	bool dragging = false;
	GameObject objectToDrag;
	IDragObjectActivator dragActivator;

	Camera trackingCamera;

	#region PROPERTIES
	public bool Dragging {
		get {
			return dragging;
		}
	}
	#endregion

	// Use this for initialization
	protected void Start () {
		trackingCamera = ( MCEditor_BringToFront_Camera.CanvasCamera != null ? MCEditor_BringToFront_Camera.CanvasCamera : NavigationManager.instance.GetCurrentCamera());
	}

	// Update is called once per frame
	protected void Update () {
		if (dragging) {
			// break dragging
			if (Input.GetMouseButtonDown (1) || Input.GetMouseButtonDown (2)) {
				mouseUp ();
			}
			// Drag
			updatePosition ( objectToDrag );
		}
	}

	private void updatePosition (GameObject _objectToDrag){
		if (_objectToDrag != null) {
			Vector3 mouse = trackingCamera.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y, -trackingCamera.transform.position.z));

			// Bring Object to front
			mouse.z = trackingCamera.transform.position.z+2f;

			_objectToDrag.transform.position = mouse;
		}
	}
		
	float startZposition;
	public void startDrag( GameObject _objectToDrag, IDragObjectActivator activator ){
		dragging = true;
		startZposition = _objectToDrag.transform.position.z;
		this.objectToDrag = _objectToDrag;
		this.dragActivator = activator;
	}
	public void mouseUp(){
		dragging = false;
		if( this.objectToDrag != null && this.dragActivator != null ){
			this.objectToDrag.transform.position = new Vector3( this.objectToDrag.transform.position.x, this.objectToDrag.transform.position.y, startZposition );
			dragActivator.endDrag ( this.objectToDrag );
		}
		this.objectToDrag = null;
		this.dragActivator = null;
	}
}
