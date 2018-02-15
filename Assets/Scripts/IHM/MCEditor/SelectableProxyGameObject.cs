using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SelectableProxyGameObject : MonoBehaviour {

	[SerializeField]
	protected Color regularColor;
	[SerializeField]
	protected Color hoverColor;
	[SerializeField]
	protected Color selectedColor;

	[SerializeField]
	protected Renderer meshRenderer;

	bool selected = false;
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
	#endregion

	// Use this for initialization
	protected void Start () {

	}
	
	// Update is called once per frame
	protected void Update () {
		if (mouseOver) {
			setColor (hoverColor);
		} else {
			if (selected) {
				setColor (selectedColor);
			} else {
				setColor (regularColor);
			}
		}

		if (Input.GetMouseButton (0)) {
			if (mouseOver) {
				selectGameObject ();
			} else {
				unselectGameObject ();
			}
		}
	}

	void setColor( Color color ){
		if (meshRenderer != null) {
			meshRenderer.material.color = color;
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
