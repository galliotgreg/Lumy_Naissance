using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This Script needs to be attach to the Camera which has the focus of the game
/// </summary>
public class CameraRay : MonoBehaviour {


    private Camera camera;
    private AgentScript self;
    private string action;  


    public AgentScript Self
    {
        get
        {
            return self;
        }
    }

    public string Action
    {
        get
        {
            return action;
        }
    }




    // Use this for initialization
    void Start () {
        camera = this.gameObject.GetComponent<Camera>(); 
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetMouseButtonDown (0) && camera != null)
        {
            drawRayOnMouse(); 
        }
	}

    private void drawRayOnMouse()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            if (hit.transform.name == "EmptyComponentPrefab(Clone)")
            {
                GameObject parent = hit.transform.parent.parent.gameObject;
                AgentScript self = parent.GetComponent<AgentScript>();
                action = parent.GetComponent<AgentBehavior>().CurActionType.ToString();
            }
        }
        else
        {
            self = null;
            action = "None";
        }
    }

   
}
