using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCEditor_DialogBoxManager : MonoBehaviour {

	#region SINGLETON
	// The static instance of the Singleton for external access
	public static MCEditor_DialogBoxManager instance = null;

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
	MCEditor_DialogBox_Param valuePrefab;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public MCEditor_DialogBox_Param instantiateValue( ProxyABParam param, Vector2 position ){
		Vector3 pos3D = new Vector3 ( position.x, position.y, -4 );
		return MCEditor_DialogBox_Param.instantiate( param, valuePrefab, pos3D, this.transform);
	}
}
