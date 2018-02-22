using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour {
	
	bool dragging = false;
	GameObject objectToDrag;
	IDragObjectActivator dragActivator;

	#region PROPERTIES
	public bool Dragging {
		get {
			return dragging;
		}
	}
	#endregion

	// Use this for initialization
	protected void Start () {

	}

	// Update is called once per frame
	protected void Update () {
		if (dragging) {
			updatePosition ( objectToDrag );
		}
	}

	private void updatePosition (GameObject _objectToDrag){
		if (_objectToDrag != null) {
			Camera cam = GameObject.Find ("Main Camera").GetComponent<Camera> ();
			Vector3 mouse = cam.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y, -cam.transform.position.z));
			//mouse.z = _objectToDrag.transform.position.z;

			_objectToDrag.transform.position = mouse;
		}
	}

	public void startDrag( GameObject _objectToDrag, IDragObjectActivator activator ){
		dragging = true;
		this.objectToDrag = _objectToDrag;
		this.dragActivator = activator;
	}
	public void mouseUp(){
		dragging = false;
		dragActivator.endDrag ( this.objectToDrag );
		this.objectToDrag = null;
		this.dragActivator = null;
	}
}
