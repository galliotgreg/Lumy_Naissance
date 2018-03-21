using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockerDropdown_DialogBox : BlockerDropdown, UnityEngine.EventSystems.IPointerClickHandler{

	MCEditor_DialogBox dialogBox;

	#region PROPRETIES
	public MCEditor_DialogBox DialogBox {
		get {
			return dialogBox;
		}
		set {
			dialogBox = value;
		}
	}
	#endregion

	void Start(){
		onValueChanged.AddListener (valueChange);
	}

	void valueChange( int index ){
		dialogBox.Dropdown_Closed ();
	}

	public override void OnPointerClick (UnityEngine.EventSystems.PointerEventData eventData)
	{
		base.OnPointerClick (eventData);
		dialogBox.Dropdown_Opened ();
	}
}
