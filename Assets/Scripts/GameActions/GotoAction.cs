using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GotoAction : GameAction {
	
    [SerializeField]
    private Vector3[] path;
   


    private Vector2 previousPosition;
	private Vector2 curDirection;
	private bool pathChanged = false;

	#region PROPERTIES
    public Vector3[] Path
    {
        get
        {
            return path;
        }

        set
        {
			// verify modifications
			bool changed = false;

			if (path == null || value == null) {
				changed = true;
			} else {
				if (value.Length != path.Length) {
					changed = true;
				} else {
					for (int i = 0; i < path.Length; i++) {
						if( path[i] != value[i] ){
							changed = true;
						}
					}
				}
			}

			pathChanged = changed;
			path = value;
        }
    }

	public Vector2 CurDirection {
		get {
			return curDirection;
		}
	}
	#endregion

	UnityEngine.AI.NavMeshAgent movingAgent;

	[SerializeField]
	int currentPathIndex = 0;

	#region implemented abstract members of GameAction

	protected override void initAction ()
	{
		this.CoolDownActivate = false;
		movingAgent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
	}

	protected override bool executeAction ()
	{
		/**
		 * every Update, we check if the unit reached its target and change it if necessary.
		 * move the unit towards the target
		 * 
		 * In addition, we define the current direction
		 */

		Vector2 curPos = agentAttr.CurPos;

		// Calculating direction : necessary to roamingAction
		Vector2 dir = curPos - previousPosition;
		if (dir.magnitude > 0.01f) {
			curDirection = dir.normalized;
		}
		previousPosition = curPos;

		Vector3 destination = getValidTarget( vec2ToWorld(curPos), path [currentPathIndex]);
		//destination = worldToVec2 ( vec2ToWorld( destination ) );
		//Vector3 destination = path [currentPathIndex];

		// On a point
		if (isClose (curPos, worldToVec2( destination )) ) {
			targetReached (currentPathIndex);
			currentPathIndex = (currentPathIndex == path.Length - 1 ? 0 : (currentPathIndex + 1));
		}

		agentAttr.TrgPos = worldToVec2( destination );

		// Use Unity A* to move
		moveTo (agentAttr, movingAgent);

		// reset cooldown : in the case cooldownactivate is false, it does not matter
		return this.CoolDownActivate;
    }

	protected override void activateAction ()
	{
		return;
	}

	protected override void deactivateAction ()
	{
		return;
	}

	protected override void frameBeginAction ()
	{
		/**
		 * every Frame, if the path has changed, we obtain the closest path point to the unit. the currentPathIndex will store the index of the point
		 * In the case of several point in the path, if the unit is close to a point, 
		 */

		if (movingAgent != null) {
			movingAgent.isStopped = false;

			if (pathChanged) {
				// define startPoint
				if (path.Length > 0) {
					if (path.Length == 1) {
						currentPathIndex = 0;
					} else {
						Vector2 curPos = agentAttr.CurPos;

						// selectNext Point
						int closestIndex = indexClosest (curPos, path);

						// On a point
						if (isClose (curPos, worldToVec2 (path [closestIndex]))) {
							// set next target (or the initial point, if the index is the last)
							currentPathIndex = (closestIndex == path.Length - 1 ? 0 : closestIndex + 1);
						} else {
							// On an intersection
							int edge = getEdge (curPos, closestIndex, path);
							currentPathIndex = closestIndex + edge;
						}
					}
				}
			}
		}
		return;
	}

	protected override void frameBeginAction_CooldownAuthorized ()
	{
		return;
	}

	protected override void frameEndAction ()
	{
		if (movingAgent != null) {
			movingAgent.isStopped = true;
		}
		return;
	}

	protected override void cooldownFinishAction ()
	{
		return;
	}

	#endregion

	const float closeFactor = 0.3f;
	public Vector3 moveTo( AgentScript agentAttr, UnityEngine.AI.NavMeshAgent navMeshAgent ){
        // Use Unity A* to move

        NavMeshPath navMeshpath = new NavMeshPath(); 
		if( navMeshAgent != null ){
            
            Vector3 destination = vec2ToWorld(agentAttr.TrgPos);
            destination.y = agentAttr.transform.position.y;

            Vector3 position = vec2ToWorld(agentAttr.CurPos);
            position.y = agentAttr.transform.position.y;



            if (OptionManager.instance.DirectionLumy != null && 
                OptionManager.instance.DirectionLumyJ2 !=null && 
                Time.timeScale == 1)
            {
                if (OptionManager.instance.DirectionLumy.isOn &&
                agentAttr.gameObject.GetComponentInParent<AgentContext>().Home.GetComponent<HomeScript>().Authority == PlayerAuthority.Player1)
                {
					DrawLine(position, destination, Color.blue, 0.2f);
                }
                else if (OptionManager.instance.DirectionLumyJ2.isOn &&
                agentAttr.gameObject.GetComponentInParent<AgentContext>().Home.GetComponent<HomeScript>().Authority == PlayerAuthority.Player2)
                {
                    DrawLine(position, destination, Color.red, 0.2f);
                }
            }

			if(!isCompletePath(position, destination))
            {
				destination = targetUnreachable (destination);
            }
           
			navMeshAgent.acceleration = 1000;
			navMeshAgent.speed = agentAttr.MoveSpd;
			navMeshAgent.autoBraking = true;
			navMeshAgent.destination = destination;
			navMeshAgent.stoppingDistance = closeFactor;

        }

		return agentAttr.transform.position;
	}

    void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.2f)
    {
        LineRenderer lr = agentAttr.gameObject.GetComponentInChildren<LineRenderer>();
       
        lr.SetColors(color, color);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        //GameObject.Destroy(myLine, duration);
    }

    private int indexClosest( Vector2 pos, Vector3[] _path ){
		int resultIndex = 0;
		UnityEngine.AI.NavMeshPath auxpath = new UnityEngine.AI.NavMeshPath();
		movingAgent.CalculatePath( _path [resultIndex], auxpath );

		float resultDistance =  pathLength( auxpath );

		for (int i = 1; i < _path.Length; i++) {
			auxpath.ClearCorners ();
			movingAgent.CalculatePath( _path [i], auxpath );

           
            float auxDistance = pathLength( auxpath );

			if ( auxDistance < resultDistance ) {
				resultIndex = i;
				resultDistance = auxDistance;
			}
		}

		return resultIndex;
	}

	public static float pathLength(UnityEngine.AI.NavMeshPath _path){
		float result = 0;
		for (int i = 0; i < _path.corners.Length-1; i++) {
			result += Vector3.Distance (_path.corners [i], _path.corners [i + 1]);
		}
		return result;
	}

	// TODO : adapt isClose and isEdge params
	private bool isClose( Vector2 pos, Vector2 point ){
		return Vector2.Distance (pos, point) < closeFactor*2;
	}
		
	// 1 edge towards the next point
	// 0 not edge or towards previous
	private int getEdge( Vector2 pos, int index, Vector3[] _path ){
		if (index < _path.Length-1) {
			UnityEngine.AI.NavMeshPath _navMeshPath = new UnityEngine.AI.NavMeshPath ();
			UnityEngine.AI.NavMesh.CalculatePath (_path [index],_path [index+1], UnityEngine.AI.NavMesh.AllAreas, _navMeshPath);
			if (isEdgePath (pos, _navMeshPath)) {
				return 1;
			}
		}
		return 0;
	}

	private bool isEdgePath( Vector2 pos, UnityEngine.AI.NavMeshPath _path ){
		for (int i = 0; i < _path.corners.Length-1; i++) {
			if (isEdgePoints (pos, worldToVec2 (_path.corners [i]), worldToVec2 (_path.corners [i + 1]))) {
				return true;
			}
		}
		return false;
	}
	private bool isEdgePoints( Vector2 pos, Vector2 a, Vector2 b ){
		return Vector3.Dot( (a-b).normalized, (a-pos).normalized ) > 0.7f ;
	}

	protected Vector2 worldToVec2( Vector3 point ){
		return AgentBehavior.worldToVec2(point);
	}
	protected Vector3 vec2ToWorld( Vector2 point ){
		return AgentBehavior.vec2ToWorld(point);
	}
	protected Vector3 vec2ToLumy( Vector2 point ){
		Vector3 result = AgentBehavior.vec2ToWorld(point);
		return vec3ToLumy( result );
	}
	protected Vector3 vec3ToLumy( Vector3 point ){
		point.y = agentAttr.transform.position.y;
		return point;
	}

	/// <summary>
	/// Called when one point in the path is reached.
	/// </summary>
	/// <param name="index">Index of the reached point in the path</param>
	protected virtual void targetReached( int index ){
		//agentAttr.TrgPos = agentAttr.CurPos;
	}

	/// <summary>
	/// Called when the path is unreachable
	/// </summary>
	/// <returns>A new destination</returns>
	protected virtual Vector3 targetUnreachable( Vector3 currentDestination ){
		agentAttr.TrgPos = agentAttr.CurPos;
		Vector3 result = vec2ToWorld (agentAttr.TrgPos);
		result.y = agentAttr.transform.position.y;
		return result;
	}

	#region VALID POSITION
	bool isNavMeshPosition( Vector3 target ){
		// Test if there is a navmesh at the point
		NavMeshHit hit = new NavMeshHit ();
		if (NavMesh.SamplePosition (target, out hit, 0.5f, NavMesh.AllAreas)) {
			return true;
		}
		return false;
	}

	protected bool isCompletePath(Vector3 origin, Vector3 target){
		if (isNavMeshPosition (target)) {
			NavMeshPath path = new NavMeshPath();
			NavMesh.CalculatePath(origin, target, NavMesh.AllAreas, path);

			return path.status == NavMeshPathStatus.PathComplete;
		}
		return false;
	}

	protected Vector3 getValidTarget(Vector3 origin, Vector3 target){
		// Test if there is a path to this 
		NavMeshPath path = new NavMeshPath();
		NavMesh.CalculatePath(origin, target, NavMesh.AllAreas, path);

		if (path.status == NavMeshPathStatus.PathComplete) {
			// if the path is complete, the target remains
			return target;
		} else {
			if (path.status == NavMeshPathStatus.PathPartial) {
				// if the path is incomplete, the target is the last point in the path
				if (path.corners.Length > 0) {
					return path.corners [path.corners.Length - 1];
				}
			} else {
				NavMeshHit hit = new NavMeshHit();
				NavMesh.Raycast (origin, target, out hit, NavMesh.AllAreas);
				return hit.position;
			}
		}

		return origin;
	}
	#endregion
}