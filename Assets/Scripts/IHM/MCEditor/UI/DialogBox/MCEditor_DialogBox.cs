using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class MCEditor_DialogBox : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	bool hover = false;

	bool enableCloseOnOutClick = true;

	#region PROPERTIES
	protected bool EnableCloseOnOutClick {
		get {
			return enableCloseOnOutClick;
		}
		set {
			enableCloseOnOutClick = value;
		}
	}
	private bool DropdownOpened{
		set{
			this.EnableCloseOnOutClick = !value;
		}
	}
	#endregion

	// Use this for initialization
	protected void Start () {
		
	}
	
	// Update is called once per frame
	bool clickReleased = false;
	protected void Update () {
		if (clickReleased) {
			if (Input.GetMouseButtonDown (0)) {
				if (!hover){
					if (EnableCloseOnOutClick) {
						// Deactivate
						close ();
					} else {
						EnableCloseOnOutClick = true;
					}
				}
			}
		} else {
			if (!(Input.GetMouseButtonDown (0))) {
				clickReleased = true;
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
	protected static MCEditor_DialogBox instantiate ( MCEditor_DialogBox prefab, Vector3 position, Transform parent ){
		MCEditor_DialogBox result = Instantiate<MCEditor_DialogBox>(prefab, parent);
		result.transform.position = position;
		return result;
	}
	#endregion

	#region Dropdown
	public void Dropdown_Opened(){
		DropdownOpened = true;
	}
	public void Dropdown_Closed(){
		DropdownOpened = false;
	}
	#endregion
}
