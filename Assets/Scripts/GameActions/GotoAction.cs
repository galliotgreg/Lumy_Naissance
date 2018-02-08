using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotoAction : GameAction {
	
    [SerializeField]
    private Vector3[] path;

    public Vector3[] Path
    {
        get
        {
            return path;
        }

        set
        {
            path = value;
        }
    }

	#region implemented abstract members of GameAction

	protected override void initAction ()
	{
		this.CoolDownActivate = false;
	}

	protected override void executeAction ()
	{
		// Setting next target
		if (path.Length > 0)
		{
			if (path.Length == 1) {
				agentAttr.TrgPos = path [0];
			} else {
				// selectNext Point
				int closestIndex = indexClosest( agentAttr.transform.position, path );

				// On a point
				if (isClose (agentAttr.transform.position, path [closestIndex])) {
					// set next target (or the last point, if the index is the last)
					agentAttr.TrgPos = (closestIndex == path.Length-1?path [closestIndex]:path [closestIndex+1]);
				} else {
					// On an intersection
					int edge = getEdge( agentAttr.transform.position, closestIndex, path );
					agentAttr.TrgPos = path [closestIndex + edge];
				}
			}

			// Use Unity A* to move
			UnityEngine.AI.NavMeshAgent movingAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
			agentAttr.transform.position = moveTo( agentAttr, movingAgent );
			//agentAttr.CurPos = new Vector2( agentAttr.transform.position.x, agentAttr.transform.position.z );
		}
	}

	#endregion

	public static Vector3 moveTo( AgentScript agentAttr, UnityEngine.AI.NavMeshAgent navMeshAgent ){
		// Use Unity A* to move
		if( navMeshAgent != null ){
			//navMeshAgent.acceleration = 1;
			//navMeshAgent.speed = agentAttr.MoveSpd;
			//navMeshAgent.destination = new Vector3( agentAttr.TrgPos.x, 0f, agentAttr.TrgPos.y);
			//navMeshAgent.updatePosition = false;
			Vector3 destination = new Vector3( agentAttr.TrgPos.x, 0f, agentAttr.TrgPos.y);

			UnityEngine.AI.NavMeshPath path = new UnityEngine.AI.NavMeshPath();
			navMeshAgent.CalculatePath( destination, path );

			// move towards next corner
			if (path.corners.Length > 0) {
    			
	            return agentAttr.transform.position + Time.deltaTime * agentAttr.MoveSpd * (path.corners [path.corners.Length-1] - agentAttr.transform.position).normalized;
			}
		}
		return agentAttr.transform.position;
	}

	private int indexClosest( Vector3 pos, Vector3[] _path ){
		int resultIndex = 0;
		float resultDistance = Vector3.Distance (pos, _path [resultIndex]);
		for (int i = 1; i < _path.Length; i++) {
			float auxDistance = Vector3.Distance (pos, _path [i]);
			if ( auxDistance < resultDistance ) {
				resultIndex = i;
				resultDistance = auxDistance;
			}
		}

		return resultIndex;
	}

	private bool isClose( Vector3 pos, Vector3 point ){
		return Vector3.Distance (pos, point) > 0.01f;
	}

	// -1 edge towards the previous point
	// 1 edge towards the next point
	// 0 not edge
	private int getEdge( Vector3 pos, int index, Vector3[] _path ){
		if (index > 0) {
			if (isEdge (pos, _path [index-1], _path [index])) {
				return -1;
			}
		}
		if (index < _path.Length-1) {
			if (isEdge (pos, _path [index], _path [index+1])) {
				return 1;
			}
		}
		return 0;
	}

	private bool isEdge( Vector3 pos, Vector3 a, Vector3 b ){
		return Vector3.Dot( (a-b).normalized, (a-pos).normalized ) > 0.99f ;
	}
}