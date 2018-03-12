using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEBUG_Manager : MonoBehaviour {

    /// <summary>
    /// The static instance of the Singleton for external access
    /// </summary>
    public static DEBUG_Manager instance = null;

    public bool debugRange = false;
    public bool debugCast = false;
    public bool debugMineraiStock = false;

    /// <summary>
    /// Enforce Singleton properties
    /// </summary>
    void Awake() {
        //Check if instance already exists and set it to this if not
        if (instance == null) {
            instance = this;
        }

        //Enforce the unicity of the Singleton
        else if (instance != this) {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
