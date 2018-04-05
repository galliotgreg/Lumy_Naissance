using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IsolatedSelectableProxyGameObject : MonoBehaviour {

	[SerializeField]
	protected Color regularColor;
	[SerializeField]
	protected Color hoverColor;
	[SerializeField]
	protected Color selectedColor;

	[SerializeField]
	protected DropArea selectZone;

	[SerializeField]
	protected Renderer colorRenderer;

	bool selected = false;
	bool selecting = false;
	bool mouseOver = false;

	#region PROPERTIES
	public bool Selected {
		get {
			return selected;
		}
	}

	public bool MouseOver {
		get {
			return mouseOver;
		}
	}

	public DropArea SelectZone {
		get {
			return selectZone;
		}
		set {
			selectZone = value;
		}
	}

	#endregion

	// Use this for initialization
	protected void Start () {

	}
	
	// Update is called once per frame
	protected void Update () {
		/**
		 * Change color and select when the mouse is on the select zone
		 */

		// Change color on hover
		if (mouseOver && (!selectZone || selectZone.IsHover)) {
			setColor (hoverColor);
		} else {
			if (selected) {
				setColor (selectedColor);
			} else {
				setColor (regularColor);
			}
		}

		// selecting on click
		if (Input.GetMouseButton (0)) {
			if (mouseOver && (!selectZone || selectZone.IsHover)) {
				selecting = true;
			} else {
				unselectGameObject ();
			}
		} else {
			if (selecting && mouseOver && (!selectZone || selectZone.IsHover)) {
				selectGameObject ();
			}
			selecting = false;
		}
	}

	void setColor( Color color ){
		if (colorRenderer != null) {
			colorRenderer.material.color = color;
		}
	}

	protected void selectGameObject(){
		select ();
		selected = true;
	}

	protected void unselectGameObject(){
		unselect ();
		selected = false;
	}

	protected void OnMouseEnter(){
		mouseOver = true;
	}

	protected void OnMouseExit(){
		mouseOver = false;
	}

	protected abstract void select();
	protected abstract void unselect();
}
