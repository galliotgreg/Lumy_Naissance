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
				agentAttr.TrgPos = worldToVec2( path [0] );
			} else {
				Vector2 curPos = worldToVec2 (agentAttr.transform.position);

				// selectNext Point
				int closestIndex = indexClosest( curPos, path );

				// On a point
				if (isClose ( curPos, worldToVec2 (path [closestIndex]))) {
					// set next target (or the last point, if the index is the last)
					agentAttr.TrgPos = worldToVec2( (closestIndex == path.Length-1?path [closestIndex]:path [closestIndex+1]) );
				} else {
					// On an intersection
					int edge = getEdge( curPos, closestIndex, path );
					agentAttr.TrgPos = worldToVec2( path [closestIndex + (edge>0?edge:0)] );
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
			Vector3 destination = vec2ToWorld( agentAttr.TrgPos );

			UnityEngine.AI.NavMeshPath path = new UnityEngine.AI.NavMeshPath();
			navMeshAgent.CalculatePath( destination, path );

			// move towards next corner
			if (path.corners.Length > 0) {
	            return agentAttr.transform.position + Time.deltaTime * agentAttr.MoveSpd * (path.corners [path.corners.Length-1] - agentAttr.transform.position).normalized;
			}
		}
		return agentAttr.transform.position;
	}

	private int indexClosest( Vector2 pos, Vector3[] _path ){
		int resultIndex = 0;
		float resultDistance = Vector2.Distance (pos, worldToVec2(_path [resultIndex]));
		for (int i = 1; i < _path.Length; i++) {
			float auxDistance = Vector3.Distance (pos, worldToVec2(_path [i]));
			if ( auxDistance < resultDistance ) {
				resultIndex = i;
				resultDistance = auxDistance;
			}
		}

		return resultIndex;
	}

	private bool isClose( Vector2 pos, Vector2 point ){
		return Vector2.Distance (pos, point) < 0.1f;
	}

	//TODO
	// -1 edge towards the previous point
	// 1 edge towards the next point
	// 0 not edge
	private int getEdge( Vector2 pos, int index, Vector3[] _path ){
		if (index > 0) {
			if (isEdge (pos, worldToVec2 (_path [index-1]), worldToVec2 (_path [index]))) {
				return -1;
			}
		}
		if (index < _path.Length-1) {
			if (isEdge (pos, worldToVec2 (_path [index]), worldToVec2 (_path [index+1]))) {
				return 1;
			}
		}
		return 0;
	}

	private bool isEdge( Vector2 pos, Vector2 a, Vector2 b ){
		return Vector3.Dot( (a-b).normalized, (a-pos).normalized ) > 0.9f ;
	}

	private static Vector2 worldToVec2( Vector3 point ){
		return AgentBehavior.worldToVec2(point);
	}
	private static Vector3 vec2ToWorld( Vector2 point ){
		return AgentBehavior.vec2ToWorld(point);
	}
}