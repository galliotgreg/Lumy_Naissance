using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class MCEditor_DialogBox : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	[SerializeField]
	Canvas canvas;

	bool hover = false;

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
		if (Input.GetMouseButton (0)) {
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

	public void OnPointerEnter (PointerEventData eventData)
	{
		hover = true;
	}

	#endregion

	#region IPointerExitHandler implementation

	public void OnPointerExit (PointerEventData eventData)
	{
		hover = false;
	}

	#endregion

	#region INSTANTIATE
	protected static MCEditor_DialogBox instantiate ( MCEditor_DialogBox prefab, Vector2 position, Transform parent = null ){
		MCEditor_DialogBox result = Instantiate<MCEditor_DialogBox> (prefab);
		result.transform.position = position;

		if (parent != null) {
			result.transform.parent = parent;
		}

		return result;
	}
	#endregion
}
