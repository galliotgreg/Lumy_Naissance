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
	public float strikeUnit( AgentEntity target, float damage ){
		float vitalityResult = Mathf.Max( target.Context.Model.Vitality - damage, 0 );
		float damageResult = target.Context.Model.Vitality - vitalityResult;
		target.Context.Model.Vitality -= damageResult;

		// Kill unit
		if( target.Context.Model.Vitality <= 0 ){
			// Reduce enemmies
			KillUnit( target );
		}

		return damageResult;
	}
	#endregion

	public GameObject getUnit( int id ){
		foreach( AgentEntity unit in homes[ PlayerAuthority.Player1 ].getPopulation() ){
			if( unit.Id == id ){
				return unit.gameObject;
			}
		}
		foreach( AgentEntity unit in homes[ PlayerAuthority.Player2 ].getPopulation() ){
			if( unit.Id == id ){
				return unit.gameObject;
			}
		}
		return null;
	}

	public void KillUnit( AgentEntity unit ){
		homes[ unit.Authority ].removeUnit( unit );
		GameObject.Destroy( unit.gameObject );
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
