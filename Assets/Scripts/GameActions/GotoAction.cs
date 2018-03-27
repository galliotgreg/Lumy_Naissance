using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GotoAction : GameAction {
	
    [SerializeField]
    private Vector3[] path;

	private Vector2 previousPosition;
	private Vector2 curDirection;

	#region PROPERTIES
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

	protected override void executeAction ()
	{
		Vector2 curPos = worldToVec2 (agentAttr.transform.position);

		// Calculating direction : necessary to roamingAction
		Vector2 dir = curPos - previousPosition;
		if (dir.magnitude > 0.01f) {
			curDirection = dir.normalized;
		}
		previousPosition = curPos;

		// On a point
		if (isClose (curPos, worldToVec2 (path [currentPathIndex]))) {
			targetReached (currentPathIndex);
			currentPathIndex = (currentPathIndex == path.Length - 1 ? currentPathIndex : (currentPathIndex + 1));
		}

		agentAttr.TrgPos = worldToVec2 (path [currentPathIndex]);

		// Use Unity A* to move
		moveTo (agentAttr, movingAgent);
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
		if (movingAgent != null) {
			movingAgent.isStopped = false;

			// define startPoint
			if (path.Length > 0) {
				if (path.Length == 1) {
					currentPathIndex = 0;
				} else {
					Vector2 curPos = worldToVec2 (agentAttr.transform.position);

					// selectNext Point
					int closestIndex = indexClosest (curPos, path);

					// On a point
					if (isClose (curPos, worldToVec2 (path [closestIndex]))) {
						// set next target (or the last point, if the index is the last)
						currentPathIndex = (closestIndex == path.Length - 1 ? closestIndex : closestIndex + 1);
					} else {
						// On an intersection
						int edge = getEdge (curPos, closestIndex, path);
						currentPathIndex = closestIndex + edge;
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

	#endregion

	const float closeFactor = 0.1f;
	public Vector3 moveTo( AgentScript agentAttr, UnityEngine.AI.NavMeshAgent navMeshAgent ){
        // Use Unity A* to move

        NavMeshPath navMeshpath = new NavMeshPath(); 
		if( navMeshAgent != null ){

            NavMeshHit hit;
            
                Vector3 destination = vec2ToWorld(agentAttr.TrgPos);
            destination.y = agentAttr.transform.position.y;

            NavMeshPath path = new NavMeshPath();
            bool hasFoundPath = navMeshAgent.CalculatePath(destination, path);

            Vector3 position = vec2ToWorld(agentAttr.CurPos);
            position.y = agentAttr.transform.position.y;

            Vector3 dest = vec2ToWorld(agentAttr.TrgPos);
            dest.y = agentAttr.transform.position.y;

            // Debug.DrawLine(position, dest, Color.blue);
            //Draw Line 
            if(OptionManager.instance.DirectionLumy != null)
            {
                if (OptionManager.instance.DirectionLumy.isOn)
                {
                    DrawLine(position, dest, Color.blue, 0.2f);
                }
            }

            if (OptionManager.instance.DirectionLumyJ2 != null) {
                if (OptionManager.instance.DirectionLumyJ2.isOn) {
                    DrawLine(position, dest, Color.blue, 0.2f);
                }
            }


            if (path.status == NavMeshPathStatus.PathPartial)
            {
                agentAttr.TrgPos = agentAttr.CurPos;
                destination = vec2ToWorld(agentAttr.TrgPos); 
				targetUnreachable ();
            }
            else if (path.status == NavMeshPathStatus.PathInvalid)
            {
                agentAttr.TrgPos = agentAttr.CurPos;
                destination = vec2ToWorld(agentAttr.TrgPos);
				targetUnreachable ();
            }
            position = vec2ToWorld(agentAttr.CurPos);
            position.y = agentAttr.transform.position.y;

            dest = vec2ToWorld(agentAttr.TrgPos);
            dest.y = agentAttr.transform.position.y;
           

            navMeshAgent.acceleration = 1000;
			navMeshAgent.speed = agentAttr.MoveSpd;
			navMeshAgent.autoBraking = true;
			navMeshAgent.destination = destination;
			navMeshAgent.stoppingDistance = 0.1f;


            /*//navMeshAgent.updatePosition = false;
			UnityEngine.AI.NavMeshPath auxpath = new UnityEngine.AI.NavMeshPath();
			navMeshAgent.CalculatePath( destination, auxpath );

			// move towards next corner
			if (auxpath.corners.Length > 0) {
				Debug.Log ("move");
				Debug.Log (auxpath.corners[0]);
				Debug.Log (auxpath.corners[1]);
				Debug.Log (agentAttr.transform.position);
				return agentAttr.transform.position + Time.deltaTime * agentAttr.MoveSpd * (auxpath.corners [1] - agentAttr.transform.position).normalized;
			}*/
        }
        return agentAttr.transform.position;
	}

    void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.2f)
    {
        GameObject myLine = new GameObject();
        myLine.transform.SetParent(GameManager.instance.transform); 
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        //lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        lr.SetColors(color, color);
        lr.SetWidth(0.1f, 0.1f);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        GameObject.Destroy(myLine, duration);
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
		return Vector2.Distance (pos, point) < closeFactor;
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

	private static Vector2 worldToVec2( Vector3 point ){
		return AgentBehavior.worldToVec2(point);
	}
	private static Vector3 vec2ToWorld( Vector2 point ){
		return AgentBehavior.vec2ToWorld(point);
	}

	/// <summary>
	/// Called when one point in the path is reached.
	/// </summary>
	/// <param name="index">Index of the reached point in the path</param>
	protected virtual void targetReached( int index ){}

	/// <summary>
	/// Called when the path is unreachable
	/// </summary>
	protected virtual void targetUnreachable(){}
}