using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_Debugger_Factory : MonoBehaviour {

	#region SINGLETON
	/// <summary>
	/// The static instance of the Singleton for external access
	/// </summary>
	public static MC_Debugger_Factory instance = null;

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

	//Templates & Prefabs
	[SerializeField]
	private Debugger_Node nodePrefab;

	void Start(){}
	void Update(){}

	#region PROPERTIES
	public Debugger_Node NodePrefab {
		get {
			return nodePrefab;
		}
	}
	#endregion

	#region INSTANTIATE
	public Debugger_Node instantiateState( ABState state, bool init, Transform parent ){
		return Debugger_Node.instantiate (nodePrefab, state, init, parent);
	}
	#endregion
}
