using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MCEditor_DialogBox_Param : MCEditor_DialogBox {

	protected ProxyABParam param;

	[SerializeField]
	UnityEngine.UI.Text title;

	#region PROPERTIES
	public ProxyABParam Param {
		get {
			return param;
		}
		protected set {
			param = value;
		}
	}

	protected string Title{
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
		confirmParam ();
		this.param.setProxyName ( MCEditorManager.GetParamValue( (ABNode) param.AbParam ) );
		close ();
	}

	protected abstract void confirmParam ();
	protected abstract void cancelParam ();
	#endregion

	#region INSTANTIATE
	public static MCEditor_DialogBox_Param instantiate ( ProxyABParam param, MCEditor_DialogBox_Param prefab, Vector3 position, Transform parent ){
		MCEditor_DialogBox_Param result = (MCEditor_DialogBox_Param)MCEditor_DialogBox.instantiate (prefab, position, parent);
		result.config ( param );

		return result;
	}

	public void config( ProxyABParam param ){
		this.Param = param;
		this.Title = param.Name;

		configParam ();
	}

	/// <summary>
	/// Based on this.param, fill the initial values of the dialogBox
	/// </summary>
	protected abstract void configParam ();
	#endregion
}
