using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamingAction : GotoAction {

	// Params
	[SerializeField]
	private float angle;
	[SerializeField]
	private float dist;

	[SerializeField]
	private Vector3 currentTarget;
	[SerializeField]
	private bool targetConsumed;

	#region PROPERTIES
	public float Angle {
		get {
			return angle;
		}
		set {
			angle = value;
		}
	}

	public float Dist {
		get {
			return dist;
		}
		set {
			dist = value;
		}
	}
	#endregion

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	#region implemented abstract members of GameAction

	protected override void initAction ()
	{
		targetConsumed = true;
		base.initAction ();
	}

	protected override void activateAction ()
	{
		newTarget ();
		base.activateAction ();
	}

	protected override void frameBeginAction ()
	{
		if (targetConsumed) {
			newTarget ();
		}
		base.frameBeginAction ();
	}

	#endregion

	#region GOTOaction
	/// <summary>
	/// Called when one point in the path is reached.
	/// </summary>
	/// <param name="index">Index of the reached point in the path</param>
	protected override void targetReached( int index ){
		// Generate new point
		newTarget();
	}
	#endregion

	protected void newTarget(){
		this.Path = new Vector3[1]{ new Vector3() };
		targetConsumed = false;
	}
}
