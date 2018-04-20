using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCEditor_BringToFront_Camera : MonoBehaviour {

	[SerializeField]
	Camera canvasCam;

	[SerializeField]
	Canvas canvas;

	public static Camera CanvasCamera;

	// Use this for initialization
	void Start () {
       // canvasCam.transform.position = Camera.main.transform.position;
        canvasCam.transform.position = NavigationManager.instance.GetCurrentCamera().transform.position; 
        CanvasCamera = canvasCam;

		canvas.planeDistance = Mathf.Abs( canvasCam.transform.position.z / 2 );
		//canvas.planeDistance = Mathf.Abs( canvasCam.transform.position.z + 2 );
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
