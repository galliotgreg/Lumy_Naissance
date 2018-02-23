using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCEditor_DialogBoxManager : MonoBehaviour {

	[SerializeField]
	MCEditor_DialogBox_Value valuePrefab;

	// Use this for initialization
	void Start () {
		instantiateValue (new Vector2 (1, 3));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public MCEditor_DialogBox_Value instantiateValue( Vector2 position ){
		return MCEditor_DialogBox_Value.instantiate (valuePrefab, position, this.transform);
	}
}
