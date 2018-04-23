using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentBehavior : MonoBehaviour
{
    [SerializeField]
    private ActionType curActionType;

    private IABType[] curActionParams;

    //Actions
    private GotoAction gotoAction;
    private TraceAction traceAction;
    private LayAction layAction;
	private StrikeAction strikeAction;
	private PickAction pickAction;
	private DropAction dropAction;
	private RoamingAction roamingAction;

	private ABAction previousAction = null;

    /// <summary>
    /// The current Atomic Action being presseced by the agent
    /// </summary>
    private ABAction curAction;

	#region Properties
	protected ABAction CurAction
    {
        get
        {
            return curAction;
        }

        set
        {
            curAction = value;
			if (curAction == null) {
				curActionType = ActionType.None;
			} else {
				curActionType = value.Type;
			}

			// Disable/Enable
			// this != previous : disable
			if( previousAction == null || value == null || previousAction.Type != value.Type ){
				GameAction previous = getGameAction (previousAction);
				GameAction current = getGameAction (value);

				if( previous != null ){
					previous.deactivate ();
				}
				//activation is done when loading params
				/*if( current != null ){
					current.activate ();
				}*/
			}
			previousAction = value;
        }
    }

	protected IABType[] CurActionParams
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
    }

	protected GameAction CurrentGameAction{
		get{
			return getGameAction (curAction);
		}
	}
	#endregion

	public void setFrame( ABAction action, IABType[] actionParams ){
		FrameEnd ();

		this.CurAction = action;
		this.CurActionParams = actionParams;

		GameAction currentGameAction = getGameAction (this.curAction);
		if (currentGameAction != null) {
			fillActionParams ();
			currentGameAction.frameBegin ();
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
		dropAction = GetComponent<DropAction>();
		roamingAction = GetComponent<RoamingAction> ();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FrameEnd()
    {
		gotoAction.frameEnd ();
		traceAction.frameEnd ();
		layAction.frameEnd ();
		strikeAction.frameEnd ();
		pickAction.frameEnd ();
		dropAction.frameEnd ();
		roamingAction.frameEnd ();
    }

	private void fillActionParams(){
		if (curAction != null) {
			try{
				// Inject Param on corresponding Action Script then enable it
				switch (curAction.Type)
				{
				case ActionType.Drop:
					dropAction.activate();
					break;
				case ActionType.Goto:
					ABTable<ABVec> path = ((ABTable<ABVec>)curActionParams [0]);
					Vector3[] pathResult = new Vector3[path.Values.Length];
					for (int i = 0; i < path.Values.Length; i++)
					{
						ABVec abVec = path.Values[i];
						Vector3 vec3 = vec2ToWorld( new Vector2( abVec.X, abVec.Y ) );
						pathResult[i] = vec3;
					}
					gotoAction.Path = pathResult;

					gotoAction.activate();
					break;
				case ActionType.Hit:
					throw new System.NotImplementedException();
					break;
				case ActionType.Hold:
					throw new System.NotImplementedException();
					break;
				case ActionType.Lay:
					ABText castName = ((ABText)curActionParams[0]);
					layAction.CastName = castName.Value;

					layAction.activate();
					break;
				case ActionType.Pick:
					ABRef item = ((ABRef)curActionParams[0]);
					pickAction.Item = Unit_GameObj_Manager.instance.getResource( Mathf.FloorToInt( ((ABScalar)item.GetAttr( "key" )).Value ) );

					pickAction.activate();
					break;
				case ActionType.Spread:
					throw new System.NotImplementedException();
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

                    case ABColor.Color.Cyan:
						traceAction.Color = Color.cyan;
						break;
                    case ABColor.Color.Magenta:
						traceAction.Color = Color.magenta;
						break;
					case ABColor.Color.Yellow:
                        traceAction.Color = new Color(1, 1, 0, 1);
						break;
					}

					// path
					ABTable<ABVec> tracePath = ((ABTable<ABVec>)curActionParams[1]);
					traceAction.Path = new Vector3[tracePath.Values.Length];
					for (int i = 0; i < tracePath.Values.Length; i++)
					{
						ABVec abVec = tracePath.Values[i];
						Vector3 vec3 = vec2ToWorld( new Vector2( abVec.X, abVec.Y ) );
						traceAction.Path[i] = vec3;
					}

					traceAction.activate();
					break;
				case ActionType.Strike:
					ABRef target = ((ABRef)curActionParams [0]);
					if (target != null) {
						strikeAction.Target = Unit_GameObj_Manager.instance.getUnit (Mathf.FloorToInt (((ABScalar)target.GetAttr ("key")).Value));
					}

					strikeAction.activate();
					break;
				case ActionType.Roaming:
					ABScalar angle = ((ABScalar)curActionParams [0]);
					ABScalar dist = ((ABScalar)curActionParams [1]);
					roamingAction.setParams( angle.Value, dist.Value );

					roamingAction.activate();
					break;
				case ActionType.None:
					break;
				}

			}
			catch( System.Exception ex ){
				throw new Action_Exception ( curAction, null, ex.Message );
			}
		}
	}

	public static Vector2 worldToVec2( Vector3 point ){
		return new Vector2 ( point.x, point.z );
	}
	public static Vector3 vec2ToWorld( Vector2 point ){
		return new Vector3 ( point.x, 0, point.y );
	}

	protected GameAction getGameAction( ABAction action ){
		if (action != null) {
			switch (action.Type) {
			case ActionType.Goto:
				return gotoAction;
			case ActionType.Trace:
				return traceAction;
			case ActionType.Lay:
				return layAction;
			case ActionType.Strike:
				return strikeAction;
			case ActionType.Pick:
				return pickAction;
			case ActionType.Drop:
				return dropAction;
			case ActionType.Roaming:
				return roamingAction;
			}
		}
		return null;
	}
}