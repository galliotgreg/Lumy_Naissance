using System.Collections;
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
        neverCalculated = true;
    }
	#endregion
    
	public enum ToolType{
		Selection,
		Hand,
		None
	};

    public  List<GameObject> SelectedNodes = new List<GameObject>();
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

    private bool selectionEnCours;

    private Vector3 mpos;
    private bool neverCalculated;
    List<Vector3> DistanceList = new List<Vector3>();

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
		btn_Selection.onClick.AddListener(() => {CurrentTool = ToolType.Selection; CancelInventory(); SelectionSquare.instance.enabled = true; } );
		btn_Main.onClick.AddListener(() => {CurrentTool = ToolType.Hand; CancelInventory(); neverCalculated = true; } );
    }

    private void Update()
    {
        //Mouse Button Press Down
        if (Input.GetMouseButtonDown(0) && !selectionEnCours)
        {
			RaycastHit hitInfo;
			getTarget = ReturnClickedObject (out hitInfo);
            if(getTarget.name == "pin(Clone)" || getTarget.name ==  "pinOut(Clone)" || getTarget.name == "transition(Clone)")
            {
                SelectionSquare.instance.enabled = false;
                isMouseDragging = false;
            }
			if( centerZone.CanDrop ){
			//if (!inventory) {
				//Hit background
				if (getTarget.name == "STUBS_backgroundCollider") //getTarget == null || 
                {
				//if (getTarget.name == "STUBS_backgroundCollider") {
					inventory = false;
					//current tool activated
					if (CurrentTool == ToolType.Selection) {
                        //Debug.Log ("le current tool est selection avec hit background");
                        
                        selectionEnCours = true;
                        SelectionSquare.instance.MultipleSelection = true;

                        SelectionSquare.instance.enabled = true;
                        
                        SelectedNodes = SelectionSquare.instance.selectedUnits;
                        //GameObject.Find ("Camera").GetComponent<SelectionSquare> ().enabled = true;
                        //SelectedNodes = GameObject.Find ("Camera").GetComponent<SelectionSquare> ().selectedUnits;
                        //Debug.Log ("il y a " + SelectedNodes.Count + " nodes selected");
					}
					if (CurrentTool == ToolType.Hand) {
                        //GameObject.Find ("Camera").GetComponent<SelectionSquare> ().enabled = false;
                        SelectionSquare.instance.enabled = false;
                        isMouseDragging = false;
                    }
				}

				if (getTarget.name != "STUBS_backgroundCollider") //(getTarget != null)
                {
					//hit something selectable (node, state, action...)
					if (getTarget.tag == "Selectable") {
						inventory = false;
						//current tool activated
						if (CurrentTool == ToolType.Selection) {
 
                            //selectionEnCours = true;
                            SelectionSquare.instance.MultipleSelection = false;
                            SelectionSquare.instance.enabled = true;
                            SelectedNodes = SelectionSquare.instance.selectedUnits;
                            //GameObject.Find ("Camera").GetComponent<SelectionSquare> ().enabled = true;
                            //SelectedNodes = GameObject.Find ("Camera").GetComponent<SelectionSquare> ().selectedUnits;
                        }
                        if (CurrentTool == ToolType.Hand) {
                            //GameObject.Find ("Camera").GetComponent<SelectionSquare> ().enabled = false;
                            SelectionSquare.instance.enabled = false;
                            isMouseDragging = true;

                            
                            if (neverCalculated)
                            {
                                DistanceList.Clear();
                                mpos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -GameObject.Find("Camera").GetComponent<Camera>().transform.position.z);
                                mpos = GameObject.Find("Camera").GetComponent<Camera>().ScreenToWorldPoint(mpos);
                                neverCalculated = false;
                                foreach (GameObject b in SelectedNodes)
                                {
                                    DistanceList.Add(b.transform.position - mpos);
                                }
                            }
                        }
					}
				}
			} else {
                // Disable square when inventory is selected
                //GameObject.Find ("Camera").GetComponent<SelectionSquare> ().enabled = false;
                SelectionSquare.instance.enabled = false;
                SelectionSquare.instance.MultipleSelection = false;
                isMouseDragging = false;
                selectionEnCours = false;
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            isMouseDragging = false;
            selectionEnCours = false;
        }

        if (isMouseDragging && CurrentTool == ToolType.Hand)
        {
            ToolMain();
        }

    }

    //Method to Return Clicked Object
    GameObject ReturnClickedObject(out RaycastHit hit)
    {
        GameObject target = null;
        Ray ray = GameObject.Find("Camera").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
        {
            //Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
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
                if (SelectedNodes != null && SelectedNodes.Contains(getTarget))
                {
                    int i = 0;
                    foreach (GameObject b in SelectedNodes)
                    {
                        //update targets current postions.
                        if(DistanceList[i].x < 0 || DistanceList[i].y < 0 )
                        {
                            DistanceList[i].Set(DistanceList[i].x * -1.0f, DistanceList[i].y * -1.0f, DistanceList[i].z);
                        }
                        b.transform.position = currentPosition + DistanceList[i];
                        i++;

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

