using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// This Script needs to be attach to the Camera which has the focus of the game
/// </summary>
public class CameraRay : MonoBehaviour {

    private Camera camera;
    [SerializeField]
    private AgentScript self;
    private string action;

    private int fingerID = -1;

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
        self = null;
        action = "None"; 
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
        if (Physics.Raycast(ray, out hit, 100.0f) && (!EventSystem.current.IsPointerOverGameObject(fingerID)))
        {
            if (hit.transform.name == "EmptyComponentPrefab(Clone)")
            {
                if (self != null) {
                    self.gameObject.transform.GetChild(2).gameObject.SetActive(false);
                }
                GameObject parent = hit.transform.parent.parent.gameObject;           
                self = parent.GetComponent<AgentContext>().Self.GetComponent<AgentScript>();
                self.gameObject.transform.GetChild(2).gameObject.SetActive(true);
                action = parent.GetComponent<AgentBehavior>().CurActionType.ToString();
                MC_Debugger_Manager.instance.activateDebugger(parent.GetComponent<AgentEntity>());
                InGameUIController.instance.unitCost(); 
            }
            else
            {   
                if(self != null) {
                    self.gameObject.transform.GetChild(2).gameObject.SetActive(false);
                }
                MC_Debugger_Manager.instance.deactivateDebugger();
                
             
                self = null;
                action = "None";
            }
        }
        
    }

   
}

