using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MCEditor_InventoryPanel_Item : MonoBehaviour {

	[SerializeField]
	/// <summary>
	/// Button that activates the item
	/// </summary>
	Button itemButton;
	RectTransform itemButtonRect;

	[SerializeField]
	/// <summary>
	/// Container for item information
	/// </summary>
	GameObject itemContainer;
	RectTransform itemContainerRect;

	RectTransform thisRect;

	public Button ItemButton {
		get {
			return itemButton;
		}
	}

	public GameObject ItemContainer {
		get {
			return itemContainer;
		}
	}

	void Awake(){
		itemButtonRect = ItemButton.GetComponent<RectTransform>();
		itemContainerRect = ItemContainer.GetComponent<RectTransform>();
		thisRect = this.GetComponent<RectTransform> ();
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void activate( bool activated, float itemStart_RelativeToParentY, float itemButtonSize_RelativeToParent, float itemContainerSize_RelativeToParent, bool BottomUPDirection = false ){
		if (!activated) {
			// Deactivate : button fills the object

			if (BottomUPDirection) {
				thisRect.anchorMin = new Vector2 (0, itemStart_RelativeToParentY);
				thisRect.anchorMax = new Vector2 (1, itemStart_RelativeToParentY + itemButtonSize_RelativeToParent);
			} else {
				thisRect.anchorMin = new Vector2 (0, 1-(itemStart_RelativeToParentY + itemButtonSize_RelativeToParent));
				thisRect.anchorMax = new Vector2 (1, 1-itemStart_RelativeToParentY);
			}

			itemContainer.SetActive (false);
			itemButtonRect.anchorMin= new Vector2( 0, 0 );
			itemButtonRect.anchorMax= new Vector2( 1, 1 );
		} else {
			float itemTotalRelativeSize = itemButtonSize_RelativeToParent + itemContainerSize_RelativeToParent;

			if (BottomUPDirection) {
				thisRect.anchorMin = new Vector2 (0, itemStart_RelativeToParentY);
				thisRect.anchorMax = new Vector2 (1, itemStart_RelativeToParentY + itemTotalRelativeSize);
			} else {
				thisRect.anchorMin = new Vector2 (0, 1-(itemStart_RelativeToParentY + itemTotalRelativeSize));
				thisRect.anchorMax = new Vector2 (1, 1-(itemStart_RelativeToParentY));
			}

			itemContainer.SetActive (true);

			if (BottomUPDirection) {
				itemButtonRect.anchorMin = new Vector2 (0, 0);
				itemButtonRect.anchorMax = new Vector2 (1, itemButtonSize_RelativeToParent / itemTotalRelativeSize);
				itemContainerRect.anchorMin = new Vector2 (0, itemButtonSize_RelativeToParent / itemTotalRelativeSize);
				itemContainerRect.anchorMax = new Vector2 (1, 1);
			} else {
				itemButtonRect.anchorMin = new Vector2 (0, 1-(itemButtonSize_RelativeToParent / itemTotalRelativeSize));
				itemButtonRect.anchorMax = new Vector2 (1, 1);
				itemContainerRect.anchorMin = new Vector2 (0, 0);
				itemContainerRect.anchorMax = new Vector2 (1, 1-(itemButtonSize_RelativeToParent / itemTotalRelativeSize));
			}
		}
	}
}
