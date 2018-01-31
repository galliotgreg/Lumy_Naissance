using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentBehavior : MonoBehaviour
{
    /// <summary>
    /// Name of the ABM the agent engages in
    /// </summary>
    [SerializeField]
    private string behaviorModelIdentifier;

    [SerializeField]
    private ActionType curActionType;

    private IABType[] curActionParams;

    //Actions
    private GotoAction gotoAction;
    private TraceAction traceAction;
    private LayAction layAction;
	private StrikeAction strikeAction;
	private PickAction pickAction;

    /// <summary>
    /// The current Atomic Action being presseced by the agent
    /// </summary>
    private ABAction curAction;

    public string BehaviorModelIdentifier
    {
        get
        {
            return behaviorModelIdentifier;
        }

        set
        {
            behaviorModelIdentifier = value;
        }
    }

    public ABAction CurAction
    {
        get
        {
            return curAction;
        }

        set
        {
            curAction = value;
            CurActionType = value.Type;
        }
    }

    public IABType[] CurActionParams
    {
        get
        {
            return curActionParams;
        }

        set
        {
            curActionParams = value;
        }
    }

    public ActionType CurActionType
    {
        get
        {
            return curActionType;
        }

        set
        {
            curActionType = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        gotoAction = GetComponent<GotoAction>();
        traceAction = GetComponent<TraceAction>();
        layAction = GetComponent<LayAction>();
		strikeAction = GetComponent<StrikeAction>();
		pickAction = GetComponent<PickAction>();
    }

    // Update is called once per frame
    void Update()
    {
        //We arre between 2 IA frames
        if (curActionParams == null)
        {
            return;
        }

        // Inject Param on corresponding Action Script then enable it
        switch (curActionType)
        {
            case ActionType.Drop:
                throw new NotImplementedException();
                break;
            case ActionType.Goto:
                ABTable<ABVec> path = ((ABTable<ABVec>)curActionParams[0]);
                gotoAction.Path = new Vector3[path.Values.Length];
                for (int i = 0; i < path.Values.Length; i++)
                {
                    ABVec abVec = path.Values[i];
                    Vector3 vec3 = new Vector3(abVec.X, abVec.Y);
                    gotoAction.Path[i] = vec3;
                }

                DisableActions();
				gotoAction.Activated = true;
                break;
            case ActionType.Hit:
                break;
            case ActionType.Hold:
                break;
            case ActionType.Lay:
                ABText castName = ((ABText)curActionParams[0]);
                layAction.CastName = castName.Value;

                DisableActions();
                layAction.activated = true;
                break;
            case ActionType.Pick:
				ABRef item = ((ABRef)curActionParams[0]);
				// TODO key
				throw new System.NotImplementedException();
				//pickAction.Item = item.GetAttr();

				DisableActions();
				pickAction.Activated = true;
                break;
            case ActionType.Spread:
                break;
            case ActionType.Trace:
				// color
				ABColor traceColor = ((ABColor)CurActionParams[0]);
				switch( traceColor.Value ){
					case ABColor.Color.Red:
						traceAction.Color = Color.red;
						break;
					case ABColor.Color.Green:
						traceAction.Color = Color.green;
						break;
					case ABColor.Color.Blue:
						traceAction.Color = Color.blue;
						break;
				}

				// path
				ABTable<ABVec> tracePath = ((ABTable<ABVec>)curActionParams[1]);
				traceAction.Path = new Vector3[tracePath.Values.Length];
				for (int i = 0; i < tracePath.Values.Length; i++)
				{
					ABVec abVec = tracePath.Values[i];
					Vector3 vec3 = new Vector3(abVec.X, abVec.Y);
					traceAction.Path[i] = vec3;
				}

				DisableActions();
				traceAction.Activated = true;
                break;
			case ActionType.Strike:
				ABRef target = ((ABRef)curActionParams[0]);
				// TODO key
				throw new System.NotImplementedException();
				//strikeAction.Target = target.GetAttr();

				DisableActions();
				strikeAction.Activated = true;
				break;
            case ActionType.None:
                break;
        }
    }

    private void DisableActions()
    {
		gotoAction.Activated = false;
		traceAction.Activated = false;
        layAction.activated = false;
		strikeAction.Activated = false;
		pickAction.Activated = false;
    }
}
