using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_Inventory_NodeItem : MC_InventoryItem {

	public enum NodeItemType{
		State, Action, Operator, Param
	};

	NodeItemType itemType;

	/// <summary>
	/// The image used to show the node
	/// </summary>
	[SerializeField]
	UnityEngine.UI.Image NodeImage;

	/// <summary>
	/// The sprite loaded for a State Image
	/// </summary>
	[SerializeField]
	protected Sprite StateImage;
	/// <summary>
	/// The sprite loaded for a Action Image
	/// </summary>
	[SerializeField]
	protected Sprite ActionImage;
	/// <summary>
	/// The sprite loaded for a Operator Image
	/// </summary>
	[SerializeField]
	protected Sprite OperatorImage;
	/// <summary>
	/// The sprite loaded for a Param Image
	/// </summary>
	[SerializeField]
	protected Sprite ParamImage;

	public NodeItemType ItemType {
		get {
			return itemType;
		}
		set {
			itemType = value;
			setImage ();
		}
	}

	void setImage(){
		switch( itemType ){
		case NodeItemType.State:
			NodeImage.sprite = StateImage;
			break;
		case NodeItemType.Action:
			NodeImage.sprite = ActionImage;
			break;
		case NodeItemType.Operator:
			NodeImage.sprite = OperatorImage;
			break;
		case NodeItemType.Param:
			NodeImage.sprite = ParamImage;
			break;
		}
	}

	// Use this for initialization
	void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
	}
}
