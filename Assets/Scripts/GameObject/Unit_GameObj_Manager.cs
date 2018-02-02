using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_GameObj_Manager : MonoBehaviour {

	/// <summary>
	/// The static instance of the Singleton for external access
	/// </summary>
	public static Unit_GameObj_Manager instance = null;

	/// <summary>
	/// Enforce Singleton properties
	/// </summary>
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

	Dictionary<PlayerAuthority,HomeScript> homes = new Dictionary<PlayerAuthority, HomeScript>();
	List<ResourceScript> resources;
	List<TraceGameObject> traces;

	#region Properties
	public List<HomeScript> Homes {
		set {
			homes = new Dictionary<PlayerAuthority, HomeScript>();

			if( value.Count > 0 ){
				homes.Add( value[0].Authority, value[0] );
				if( value.Count > 1 ){
					homes.Add( value[1].Authority, value[1] );
				}
			}
		}
	}

	public List<ResourceScript> Resources {
		get {
			return resources;
		}
	}

	public List<TraceGameObject> Traces {
		get {
			return traces;
		}
	}
	#endregion

	#region Actions
	public void hitUnit( float damage ){
		//TODO
		throw new System.NotImplementedException();
	}
	#endregion

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
