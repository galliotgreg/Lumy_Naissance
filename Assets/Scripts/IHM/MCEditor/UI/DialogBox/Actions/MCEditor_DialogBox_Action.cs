using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MCEditor_DialogBox_Action : MCEditor_DialogBox_Proxy {

	protected ProxyABAction actionProxy;

	// Use this for initialization
	void Start () {
		base.Start ();

		actionProxy = (ProxyABAction)this.proxy;
	}

	// Update is called once per frame
	void Update () {
		base.Update ();
	}

	#region implemented abstract members of MCEditor_DialogBox_Proxy

	protected override void confirmProxy ()
	{
		confirmAction();
		actionProxy.SetProxyName ( actionProxy.GetProxyName() );
	}

	protected override void deactivateProxy ()
	{
		deactivateAction();
	}

	protected abstract void confirmAction ();
	protected abstract void deactivateAction ();
	#endregion

	#region INSTANTIATE
	public static MCEditor_DialogBox_Action instantiate ( ProxyABAction action, MCEditor_DialogBox_Action prefab, Vector3 position, Transform parent ){
		MCEditor_DialogBox_Action result = (MCEditor_DialogBox_Action)MCEditor_DialogBox_Proxy.instantiate (action, prefab, position, parent);
		result.config ( action );

		return result;
	}

	private void config( ProxyABAction action ){
		this.actionProxy = action;

		configAction ();
	}

	/// <summary>
	/// Based on this.action, fill the initial values of the dialogBox
	/// </summary>
	protected abstract void configAction ();
	#endregion
}