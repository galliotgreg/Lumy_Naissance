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

	protected override void executeAction ()
	{
		// Create Tracing Object
		if( tracePrefab != null ){
			GameObject traceObject = Instantiate( tracePrefab, this.transform.position, this.transform.rotation );
			TraceGameObject traceScript = traceObject.GetComponent<TraceGameObject>();
			if( traceScript != null ){
				traceScript.Color = this.color;
			}
		}

		// Update Position
		if (path.Length > 0)
		{
			agentAttr.TrgPos = path[0];
			agentAttr.transform.position = GotoAction.moveTo( agentAttr, this.GetComponent<UnityEngine.AI.NavMeshAgent>() );
			agentAttr.CurPos = new Vector2( agentAttr.transform.position.x, agentAttr.transform.position.z );
		}

		// TODO where to store the trace? Normally at the GameManager
	}

	#endregion
}
