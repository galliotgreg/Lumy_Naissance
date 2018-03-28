using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameAction : MonoBehaviour {
	
	// a value indicating whether this <see cref="GameAction"/> is activated.
	protected bool activated = false;

	private bool coolDownElapsed = true;

	[SerializeField]
	private bool coolDownActivate = true;
	[SerializeField]
	/// <summary>
	/// Factor to be applied to the cooldown (based on the GameAction's internal value for the cooldown action)
	/// </summary>
	private float coolDownTime = 1f;

	private float actionStepTime = 1;	// Seconds used as basis for the actions (ie 1 action per second)

	/// <summary>
	/// Gets or sets a value indicating whether this <see cref="GameAction"/> cool down is activated.
	/// </summary>
	/// <value><c>true</c> if cool down is activated; otherwise, <c>false</c>.</value>
	public bool CoolDownActivate {
		get {
			return coolDownActivate;
		}
		set {
			coolDownActivate = value;
		}
	}

	/// <summary>
	/// Gets or sets the cool down time.
	/// </summary>
	/// <value>The cool down time.</value>
	public float CoolDownTime {
		get {
			return coolDownTime;
		}
		set {
			coolDownTime = value;
		}
	}

	//private bool CoolDownAuthorized = true;		// Indicates if the cooldown authorizes the execution of the action
	//private bool firstExecution = true;			// Indicates the first execution of the action

	/// <summary>
	/// The agent entity.
	/// </summary>
	protected AgentEntity agentEntity;
	/// <summary>
	/// The agent script.
	/// </summary>
	protected AgentScript agentAttr;

	// Use this for initialization
	void Start () {
		agentEntity = GetComponent<AgentEntity>();
		agentAttr = agentEntity.Context.Model;
		this.initAction();
	}

	// Update is called once per frame
	void Update () {
		if (!activated)
		{
			return;
		}

		if (coolDownElapsed && coolDownTime > 0)
		{
			bool activeCooldown = executeAction ();
			coolDownElapsed = false;
			if ( coolDownActivate && activeCooldown ) {
				Invoke ("EndCooldown", coolDownTime * actionStepTime);
			} else {
				EndCooldown ();
			}
		}
	}

	private void EndCooldown()
	{
		coolDownElapsed = true;
		cooldownFinishAction ();

		//CoolDownAuthorized = true;
		//CoolDownAuthorized = !firstExecution; // As the action is executed on FrameBeginAction, and the authorization is given by ExecuteAction (which is executed by default), it avoids the incorrect authorization during the first execution
		//firstExecution = false;
	}

	public void activate(){
		if (!activated) {
			activated = true;
			activateAction ();
		}
	}
	public void deactivate(){
		if (activated) {
			activated = false;
			deactivateAction ();
		}
	}

	public void frameBegin(){
		if (activated) {
			frameBeginAction ();
			if (coolDownElapsed) {
				//this.CoolDownAuthorized = false;
				frameBeginAction_CooldownAuthorized ();
			}
		}
	}

	public void frameEnd(){
		if (activated) {
			frameEndAction ();
		}
	}

	/// <summary>
	/// Inits the action during Start method.
	/// </summary>
	protected abstract void initAction();

	/// <summary>
	/// Executes the action during Update method (Considers the cooldownTime and activated attributes)
	/// </summary>
	/// <returns>if the cooldown must be activated</returns>
	protected abstract bool executeAction();

	/// <summary>
	/// Called at the activation of an activity (when it is reached in the behavior tree)
	/// </summary>
	protected abstract void activateAction();

	/// <summary>
	/// Called at the deactivation of an activity (when it is no more reached in the behavior tree)
	/// </summary>
	protected abstract void deactivateAction();

	/// <summary>
	/// Called at the moment where a the cooldown is finished
	/// </summary>
	protected abstract void cooldownFinishAction();

	/// <summary>
	/// Called at the start of each frame that the action is activated
	/// </summary>
	protected abstract void frameBeginAction();

	/// <summary>
	/// Called at the start of each frame that the action is activated IF the cooldown authorizes it
	/// </summary>
	protected abstract void frameBeginAction_CooldownAuthorized();

	/// <summary>
	/// Called when an action is deactivated.
	/// </summary>
	protected abstract void frameEndAction();
}
