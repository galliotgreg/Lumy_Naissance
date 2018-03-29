using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// This Script needs to be attach to the Camera which has the focus of the game
/// </summary>
public class CameraRay : MonoBehaviour {

    private Camera camera;
    //Used for Intersection with UI 
    private int fingerID = -1;
    private Color color;
    AgentScript self;    


    // Use this for initialization
    void Start () {
        self = null;
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
        //Draw A ray and test if not on UI 
        if (Physics.Raycast(ray, out hit, 100.0f) && (!EventSystem.current.IsPointerOverGameObject(fingerID)))
        {
            if (hit.transform.name == "EmptyComponentPrefab(Clone)") //If the hit is an EmptyComponentPrefab
            {
                if (self != null) {
                    //Disable SelectionShader
                    self.gameObject.transform.GetChild(2).gameObject.SetActive(false); //Disable Canvas
                }
                GameObject parent = hit.transform.parent.parent.gameObject;           
                self = parent.GetComponent<AgentContext>().Self.GetComponent<AgentScript>();

                //Disable Selection Shader
                self.gameObject.transform.GetChild(2).gameObject.SetActive(true); //Enable Canvas

                //Enable MC Debugger
                MC_Debugger_Manager.instance.activateDebugger(parent.GetComponent<AgentEntity>());
                
                //Set Color 
                ColorPlayer();

               //Enable showing in UI
               InGameUIController.instance.UnitSelected = true;
                //Set the self in UI
                InGameUIController.instance.Self = self; 
               InGameUIController.instance.unitCost(); 
            }
            else
            {   
                if(self != null) {
                    //Disable Selection Shader
                    self.gameObject.transform.GetChild(2).gameObject.SetActive(false);
                }
                //Disable MC Debugger 
                MC_Debugger_Manager.instance.deactivateDebugger();

                //Disable Showing in UI
                InGameUIController.instance.UnitSelected = false;
                InGameUIController.instance.Color = Color.white; 
                InGameUIController.instance.Self = null; 
            }
        }
       
    }

    private void ColorPlayer()
    {
        if (self.GetComponentInParent<AgentContext>().Home.gameObject.GetComponent<HomeScript>().Authority == PlayerAuthority.Player1)
        {
            InGameUIController.instance.Color = Color.blue;
        }
        else if (self.GetComponentInParent<AgentContext>().Home.gameObject.GetComponent<HomeScript>().Authority == PlayerAuthority.Player2)
        {
            InGameUIController.instance.Color = Color.red;
        }
    }
   
}

