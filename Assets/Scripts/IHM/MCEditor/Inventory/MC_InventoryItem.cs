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
	UnityEngine.UI.Text text;
	[SerializeField]
	UnityEngine.UI.Image image;

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

			Image = MCEditor_ProxyIcon_Manager.instance.getItemImage (value);
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

	public Sprite Image {
		get {
			return image.sprite;
		}
		set {
			image.sprite = value;

			// Choose the element which will be activated
			if (value == null) {
				image.gameObject.SetActive (false);

				//image.type = UnityEngine.UI.Image.Type.Sliced;
				//image.preserveAspect = true;
			} else {
				text.gameObject.SetActive (false);
			}
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
		MCToolManager.instance.CancelInventory ();
		inventory.DropItem ( droppedObject, this );
        MCToolManager.instance.TemporarySave();
	}

    #endregion

    #region ToolTip

    public void OnPointerEnter(PointerEventData eventData)
    {
        toolTip = MCEditor_DialogBoxManager.instance.instantiateToolTip(this.transform.position, item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Destroy(toolTip.gameObject);
    }

    #endregion
}
