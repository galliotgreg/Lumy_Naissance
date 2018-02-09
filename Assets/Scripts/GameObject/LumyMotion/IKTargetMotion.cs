using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKTargetMotion : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Vector2 screenPos = 
                new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 pos = Camera.main.ScreenToWorldPoint(screenPos);
            this.transform.position = new Vector3(pos.x, pos.y, 0f);
        }
    }
}
