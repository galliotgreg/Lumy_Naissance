using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_Camera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		HandleCameras ( false );
	}

	// Update is called once per frame
	void Update () {
		
	}

	[SerializeField]
	Camera MCcamera;

	float stateRadius = 1;

	public void adjustCamera( List<GameObject> states ){
		if (states.Count > 1) {
			Vector2 min = states [0].transform.position;
			Vector2 max = states [0].transform.position;

			// Obtain min and Max positions
			for (int i = 1; i < states.Count; i++) {
				Vector2 curPos = states [i].transform.position;
				if (min.x > curPos.x) {
					min.x = curPos.x;
				}
				if (max.x < curPos.x) {
					max.x = curPos.x;
				}
				if (min.y > curPos.y) {
					min.y = curPos.y;
				}
				if (max.y < curPos.y) {
					max.y = curPos.y;
				}
			}

			// Set Camera
			float statesHeight = max.y - min.y + stateRadius;
			float statesWidth = max.x - min.x + stateRadius;

			MCcamera.orthographicSize = Mathf.Max (statesHeight, statesWidth);

			// adjust camera position
			MCcamera.transform.position = new Vector3 (min.x + statesWidth / 2f - stateRadius / 2f, min.y + statesHeight / 2f - stateRadius / 2f, MCcamera.transform.position.z);
		} else if (states.Count == 1) {
			MCcamera.orthographicSize = Mathf.Max (2*stateRadius);
			MCcamera.transform.position = new Vector3 ( states[0].transform.position.x, states[0].transform.position.y, MCcamera.transform.position.z);
		}
	}

	#region Enable/Disable Cameras
	void HandleCameras( bool enable ){
		// Disable Cameras
		Camera[] cameras = Camera.allCameras;
		foreach( Camera cam in cameras ){
			if (cam != MCcamera) {
				cam.gameObject.SetActive( enable );
			}
		}
		MCcamera.gameObject.SetActive (!enable);
	}

	void OnDestroy(){
		HandleCameras( true );
	}
	#endregion
}
