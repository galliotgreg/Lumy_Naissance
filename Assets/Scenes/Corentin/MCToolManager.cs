﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class MCToolManager : MonoBehaviour
{
	#region SINGLETON
    /// <summary>
    /// The static instance of the Singleton for external access
    /// </summary>
    public static MCToolManager instance = null;

    /// <summary>
    /// Enforce Singleton properties
    /// </summary>
    void Awake()
    {
        //Check if instance already exists and set it to this if not
        if (instance == null)
        {
            instance = this;
        }

        //Enforce the unicity of the Singleton
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
	#endregion
    
	public enum ToolType{
		Selection,
		Hand,
		None
	};

    List<GameObject> SelectedNodes = new List<GameObject>();
    public GameObject getTarget;
	[SerializeField]
	ToolType currentTool = ToolType.None;
    bool isMouseDragging;
	[SerializeField]
    bool inventory = false;

    [SerializeField]
    private Button btn_Selection;
    [SerializeField]
    private Button btn_Main;

	[SerializeField]
	DropArea centerZone;

	#region PROPERTIES
	ToolType CurrentTool {
		get {
			return currentTool;
		}
		set {
			currentTool = value;
		}
	}
	#endregion

    private void Start()
    {
		btn_Selection.onClick.AddListener(() => {CurrentTool = ToolType.Selection; CancelInventory();} );
		btn_Main.onClick.AddListener(() => {CurrentTool = ToolType.Hand; CancelInventory();} );
    }

    private void Update()
    {
        //Mouse Button Press Down
        if (Input.GetMouseButtonDown(0))
        {
			RaycastHit hitInfo;
			getTarget = ReturnClickedObject (out hitInfo);

			if( centerZone.CanDrop ){
			//if (!inventory) {
				//Hit background
				if (getTarget == null) {
				//if (getTarget.name == "STUBS_backgroundCollider") {
					inventory = false;
					//current tool activated
					if (CurrentTool == ToolType.Selection) {
						Debug.Log ("le current tool est selection avec hit background");
						GameObject.Find ("Camera").GetComponent<SelectionSquare> ().enabled = true;
						SelectedNodes = GameObject.Find ("Camera").GetComponent<SelectionSquare> ().selectedUnits;
						Debug.Log ("il y a " + SelectedNodes.Count + " nodes selected");
					}
					if (CurrentTool == ToolType.Hand) {
						GameObject.Find ("Camera").GetComponent<SelectionSquare> ().enabled = false;
						isMouseDragging = false;
					}
				}

				if (getTarget != null) {
					//hit something selectable (node, state, action...)
					if (getTarget.tag == "Selectable") {
						inventory = false;
						//current tool activated
						if (CurrentTool == ToolType.Selection) {
							Debug.Log ("le current tool est selection avec hit node");
							GameObject.Find ("Camera").GetComponent<SelectionSquare> ().enabled = true;
							SelectedNodes = GameObject.Find ("Camera").GetComponent<SelectionSquare> ().selectedUnits;
						}
						if (CurrentTool == ToolType.Hand) {
							GameObject.Find ("Camera").GetComponent<SelectionSquare> ().enabled = false;
							isMouseDragging = true;
						}
					}
				}
			} else {
				// Disable square when inventory is selected
				GameObject.Find ("Camera").GetComponent<SelectionSquare> ().enabled = false;
				isMouseDragging = false;
			}
        }

        if(Input.GetMouseButtonUp(0))
        {
            isMouseDragging = false;
        }

		if (isMouseDragging && CurrentTool == ToolType.Hand) ToolMain();

    }

    //Method to Return Clicked Object
    GameObject ReturnClickedObject(out RaycastHit hit)
    {
        GameObject target = null;
        Ray ray = GameObject.Find("Camera").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
        {
            target = hit.collider.gameObject;
        }
        return target;
    }

    private void ToolMain()
    {
        //Mouse moving
        if (isMouseDragging)
        {
            //tracking mouse pos
            Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -GameObject.Find("Camera").GetComponent<Camera>().transform.position.z);

            //converting screen pos => world pos.
            Vector3 currentPosition = GameObject.Find("Camera").GetComponent<Camera>().ScreenToWorldPoint(currentScreenSpace);

            if (SelectedNodes != null && SelectedNodes.Count == 0)
            {
                getTarget.transform.position = currentPosition;
            }

            else
            {
                Debug.Log("il y a plusieurs nodes à bouger");
                if (SelectedNodes != null && SelectedNodes.Contains(getTarget))
                {
                    foreach (GameObject b in SelectedNodes)
                    {
                        //update target current postion.
                        b.transform.position = currentPosition;
                    }
                }
            }
        }
    }

    public void Inventory()
    {
        inventory = true;
    }
	public void CancelInventory()
	{
		inventory = false;
	}
}

