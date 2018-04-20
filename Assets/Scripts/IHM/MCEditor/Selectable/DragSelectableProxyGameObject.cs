using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class DragSelectableProxyGameObject : MonoBehaviour {
	[SerializeField]
	protected Color regularColor;
	[SerializeField]
	protected Color hoverColor;
	[SerializeField]
	protected Color selectedColor;

	protected static DragSelectableProxyGameObject firstSelected;
	protected static DragSelectableProxyGameObject secondSelected;

	[SerializeField]
	protected Renderer colorRenderer;

    protected string toolTipText = "";

	bool selected = false;
	bool clicked = false;
	bool mouseOver = false;

	float lastClick = -1;

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
		if (mouseOver && MCEditor_Proxy_Factory.instance.InteractableZone.IsHover) {
			setColor (hoverColor);            
		} else {            
            if (selected) {
				setColor (selectedColor);
			} else {
				setColor (regularColor);
			}
		}

		if (!Input.GetMouseButton (0)) {
			// mouse released
			if (clicked && mouseOver) {
				if (firstSelected != null && firstSelected != this) {
					secondSelected = this;

					selectGameObject ();

					firstSelected.select ();
					secondSelected.select ();
				}

				// double click
				if (lastClick > 0 && Time.time - lastClick < MCEditor_Proxy.doubleClickIntervalMseconds / 1000f) {
					doubleClick ();
				}
				lastClick = Time.time;
			}

			// clear
			if (!clicked) {
				if (firstSelected != null) {
					firstSelected.unselectGameObject ();
				}
				if (secondSelected != null) {
					secondSelected.unselectGameObject ();
				}
			}

			clicked = false;
		} else {
			if( MCEditor_Proxy_Factory.instance.InteractableZone.IsHover ){
				clicked = true;
			}
		}
	}        

	void setColor( Color color ){
		if (colorRenderer != null) {
			colorRenderer.material.color = color;
		} else
        {
            MeshRenderer internRendering = transform.Find("InternSphere").gameObject.GetComponent<MeshRenderer>();
            MeshRenderer externRendering = transform.Find("ExternSphere").gameObject.GetComponent<MeshRenderer>();
            internRendering.material.SetColor("_Color", color);
            externRendering.material.SetColor("_node_4083", color);
        }
	}

    public Color GetColor()
    {
        return regularColor;
    }

	protected void selectGameObject(){
		selected = true;
	}

	protected void unselectGameObject(){
		selected = false;

		if (firstSelected == this) {
			firstSelected.unselect ();
			firstSelected = null;
		}
		if (secondSelected == this) {
			secondSelected.unselect ();
			secondSelected = null;
		}
	}

	protected void OnMouseEnter(){
		mouseOver = true;
        ShowToolTip();
    }

	protected void OnMouseExit(){
		mouseOver = false;
        HideToolTip();
    }

    protected void OnMouseDown(){
		if (MCEditor_Proxy_Factory.instance.InteractableZone.IsHover) {
			firstSelected = this;

			selectGameObject ();
		}
	}

   protected void ShowToolTip()
    {
        MCEditorManager.instance.ShowToolTip(toolTipText, this.transform.position);
    }

    protected void HideToolTip()
    {
        MCEditorManager.instance.ShowToolTip("", this.transform.position);
    }

    protected abstract void select();
	protected abstract void doubleClick();
	protected abstract void unselect();
}
