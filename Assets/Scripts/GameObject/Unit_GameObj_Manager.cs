using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_GameObj_Manager : MonoBehaviour {

	/// <summary>
	/// The static instance of the Singleton for external access
	/// </summary>
	public static Unit_GameObj_Manager instance = null;
    public GameObject Deathexplosion;

    public int unitPlayer1Created = 0;
    public int unitPlayer2Created = 0;
    public int unitPlayer1Destroyed = 0;
    public int unitPlayer2Destroyed = 0;

    public int nbLights = 0;
    public int maxLights = 10;

    //Variables for timer
    private float timerJ1 = 5;
    private float timerJ2 = 5;
    private bool isJ1Damaged = false;
    private bool isJ2Damaged = false;
    private bool SoundJ1Played = false;
    private bool SoundJ2Played = false;

    private float timerLumy= 5;
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
	List<ResourceScript> resources = new List<ResourceScript>();
	Dictionary<PlayerAuthority, List<TraceScript>> traces = new Dictionary<PlayerAuthority, List<TraceScript>>();


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
		set {
			resources = value;
		}
	}

	public Dictionary<PlayerAuthority, List<TraceScript>> Traces {
		get {
			return traces;
		}
	}
	#endregion

	#region Actions
	public float strikeUnit( AgentEntity target, float damage ){
        if(target.Context.Model.Vitality <= 0)
        {
            return 0; 
        }
        float vitalityResult = Mathf.Max( target.Context.Model.Vitality - damage, 0 );
		float damageResult = target.Context.Model.Vitality - vitalityResult;
		target.Context.Model.Vitality -= damageResult;


		// Kill unit
		if( target.Context.Model.Vitality <= 0 ){
			// Reduce enemmies
			KillUnit( target );
		}

        //Play SFX on prysme attacked
        if(InGameUIController.instance != null)
        {
            if (target.gameObject.GetComponent<AgentContext>().Self.GetComponent<AgentScript>().Cast == "prysme" &&
             target.gameObject.GetComponent<AgentContext>().Home.name == "p1_hive")
            {
                isJ1Damaged = true;
            }
            if (target.gameObject.GetComponent<AgentContext>().Self.GetComponent<AgentScript>().Cast == "prysme" &&
               target.gameObject.GetComponent<AgentContext>().Home.name == "p2_hive")
            {
                isJ2Damaged = true;
            }
        }
  
        return damageResult;
	}



    public void addPrysme( AgentEntity prysme, HomeScript home ){ 
		home.addPrysmeToHome (prysme); 
	}
	public void addUnit( AgentEntity unit, HomeScript home ) {
        if (nbLights > maxLights)
        {
            Light light = unit.transform.Find("Light").GetComponent<Light>();
            light.enabled = false;
        } else
        {
            nbLights++;
        }

		home.addUnitToHome (unit);
        incrementUnitCreation(home);
	}

    public void incrementUnitCreation(HomeScript home) {
        if (home.Authority == PlayerAuthority.Player1) {
            unitPlayer1Created++;
        }
        else if (home.Authority == PlayerAuthority.Player2) {
            unitPlayer2Created++;
        }
    }

	public bool pickResource( ResourceScript resource ){
        if (resource.Stock >1)
        {
            return true; 
        }

		if( this.resources.Remove( resource ) ){
			return true;
		}
		return false;
	}
	public bool dropResource(ResourceScript resource ){
		if( !this.resources.Contains( resource ) ){
			this.resources.Add( resource );
            Vector3 pos =  resource.gameObject.transform.position;
            resource.gameObject.transform.SetParent(this.transform);
            resource.gameObject.transform.position = pos; 

			return true;
		}
		return false;
	}

	public bool addTrace( TraceScript trace ){
		if (!this.traces.ContainsKey (trace.Authority)) {
			this.traces.Add (trace.Authority, new List<TraceScript> (){ trace });
			return true;
		} else {
			if (!this.traces [trace.Authority].Contains (trace)) {
				this.traces[trace.Authority].Add (trace);
				return true;
			}
		}
		return false;
	}
	#endregion

	#region Context Information
	public List<AgentEntity> alliesInRange( AgentEntity agent ){
		return this.AgentsInRange ( agent, true );
	}
	public List<AgentEntity> enemiesInRange( AgentEntity agent ){
		return this.AgentsInRange ( agent, false );
	}

	public List<ResourceScript> resourcesInRange( AgentEntity agent ){
		List<ResourceScript> resultList = new List<ResourceScript>();

		float agentRange = agent.Context.Model.VisionRange;
		foreach (ResourceScript resource in this.resources) {
			if( Vector2.Distance(agent.Context.Model.CurPos, resource.Location) <= agentRange ){
				resultList.Add (resource);
			}
		}

		return resultList;
	}
	public List<TraceScript> tracesInRange( AgentEntity agent ){
		List<TraceScript> resultList = new List<TraceScript>();

		float agentRange = agent.Context.Model.VisionRange;
		if (this.traces.ContainsKey (agent.Authority)) {
			foreach (TraceScript trace in this.traces[agent.Authority]) {
				foreach (Vector2 tracePos in trace.VisualPoints) {
					if (Vector2.Distance (agent.Context.Model.CurPos, tracePos) <= agentRange) {
						resultList.Add (trace);
						break;
					}
				}
			}
		}

		return resultList;
	}

	/// <summary>
	/// Obtains the list of agents that are inside the AGENT's range of vision
	/// </summary>
	/// <returns>List of agents that are inside the AGENT's range of vision.</returns>
	/// <param name="agent">Agent whose informations will be used to calculate the range.</param>
	/// <param name="allies">If set to <c>true</c>, returns the allies. <otherwise>, the enemies.</param>
	private List<AgentEntity> AgentsInRange( AgentEntity agent, bool allies ){
		List<AgentEntity> evaluationList;
		List<AgentEntity> resultList = new List<AgentEntity>();

		if (allies) {
			evaluationList = this.homes [agent.Authority].getPopulation ();
			evaluationList.Remove (agent);
		} else {
			evaluationList = new List<AgentEntity> ();
			List<HomeScript> h = this.enemyHomes (agent);
			foreach (HomeScript enemyHome in this.enemyHomes( agent )) {
				evaluationList.AddRange ( enemyHome.getPopulation() );
			}
		}

		float agentRange = agent.Context.Model.VisionRange;
		foreach (AgentEntity unit in evaluationList) {
			if( Vector2.Distance(agent.Context.Model.CurPos, unit.Context.Model.CurPos) <= agentRange ){
				resultList.Add (unit);
			}
		}

		return resultList;
	}

	/// <summary>
	/// A list containing the enemies' homes.
	/// </summary>
	/// <returns>Enemies' homes.</returns>
	/// <param name="agent">Agent whose informations will be used to calculate the list.</param>
	private List<HomeScript> enemyHomes( AgentEntity agent ){
		List<HomeScript> result = new List<HomeScript> ();

		foreach( PlayerAuthority auth in homes.Keys ){
			if (auth != agent.Authority) {
				result.Add (homes [auth]);
			}
		}

		return result;
	}
	#endregion

	public GameObject getUnit( int key ){
		foreach( AgentEntity unit in homes[ PlayerAuthority.Player1 ].getPopulation() ){
			if( unit.Context.Model.Key == key ){
				return unit.gameObject;
			}
		}
		foreach( AgentEntity unit in homes[ PlayerAuthority.Player2 ].getPopulation() ){
			if( unit.Context.Model.Key == key ){
				return unit.gameObject;
			}
		}
		return null;
	}

    //TODO : Refacto, use string instead of int/float for key/hash
	public GameObject getResource( int key ){
		foreach( ResourceScript resource in resources ){
			if( ((int)(resource.Key/10000)) == ((int)(key/10000)) ){
				return resource.gameObject;
			} 
		}
        Debug.LogError("Key is not find " + key);
        Debug.LogError("==================== " );
        foreach (ResourceScript resource in resources) {
            Debug.LogError(resource.Key.ToString());
        }
        Debug.LogError("==================== ");

        return null;
	}

	public void KillUnit( AgentEntity unit ){

        if (InGameUIController.instance != null &&
            InGameUIController.instance.Self 
            == unit.gameObject.GetComponent<AgentContext>().Self.GetComponent<AgentScript>())
        {
            SoundManager.instance.PlayLumyDeathSFX();
        }

        GameObject explosion = Instantiate(Deathexplosion, unit.Context.Model.transform.position, Quaternion.identity);
        //Move behind GameManager (avoid conflict with swapping scene) 
        explosion.transform.SetParent(GameManager.instance.transform);
        Destroy(explosion, explosion.GetComponentInChildren<ParticleSystem>().duration);
        //add fx end

        homes[ unit.Authority ].removeUnit( unit );
		GameObject.Destroy( unit.gameObject );

        Light light = unit.transform.Find("Light").GetComponent<Light>();
        if (light.enabled)
        {
            nbLights--;
        }

        incrementKilledEntity(unit);

	}

    public void incrementKilledEntity(AgentEntity unit) {
        if(unit.Authority == PlayerAuthority.Player1) {
            unitPlayer1Destroyed++;
        }
        else if(unit.Authority == PlayerAuthority.Player2) {
            unitPlayer2Destroyed++;
        }
    }

	public bool destroyTrace( TraceScript trace ){
		if ( this.traces.ContainsKey (trace.Authority) ) {
			if (this.traces [trace.Authority].Remove(trace)) {
				Destroy( trace.gameObject );
				return true;
			}
		}
		return false;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //Incase not in game
        if (InGameUIController.instance == null) {
            return; 
        }

        //Timer for Alert Prysme
        if(isJ1Damaged)
        {
            timerJ1 -= Time.deltaTime; 
            if(timerJ1 <= 0)
            {
                isJ1Damaged = false;
                timerJ1 = 5;
                SoundJ1Played = false;
            }
            else if(!SoundJ1Played) {
                SoundManager.instance.PlayPrysmeIsAttackedSFX();
                SoundJ1Played = true; 
            }
        }

        if (isJ2Damaged)
        {
            timerJ2 -= Time.deltaTime;
            if (timerJ2 <= 0)
            {
                isJ2Damaged = false;
                timerJ2 = 5;
                SoundJ2Played = false;
            }
            else if (!SoundJ2Played)
            {
                SoundManager.instance.PlayPrysmeIsAttackedSFX();
                SoundJ2Played = true;
            }
        }

       


    }
}
