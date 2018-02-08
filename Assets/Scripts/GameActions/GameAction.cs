using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameAction : MonoBehaviour {
	// Workaround for script enabling issues
	protected bool activated;

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

	/// <summary>
	/// Gets or sets a value indicating whether this <see cref="GameAction"/> is activated.
	/// </summary>
	/// <value><c>true</c> if activated; otherwise, <c>false</c>.</value>
	public bool Activated {
		get {
			return activated;
		}
		set {
			activated = value;
		}
	}

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
			executeAction();
			coolDownElapsed = false;
			if( coolDownActivate ){
				Invoke("EndCooldown", coolDownTime*actionStepTime);
			} else {
				EndCooldown();
			}
		}
	}

	private void EndCooldown()
	{
		coolDownElapsed = true;
	}

	/// <summary>
	/// Inits the action during Start method.
	/// </summary>
	protected abstract void initAction();

	/// <summary>
	/// Executes the action during Update method (Considers the cooldownTime and activated attributes)
	/// </summary>
	protected abstract void executeAction();
}
