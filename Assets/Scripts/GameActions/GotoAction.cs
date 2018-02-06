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
			agentAttr.TrgPos = path[0];
		}

		// Use Unity A* to move
		UnityEngine.AI.NavMeshAgent movingAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		agentAttr.transform.position = moveTo( agentAttr, movingAgent );
		//agentAttr.CurPos = new Vector2( agentAttr.transform.position.x, agentAttr.transform.position.z );
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
				return agentAttr.transform.position + Time.deltaTime * agentAttr.MoveSpd * (agentAttr.transform.position - path.corners [path.corners.Length-1]).normalized;
			}
		}
		return agentAttr.transform.position;
	}
}