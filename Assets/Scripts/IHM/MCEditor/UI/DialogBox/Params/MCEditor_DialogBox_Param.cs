using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MCEditor_DialogBox_Param : MCEditor_DialogBox_Proxy {

	protected ProxyABParam paramProxy;

	// Use this for initialization
	protected void Start () {
		base.Start ();
		paramProxy = ((ProxyABParam)this.proxy);
	}
	
	// Update is called once per frame
	protected void Update () {
		base.Update ();
	}

	#region implemented abstract members of MCEditor_DialogBox_Proxy

	protected override void confirmProxy ()
	{
		confirmParam();
		paramProxy.SetProxyName ( paramProxy.GetProxyName() );
	}

	protected override void deactivateProxy ()
	{
		deactivateParam();
	}

	protected abstract void confirmParam ();
	protected abstract void deactivateParam ();
	#endregion

	#region INSTANTIATE
	public static MCEditor_DialogBox_Param instantiate ( ProxyABParam param, MCEditor_DialogBox_Param prefab, Vector3 position, Transform parent ){
		MCEditor_DialogBox_Param result = (MCEditor_DialogBox_Param)MCEditor_DialogBox_Proxy.instantiate (param ,prefab, position, parent);
		result.config ( param );

		return result;
	}

	private void config( ProxyABParam param ){
		this.paramProxy = param;

		configParam ();
	}

	/// <summary>
	/// Based on this.param, fill the initial values of the dialogBox
	/// </summary>
	protected abstract void configParam ();
	#endregion
}
