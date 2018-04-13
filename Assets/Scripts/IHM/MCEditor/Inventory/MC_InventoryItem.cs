using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MC_InventoryItem : MonoBehaviour, IDragObjectActivator, IPointerEnterHandler, IPointerExitHandler
{

	/// <summary>
	/// The inventory that contains the item
	/// </summary>
	protected MC_Inventory inventory;
	/// <summary>
	/// The item that is represented by the Inventory Item
	/// </summary>
	protected System.Object item;

    protected GameObject toolTip;

	[SerializeField]
	UnityEngine.UI.Image itemImage;
	[SerializeField]
	UnityEngine.UI.Text textItem;
	[SerializeField]
	UnityEngine.UI.Image imageItem;

	[SerializeField]
	UnityEngine.UI.Text title;
	[SerializeField]
	Transform content;
	[SerializeField]
	MC_Inventory_Item_Info infoPrefab;

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

			ImageItem = MCEditor_ProxyIcon_Manager.instance.getItemImage (value);
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

	public UnityEngine.UI.Text TextItem {
		get {
			return textItem;
		}
		set {
			textItem = value;
		}
	}

	public Sprite ImageItem {
		get {
			return imageItem.sprite;
		}
		set {
			imageItem.sprite = value;

			// Choose the element which will be activated
			if (value == null) {
				imageItem.gameObject.SetActive (false);

				//image.type = UnityEngine.UI.Image.Type.Sliced;
				//image.preserveAspect = true;
			} else {
				textItem.gameObject.SetActive (false);
			}
		}
	}

	public string Title {
		get {
			return title.text;
		}
		set {
			title.text = value;
		}
	}

	public void AddContent( System.Type type ){
		AddContent( MC_Inventory_Item_Info.instantiate( type, infoPrefab, content.transform ) );
	}
	public void AddContent( string text ){
		AddContent( MC_Inventory_Item_Info.instantiate( text, infoPrefab, content.transform ) );
	}
	void AddContent( MC_Inventory_Item_Info info ){
		// show the gameobject that was hidden
		content.GetComponent<RectTransform> ().anchorMin = new Vector2 (0.6f,content.GetComponent<RectTransform> ().anchorMin.y);
		itemImage.transform.parent.gameObject.GetComponent<RectTransform> ().anchorMax = new Vector2 (0.6f,itemImage.transform.parent.gameObject.GetComponent<RectTransform> ().anchorMax.y);
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
		MCToolManager.instance.CancelInventory ();
		inventory.DropItem ( droppedObject, this );
        MCToolManager.instance.TemporarySave();
	}

    #endregion

    #region ToolTip

    public void OnPointerEnter(PointerEventData eventData)
    {
        MCEditor_DialogBoxManager.instance.instantiateToolTip(this.transform.position, item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }

    #endregion
}
