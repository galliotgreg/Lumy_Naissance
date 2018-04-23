using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MCEditor_DialogBox_State : MCEditor_DialogBox_Proxy {

	protected ProxyABState stateProxy;

	// Use this for initialization
	void Start () {
		base.Start ();

		stateProxy = (ProxyABState)this.proxy;
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
	}

	#region implemented abstract members of MCEditor_DialogBox_Proxy

	protected override void confirmProxy ()
	{
		confirmState();
        if (!ValidateChars(stateProxy.GetProxyName()))
        {
            return;
        }
		stateProxy.SetProxyName ( stateProxy.GetProxyName() );
	}

	protected override void deactivateProxy ()
	{
		deactivateState();
	}

	protected abstract void confirmState ();
	protected abstract void deactivateState ();
	#endregion

	#region INSTANTIATE
	public static MCEditor_DialogBox_State instantiate ( ProxyABState state, MCEditor_DialogBox_State prefab, Vector3 position, Transform parent ){
		MCEditor_DialogBox_State result = (MCEditor_DialogBox_State)MCEditor_DialogBox_Proxy.instantiate (state, prefab, position, parent);
		result.config ( state );

		return result;
	}

	private void config( ProxyABState state ){
		this.stateProxy = state;

		configState ();
	}

	/// <summary>
	/// Based on this.param, fill the initial values of the dialogBox
	/// </summary>
	protected abstract void configState ();
	#endregion
}
