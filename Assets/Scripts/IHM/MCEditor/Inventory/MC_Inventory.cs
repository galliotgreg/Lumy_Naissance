using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MC_Inventory : MonoBehaviour {

	// Use this for initialization
	protected void Start () {
		
	}
	
	// Update is called once per frame
	protected void Update () {
		
	}

	[SerializeField]
	GameObject container;

	[SerializeField]
	DropArea dropArea;

	#region Load
	[SerializeField]
	protected GameObject itemPrefab;

	[SerializeField]
	GameObject proxyPrefab;

	protected void setItems (List<ABNode> items){
		foreach( ABNode item in items ){
			GameObject newItem = Instantiate (itemPrefab);
			MC_InventoryItem inventoryItem = newItem.GetComponent<MC_InventoryItem> ();
			inventoryItem.Item = item;
			inventoryItem.ProxyPrefab = proxyPrefab;
			inventoryItem.transform.SetParent ( container.transform );

			inventoryItem.Inventory = this;
			configItem ( inventoryItem );
		}
	}

	protected abstract void configItem ( MC_InventoryItem item );
	public abstract void configProxyItem ( GameObject proxy, MC_InventoryItem item );
	#endregion

	#region Drop
	public void DropItem ( GameObject proxy, MC_InventoryItem item ){
		if (dropArea.CanDrop) {
			Drop ();
		} else {
			Destroy (proxy.gameObject);
		}
	}

	protected abstract void Drop( GameObject proxy, MC_InventoryItem item );
	#endregion
}
