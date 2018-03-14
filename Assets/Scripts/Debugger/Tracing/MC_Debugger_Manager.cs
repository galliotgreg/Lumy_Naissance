using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_Debugger_Manager : MonoBehaviour {

	[SerializeField]
	GameObject container;

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
		clearTracing ();
		instantiateTracing( current_Tracing, current_Model );
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
		if (ABManager.instance.Agent0 != null) {
			activateDebugger (ABManager.instance.Agent0);
		}
	}

	#region Tracing
	void instantiateTracing( Tracing tracing, ABModel model ){
		if (tracing != null && model != null) {
			List<TracingInfo> tracingList = tracing.getTracingInfo ();
			List<Debugger_Node> proxies = new List<Debugger_Node> ();
			for (int i = 0; i < tracingList.Count; i++) {
				// State
				proxies.Add (tracingList [i].InfoProxy (model, container.transform));
				UnityEngine.UI.LayoutElement layoutElement = proxies [i].gameObject.AddComponent<UnityEngine.UI.LayoutElement> ();
				layoutElement.flexibleHeight = 70;
				//proxies [i].transform.position = new Vector3 (0, (tracingList.Count - i) * 1.5f, 0);

				// Transition
			}
		}
	}
	void clearTracing(  ){
		for( int i = 0; i < container.transform.childCount; i++ ){
			Transform child = container.transform.GetChild (i);
			Destroy (child.gameObject);
		}
	}
	#endregion
}
