using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_InventoryItem : MonoBehaviour, IDragObjectActivator {

	/// <summary>
	/// The inventory that contains the item
	/// </summary>
	protected MC_Inventory inventory;
	/// <summary>
	/// The item that is represented by the Inventory Item
	/// </summary>
	protected System.Object item;

	[SerializeField]
	UnityEngine.UI.Text text;

	// Use this for initialization
	protected void Start () {
		RectTransform rect = GetComponent<RectTransform>();
		rect.localScale = new Vector3 (1, 1, 1);
	}

	// Update is called once per frame
	protected void Update () {
	}

	public System.Object Item {
		get {
			return item;
		}
		set {
			item = value;
		}
	}

	public MC_Inventory Inventory {
		get {
			return inventory;
		}
		set {
			inventory = value;
		}
	}


	public UnityEngine.UI.Text Text {
		get {
			return text;
		}
		set {
			text = value;
		}
	}

	[SerializeField]
	DragObject dragObjectComponent;

	public void activateClick(){
		GameObject proxy = inventory.instantiateProxy (this);
		MCToolManager.instance.Inventory ();

		dragObjectComponent.startDrag (proxy, this);
	}

	#region IDragObjectActivator implementation

	public void endDrag (GameObject droppedObject)
	{
		inventory.DropItem ( droppedObject, this );
	}

	#endregion
}
