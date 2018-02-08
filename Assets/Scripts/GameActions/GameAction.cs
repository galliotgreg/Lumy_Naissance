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
	private float coolDownTime = 0.1f;

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

		if (coolDownElapsed)
		{
			executeAction();
			coolDownElapsed = false;
			if( coolDownActivate ){
				Invoke("EndCooldown", coolDownTime);
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
