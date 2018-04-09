using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCEditor_ProxyIcon_Manager : MonoBehaviour {

	#region SINGLETON
	// The static instance of the Singleton for external access
	public static MCEditor_ProxyIcon_Manager instance = null;

	// Enforce Singleton properties
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
	private string iconsFolder = "MCEditor_Proxy_Icons/";

	public Sprite getItemImage( System.Object item ){
		string imageFileName = "";

		if (item is ABState) {
			ABState state = (ABState)item;

			if( state.Action != null ){
				imageFileName = state.Action.Type.ToString ();
			}
		} else if (item is IABOperator) {
			string prefix = "Operator/Operator_";
			IABOperator operat = (IABOperator)item;

			imageFileName = operat.ViewName;

			if(imageFileName.Contains( "Not" )){
				imageFileName = prefix + "Not";
				return getItemImage ( imageFileName );
			}
			imageFileName = prefix+operat.OpCategory.ToString();
		} else if (item is IABParam) {
			IABParam param = (IABParam)item;
			imageFileName = param.Identifier;
		}

		return null;
		//return getItemImage ( imageFileName );
	}

	Sprite getItemImage( string itemName ){
		try{
			return Resources.Load<Sprite> ( iconsFolder+itemName );
		}catch( System.Exception ex ){
			return null;
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
