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
	}

	#endregion
}