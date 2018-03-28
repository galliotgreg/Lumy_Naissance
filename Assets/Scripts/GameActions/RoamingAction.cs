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
	private bool changeTargetDemand = true;

	#region PROPERTIES
	public float Angle {
		get {
			return angle;
		}
		protected set {
			angle = value;
		}
	}

	public float Dist {
		get {
			return dist;
		}
		protected set {
			dist = value;
		}
	}

	public void setParams( float _angle, float _dist ){
		// Check if values are positive
		if (_angle > 0 && _dist > 0) {
			// Check if values are not equals
			if( Mathf.Abs( _angle - Angle ) > 0.01f || Mathf.Abs( _dist - Dist ) > 0.01f){
				Angle = _angle;
				Dist = _dist;
				changeTargetDemand = true;
			}
		}
	}
	#endregion

	#region implemented abstract members of GameAction
	protected override void activateAction ()
	{
		newTarget ();
		base.activateAction ();
	}

	protected override void frameBeginAction ()
	{
		// Check if params were changed
		if (changeTargetDemand) {
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

	/// <summary>
	/// Called when the path is unreachable
	/// </summary>
	/// <returns>A new destination</returns>
	protected override Vector3 targetUnreachable( Vector3 currentDestination ){
		newTarget();
		return currentDestination;
	}
	#endregion

	protected void newTarget(){
		Vector3 newtarget = AgentBehavior.vec2ToWorld( agentAttr.CurPos );

		// If params are valid, calculate target
		if (Angle > 0 && Dist > 0) {
			float newAngle = 0;
			Vector3 dir = Vector3.zero;

			// Check current Direction : if direction exists, use angle to calculate. Else, create a random target
			if (this.CurDirection != null) {
				int roundedAngle = Mathf.RoundToInt( Mathf.Abs( Angle ) );
				newAngle = Random.Range ( - roundedAngle/2, roundedAngle/2 );
				dir = Quaternion.Euler(0,newAngle,0) * AgentBehavior.vec2ToWorld(this.CurDirection).normalized;
			} else {
				newAngle = Random.Range ( 1, 360 );
				dir = AgentBehavior.vec2ToWorld (new Vector2 (Mathf.Cos (newAngle * Mathf.Deg2Rad), Mathf.Sin (newAngle * Mathf.Deg2Rad))).normalized;
			}
			newtarget += dir*Dist;
		}else{
			// If params are not valid, keep the unit stopped
		}
			
		this.Path = new Vector3[1]{ newtarget };
		changeTargetDemand = false;
	}
}
