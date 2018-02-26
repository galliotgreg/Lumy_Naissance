using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class MCEditor_DialogBox : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	[SerializeField]
	Canvas canvas;

	// TODO :  ATTENTION : the hover flag is used to detect a click that deactive the dialog. It must start as FALSE, but it is considering that the click is still valid and destroies the dialog
	// TODO :  ATTENTION : the dialog is not handling the mouse events
	bool hover = true;

	public Vector2 Size {
		get {
			return canvas.GetComponent<RectTransform>().rect.size;
		}
	}

	// Use this for initialization
	protected void Start () {
		
	}
	
	// Update is called once per frame
	protected void Update () {
		if (Input.GetMouseButtonDown (0)) {
			if( !hover ){
				// Deactivate
				close();
			}
		}
	}

	protected void close (){
		this.deactivate();
		Destroy (this.gameObject);
	}

	protected abstract void deactivate ();
	public abstract void confirm ();

	#region IPointerEnterHandler implementation

	public void OnMouseEnter(){
		Debug.Log ("in");
		hover = true;
	}

	public void OnPointerEnter (PointerEventData eventData)
	{
		Debug.Log ("in i");
		hover = true;
	}

	#endregion

	#region IPointerExitHandler implementation

	public void OnMouseExit(){
		Debug.Log ("out");
		hover = false;
	}

	public void OnPointerExit (PointerEventData eventData)
	{
		Debug.Log ("out i");
		hover = false;
	}

	#endregion

	#region INSTANTIATE
	protected static MCEditor_DialogBox instantiate ( MCEditor_DialogBox prefab, Vector3 position, Transform parent = null ){
		MCEditor_DialogBox result = Instantiate<MCEditor_DialogBox>(prefab);
		result.transform.position = position;

		if (parent != null) {
			result.transform.parent = parent;
		}
		return result.GetComponentInChildren<MCEditor_DialogBox>();
	}
	#endregion
}
