using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceAction : GameAction {

    [SerializeField]
	private Vector3[] path;
    [SerializeField]
	private Color32 color;

	[SerializeField]
	private GameObject tracePrefab;
	[SerializeField]
	private GameObject traceUnitPrefab;

	public Vector3[] Path {
		get {
			return path;
		}
		set {
			path = value;
		}
	}

	public Color32 Color {
		get {
			return color;
		}
		set {
			color = value;
		}
	}

	#region implemented abstract members of GameAction

	protected override void initAction ()
	{
		this.CoolDownActivate = false;
	}

	protected override void executeAction (){}

	protected override void activateAction ()
	{
		// TraceScript
		if( tracePrefab != null ){
			GameObject traceObject = Instantiate( tracePrefab, this.transform.position, this.transform.rotation );
			TraceScript traceScript = traceObject.GetComponent<TraceScript>();
			if (traceScript != null) {
				if (path.Length == 1) {
					traceScript.CreateTrace (color, AgentBehavior.worldToVec2 (this.transform.position), AgentBehavior.worldToVec2 (path [0]), agentEntity.Authority);
				} else {
					Vector2[] convertedPath = new Vector2[ path.Length ];
					for (int i = 0; i < path.Length; i++) {
						convertedPath [i] = AgentBehavior.worldToVec2 ( path[i] );
					}

					traceScript.CreateTrace (color, AgentBehavior.worldToVec2 (this.transform.position), convertedPath, agentEntity.Authority);
				}

				// create game objects
				if( traceUnitPrefab != null ){
					foreach( Vector2 pos in traceScript.VisualPoints ){
						GameObject traceUnitObject = Instantiate( traceUnitPrefab, AgentBehavior.vec2ToWorld( pos ), this.transform.rotation, this.transform );
					}
				}

				Unit_GameObj_Manager.instance.addTrace ( traceScript );
			}
		}
	}

	protected override void deactivateAction (){}

	#endregion
}
