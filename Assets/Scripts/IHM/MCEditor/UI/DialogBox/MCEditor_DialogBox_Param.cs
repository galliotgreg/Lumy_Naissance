using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCEditor_DialogBox_Param : MCEditor_DialogBox {

	ProxyABParam param;

	[SerializeField]
	UnityEngine.UI.Text valueText;

	[SerializeField]
	UnityEngine.UI.Text title;

	#region PROPERTIES
	public ProxyABParam Param {
		get {
			return param;
		}
		set {
			param = value;
		}
	}

	public string Title{
		set{
			title.text = value;
		}
	}
	#endregion

	// Use this for initialization
	void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
	}

	#region implemented abstract members of MCEditor_DialogBox
	protected override void deactivate (){
	}

	public override void confirm ()
	{
		param.Value = valueText.text;
		close ();
	}

	#endregion

	#region INSTANTIATE
	public static MCEditor_DialogBox_Param instantiate ( ProxyABParam param, MCEditor_DialogBox_Param prefab, Vector3 position, Transform parent = null ){
		MCEditor_DialogBox_Param result = (MCEditor_DialogBox_Param)MCEditor_DialogBox.instantiate (prefab, position, parent);
		result.Param = param;
		result.Title = param.Name;
		return result;
	}
	#endregion
}
