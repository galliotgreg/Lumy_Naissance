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
            if(hit.transform.name == "EmptyComponentPrefab(Clone)")
            {
                GameObject parent = hit.transform.parent.parent.gameObject;
                AgentScript self = parent.GetComponent<AgentScript>();
                action = parent.GetComponent<AgentBehavior>().CurActionType.ToString();
                Debug.Log(action);
            }
        }

    }

    //TODO Move to DebugManager
    private void UnitStats ()
    {
        AgentScript unitSelf = self; //get Self from the CameraRay 
        float vitality = self.Vitality;
        float visionRange = self.VisionRange;
        float vitalityMax = self.VitalityMax;
        float strength = self.Strength;
        float pickRange = self.PickRange;
        float atkRange = self.AtkRange;
        float actSpeed = self.ActSpd;
        float moveSpeed = self.MoveSpd;
        float nbItemMax = self.NbItemMax;
        float nbItem = self.NbItem;
        float layTimeCost = self.LayTimeCost;
        float stamina = self.Stamina;
        string cast = self.Cast; 
    }

    //TODO Move to DebugManager 
    private void getCurAction()
    {
        //Warning Real State from the Action.
        //Maybe make a traduction for more visibility. 
        string currentAction = action;  //Get Action from the Camera Ray
    }
}
