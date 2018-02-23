using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCEditor_DialogBox_Value : MCEditor_DialogBox {

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
		close ();
	}

	#endregion

	#region INSTANTIATE
	public static MCEditor_DialogBox_Value instantiate ( MCEditor_DialogBox prefab, Vector2 position, Transform parent = null ){
		return (MCEditor_DialogBox_Value) MCEditor_DialogBox.instantiate (prefab, position, parent);
	}
	#endregion
}
