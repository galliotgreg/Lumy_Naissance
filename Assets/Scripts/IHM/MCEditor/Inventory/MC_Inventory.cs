﻿using System.Collections;
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
			inventoryItem.transform.SetParent ( container.transform );

			configItem ( inventoryItem );
		}
	}

	protected abstract void configItem ( MC_InventoryItem item );
	#endregion
}
