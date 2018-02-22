using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MCEditor_InventoryPanel : MonoBehaviour {

	[SerializeField]
	List<MCEditor_InventoryPanel_Item> items;

	float itemButton_RelativeHeight = 0.05f;
	float itemContent_RelativeHeight = 0.95f;

	int selectedIndex = 0;
	RectTransform thisRect;

	void Awake(){
		// Updating itemContent_RelativeHeight according to items
		if (items != null && items.Count > 0) {
			itemContent_RelativeHeight = 1f-(items.Count*itemButton_RelativeHeight);
		}

		thisRect = this.GetComponent<RectTransform> ();
	}

	// Use this for initialization
	void Start () {
		adjustItems ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void selectItem( MCEditor_InventoryPanel_Item item ){
		int itemIndex = items.IndexOf (item);

		if (selectedIndex == itemIndex) {
			selectedIndex = -1;
		} else {
			selectedIndex = itemIndex;
		}

		adjustItems ();
	}

	void adjustItems(){
		for (int i=0; i< items.Count; i++) {
			int itemIndex = i;
			MCEditor_InventoryPanel_Item item = items[itemIndex];
			RectTransform itemRect = item.gameObject.GetComponent<RectTransform> ();

			if (isSelected (item)) {
				item.activate( true, itemIndex*itemButton_RelativeHeight, itemButton_RelativeHeight, itemContent_RelativeHeight );
			} else {
				if (isSelectedBefore(item)) {
					item.activate( false, itemIndex*itemButton_RelativeHeight+itemContent_RelativeHeight, itemButton_RelativeHeight, itemContent_RelativeHeight );
				}
				else{
					item.activate( false, itemIndex*itemButton_RelativeHeight, itemButton_RelativeHeight, itemContent_RelativeHeight );
				}
			}
		}
	}

	bool isSelected( MCEditor_InventoryPanel_Item item ){
		return selectedIndex != -1 && selectedIndex == items.IndexOf ( item );
	}
	bool isSelectedBefore( MCEditor_InventoryPanel_Item item ){
		return selectedIndex != -1 && selectedIndex < items.IndexOf ( item );
	}
}
