using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class MCToolManager : MonoBehaviour
{
    
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
    
    List<GameObject> SelectedNodes = new List<GameObject>();
    public GameObject getTarget;
    string CurrentTool = null;
    bool isMouseDragging;
    bool inventory = false;

    [SerializeField]
    private Button btn_Selection;
    [SerializeField]
    private Button btn_Main;



    private void Start()
    {
        btn_Selection.onClick.AddListener(() => CurrentTool = "Selection");
        btn_Main.onClick.AddListener(() => CurrentTool = "Main");
    }

    private void Update()
    {
        //Mouse Button Press Down
        if (Input.GetMouseButtonDown(0) && inventory == false)
        {
            RaycastHit hitInfo;
            getTarget = ReturnClickedObject(out hitInfo);

            //Hit background
            if (getTarget.name == "STUBS_backgroundCollider")
            {
                inventory = false;
                //current tool activated
                if (CurrentTool == "Selection")
                {
                    Debug.Log("le current tool est selection avec hit background");
                    //GameObject.Find("Camera").GetComponent<SelectionSquare>().hasCreatedSquare = false;
                    GameObject.Find("Camera").GetComponent<SelectionSquare>().enabled = true;
                    SelectedNodes = GameObject.Find("Camera").GetComponent<SelectionSquare>().selectedUnits;
                    Debug.Log("il y a " + SelectedNodes.Count + " nodes selected");
                }
                if (CurrentTool == "Main")
                {
                    GameObject.Find("Camera").GetComponent<SelectionSquare>().enabled = false;
                    isMouseDragging = false;
                }
            }

            if (getTarget != null)
            {
                //hit something selectable (node, state, action...)
                if (getTarget.tag == "Selectable")
                {
                    inventory = false;
                    //current tool activated
                    if (CurrentTool == "Selection")
                    {
                        Debug.Log("le current tool est selection avec hit node");
                        GameObject.Find("Camera").GetComponent<SelectionSquare>().enabled = true;
                        SelectedNodes = GameObject.Find("Camera").GetComponent<SelectionSquare>().selectedUnits;
                    }
                    if (CurrentTool == "Main")
                    {
                        GameObject.Find("Camera").GetComponent<SelectionSquare>().enabled = false;
                        isMouseDragging = true;
                    }
                }
            }
            
        }
        if(Input.GetMouseButtonUp(0))
        {
            isMouseDragging = false;
        }

        if (isMouseDragging) ToolMain();

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
}

