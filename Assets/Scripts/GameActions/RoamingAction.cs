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

		Vector3 curPos = vec2ToWorld (agentAttr.CurPos);
		Vector3 newtarget = vec3ToLumy( GenerateNewTarget( true ) );

		// Create several targets (use the angle of view) and check if they are valid; if no one is valid, generate with an angle of view of 360 degrees
		int MaxTries = 5;
		int tries = MaxTries;
		// a half of tries will consider the angle; the other half will consider 360 degrees
		while( !isCompletePath( curPos, newtarget ) && tries > -MaxTries ){
			newtarget = vec3ToLumy( GenerateNewTarget( tries > 0 ) );
			tries--;
		}

		this.Path = new Vector3[1]{ newtarget };
		changeTargetDemand = false;
	}

	/// <summary>
	/// Generates the new target
	/// </summary>
	/// <returns>The new target.</returns>
	/// <param name="useAngle">If set to <c>true</c> consider the angle. Else, consider 360 degrees.</param>
	protected Vector3 GenerateNewTarget( bool useAngle ){
		Vector3 newtarget = AgentBehavior.vec2ToWorld( agentAttr.CurPos );

		// If params are valid, calculate target
		if (Angle > 0 && Dist > 0) {
			float newAngle = 0;
			Vector3 dir = Vector3.zero;

			// Check current Direction : if direction exists, use angle to calculate. Else, create a random target
			if (this.CurDirection != null && useAngle) {
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
			throw new System.Exception("Roaming : invalid params values");
		}

		return newtarget;
	}
}
