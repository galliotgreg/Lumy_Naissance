using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceScript : MonoBehaviour {

	// GameObject Identification
	[AttrName(Identifier = "key")]
	[SerializeField]
	private int key;

	[AttrName(Identifier = "color")]
	[SerializeField]
	Color32 color;

	[SerializeField]
	private Vector2[] controlPoints;

	[AttrName(Identifier = "points")]
	[SerializeField]
	private Vector2[] visualPoints;

	[AttrName(Identifier = "age")]
	[SerializeField]
	private float age;

	[SerializeField] 
	private float lifeTime;

	[SerializeField]
	private PlayerAuthority authority;

	#region PROPERTIES
	public Color32 Color {
		get {
			return color;
		}
	}

	public Vector2[] ControlPoints {
		get {
			return controlPoints;
		}
	}

	public Vector2[] VisualPoints {
		get {
			return visualPoints;
		}
	}

	public float Age {
		get {
			return age;
		}
	}

	public float LifeTime {
		get {
			return lifeTime;
		}
	}

	public PlayerAuthority Authority {
		get {
			return authority;
		}
	}

	public int Key {
		get {
			return key;
		}
	}
	#endregion

	void Awake(){
		key = this.GetHashCode();
		this.age = 0;
		this.lifeTime = 1;
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		this.age += Time.deltaTime;

		this.lifeTime -= Time.deltaTime; 
		if( lifeTime <= 0 ){ 
			Unit_GameObj_Manager.instance.destroyTrace (this); 
		}
	}

	#region CreateTrace
	void CreateTrace_Points( Color32 color, PlayerAuthority authority, Vector2[] controlPoints, Vector2[] visualPoints, GameObject traceUnitPrefab, float lifeTime = 10 ){
		this.color = color;
		this.authority = authority;
		this.lifeTime = lifeTime;
		this.controlPoints = controlPoints;
		this.visualPoints = visualPoints;

		// create game objects
		if( traceUnitPrefab != null ){
			foreach( Vector2 pos in visualPoints ){
				GameObject traceUnitObject = Instantiate( traceUnitPrefab, AgentBehavior.vec2ToWorld( pos ), this.transform.rotation, this.transform );
				TraceUnitGameObject traceUnit = traceUnitObject.GetComponent<TraceUnitGameObject> ();
				traceUnit.Color = color;
			}
		}
	}

	// One point path
	public void CreateTrace( Color32 color, Vector2 start, Vector2 target, PlayerAuthority authority, GameObject traceUnitPrefab, float lifeTime = 10 ){
		CreateTrace_Points ( color, authority, new Vector2[]{start,target}, traceBetween( start, target ).ToArray(), traceUnitPrefab, lifeTime );
	}

	// more than one point path
	public void CreateTrace( Color32 color, Vector2 start, Vector2[] controlPoints, PlayerAuthority authority, GameObject traceUnitPrefab, float lifeTime = 10 ){
		// trace points
		List<Vector2> result = new List<Vector2> ();

		/*
		// Considering start point
		Vector3[] pointsVec3 = new Vector3[ controlPoints.Length ];
		for (int i = 0; i < controlPoints.Length; i++) {
			pointsVec3 [i] = AgentBehavior.vec2ToWorld ( controlPoints[i] );
		}

		int index = indexClosest ( start, pointsVec3 );
		// Start -> index
		result.AddRange( traceBetween( start, controlPoints[index] ) );
		// index -> index+1
		for (int i = index; i < controlPoints.Length-1; i++) {
			result.AddRange( traceBetween( controlPoints[index], controlPoints[index+1] ) );
		}

		// control points including start
		Vector2[] newControlPoints = new Vector2[controlPoints.Length+1];
		newControlPoints [0] = start;
		for (int i = 0; i < controlPoints.Length; i++) {
			newControlPoints [i + 1] = controlPoints [i];
		}*/

		// Not considering start Point
		for (int i = 0; i < controlPoints.Length-1; i++) {
			result.AddRange( traceBetween( controlPoints[i], controlPoints[i+1] ) );
		}

		CreateTrace_Points ( color, authority, controlPoints, result.ToArray(), traceUnitPrefab, lifeTime );
	}

	List<Vector2> traceBetween(Vector2 posA, Vector2 posB){
		List<Vector2> result = new List<Vector2> ();

		Vector3 posAVec3 = AgentBehavior.vec2ToWorld ( posA );
		Vector3 posBVec3 = AgentBehavior.vec2ToWorld ( posB );

		UnityEngine.AI.NavMeshPath auxpath = new UnityEngine.AI.NavMeshPath();
		UnityEngine.AI.NavMesh.CalculatePath( posAVec3, posBVec3, 1, auxpath );

		for (int i = 0; i < auxpath.corners.Length-1; i++) {
			result.AddRange ( traceRectBetween( auxpath.corners[i], auxpath.corners[i+1] ) );
		}

		return result;
	}
	// create one point to each 1 m
	List<Vector2> traceRectBetween(Vector3 posA, Vector3 posB){
		float distance = (posB - posA).magnitude;
		Vector3 unitaryDirectionVector = (posB - posA).normalized;

		List<Vector2> result = new List<Vector2> ();

		float it = 0;
		while (it < distance) {
			// create a point
			result.Add( AgentBehavior.worldToVec2( posA + unitaryDirectionVector*it ) );
			it += distance/Mathf.Ceil(distance);
		}
		// end Point
		result.Add( AgentBehavior.worldToVec2( posB ) );

		return result;
	}

	/*private int indexClosest( Vector2 pos, Vector3[] _path ){
		int resultIndex = 0;
		UnityEngine.AI.NavMeshPath auxpath = new UnityEngine.AI.NavMeshPath();
		UnityEngine.AI.NavMesh.CalculatePath( pos, _path [resultIndex], UnityEngine.AI.NavMesh.AllAreas, auxpath );

		float resultDistance = GotoAction.pathLength( auxpath );

		for (int i = 1; i < _path.Length; i++) {
			auxpath.ClearCorners ();
			UnityEngine.AI.NavMesh.CalculatePath( pos, _path [i], UnityEngine.AI.NavMesh.AllAreas, auxpath );
			float auxDistance = GotoAction.pathLength( auxpath );

			if ( auxDistance < resultDistance ) {
				resultIndex = i;
				resultDistance = auxDistance;
			}
		}

		return resultIndex;
	}*/
	#endregion
}
