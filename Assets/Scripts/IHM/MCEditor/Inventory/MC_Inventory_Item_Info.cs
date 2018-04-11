using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_Inventory_Item_Info : MonoBehaviour {

	[SerializeField]
	UnityEngine.UI.Image infoImage;
	[SerializeField]
	UnityEngine.UI.Text infoText;

	public System.Type InfoType{
		set{
			infoText.text = MCEditor_Proxy.typeToString ( value );
			//infoText.color = Color.black;
			infoImage.color = PinColor.GetColorPinFromType( value );
			infoImage.color = new Color (infoImage.color.r, infoImage.color.g, infoImage.color.b, 0.4f);
		}
	}

	public string Text{
		set{
			infoText.text = value;
			infoImage.color = new Color(0,0,0,0);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static MC_Inventory_Item_Info instantiate( System.Type type, MC_Inventory_Item_Info prefab, Transform parent ){
		MC_Inventory_Item_Info result = instantiate( prefab, parent );
		result.InfoType = type;
		return result;
	}
	public static MC_Inventory_Item_Info instantiate( string text, MC_Inventory_Item_Info prefab, Transform parent ){
		MC_Inventory_Item_Info result = instantiate( prefab, parent );
		result.Text = text;
		return result;
	}
	private static MC_Inventory_Item_Info instantiate( MC_Inventory_Item_Info prefab, Transform parent ){
		return Instantiate<MC_Inventory_Item_Info> (prefab, parent);
	}
}
