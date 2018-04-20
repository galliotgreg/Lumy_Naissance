using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_Proxy_Image_Factory : MonoBehaviour {

	#region SINGLETON
	/// <summary>
	/// The static instance of the Singleton for external access
	/// </summary>
	public static MC_Proxy_Image_Factory instance = null;

	void Awake()
	{
		//Check if instance already exists and set it to this if not
		if (instance == null)
		{
			instance = this;
		}

		//Enforce the unicity of the Singleton
		else if (instance != this)
		{
			Destroy(gameObject);
		}
	}
	#endregion

	[SerializeField]
	private ProxyABState statePrefab;
	[SerializeField]
	private ProxyABAction actionPrefab;
	[SerializeField]
	private MC_Proxy_Debugger_Image imagePrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public MC_Proxy_Debugger_Image instantiateStateImage( ABState state, bool init, Transform parent ){
		ProxyABState proxy = ProxyABState.instantiateSimple (state, init, this.transform, statePrefab);
		MC_Proxy_Debugger_Image result = Instantiate<MC_Proxy_Debugger_Image> (imagePrefab, parent);
		result.setCam ( proxy );
		return result;
	}
	public MC_Proxy_Debugger_Image instantiateActionImage( ABState state, Transform parent ){
		ProxyABAction proxy = ProxyABAction.instantiateSimple (state, this.transform, actionPrefab);
		MC_Proxy_Debugger_Image result = Instantiate<MC_Proxy_Debugger_Image> (imagePrefab, parent);
		result.setCam ( proxy );
		return result;
	}
}
