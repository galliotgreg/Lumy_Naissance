using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_Debugger_Manager : MonoBehaviour {

	[SerializeField]
	RectTransform container;

	// Current Values to be shown
	ABModel current_Model;
	AgentEntity current_Entity;
	Tracing current_Tracing;

	public void activateDebugger( AgentEntity entity ){
		// activate container
		// get data
		current_Entity = entity;
		current_Model = ABManager.instance.FindABInstance( entity.Id ).Model;
		current_Tracing = ABManager.instance.getCurrentTracing (entity);
		// Draw model
	}
	public void deactivateDebugger(){
		// deactivate container
		// removing data
		current_Model = null;
		current_Entity = null;
		current_Tracing = null;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	#region Tracing
	void instantiateTracing( Tracing tracing, ABModel model ){
		List<TracingInfo> tracingList = tracing.getTracingInfo();

		if( tracingList.Count > 0 ){
			// Init state
			tracingList [0].InfoProxy (model);
			for (int i = 1; i < tracingList.Count; i++) {
				// State
				tracingList [i].InfoProxy (model);
				// Transition

			}
		}
	}
	#endregion
}
