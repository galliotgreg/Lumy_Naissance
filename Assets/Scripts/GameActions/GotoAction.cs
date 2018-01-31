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

		// Moving to target
			// If it is too close, move directly
		// TODO replace by Unity A*
		if (Vector3.Distance( agentAttr.TrgPos, agentAttr.CurPos ) < 0.1f)
		{
			this.transform.position = new Vector3(
				agentAttr.TrgPos.x, 
				0f, 
				agentAttr.TrgPos.y);
			agentAttr.CurPos = agentAttr.TrgPos;
		}
		else
		{
			// Calculate A* to the target
			// TODO where search for the terrain information?
			// TODO use Unity A*?

			float speed = agentAttr.MoveSpd;

			Vector3 dir = (	agentAttr.TrgPos - agentAttr.CurPos ).normalized;
			this.transform.position += Time.deltaTime * speed * new Vector3(dir.x, 0f, dir.y);

			agentAttr.CurPos = this.transform.position;
		}

		// Use Unity A* to move
		/*UnityEngine.AI.NavMeshAgent movingAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		agentAttr.transform.position = moveTo( agentAttr, movingAgent );
		agentAttr.CurPos = new Vector2( agentAttr.transform.position.x, agentAttr.transform.position.z );*/
	}

	#endregion

	public static Vector3 moveTo( AgentScript agentAttr, UnityEngine.AI.NavMeshAgent navMeshAgent ){
		// Use Unity A* to move
		if( navMeshAgent != null ){
			//navMeshAgent.acceleration = 1;
			//navMeshAgent.speed = agentAttr.MoveSpd;
			navMeshAgent.destination = new Vector3( agentAttr.TrgPos.x, 0f, agentAttr.TrgPos.y);
			navMeshAgent.updatePosition = false;

			UnityEngine.AI.NavMeshPath path = new UnityEngine.AI.NavMeshPath();
			navMeshAgent.CalculatePath( agentAttr.gameObject.transform.position, path );

			// move towards next corner
			return agentAttr.transform.position + Time.deltaTime * agentAttr.MoveSpd * (path.corners[0] - agentAttr.transform.position).normalized;
		}
		return agentAttr.transform.position;
	}
}