using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using System.IO;

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
        cast_name = AppContextManager.instance.ActiveCast.Name + "_behavior_POSITION";

        //string[] path_cutted = cast_name.Split('/');
        //cast_name = path_cutted[path_cutted.Length - 2];
    }
    #endregion

    public enum ToolType
    {
        Selection,
        Hand,
        Undo,
        Redo,
        None
    };

    /*public class UndoableAction
    {
        public GameObject[] impactedNodes;
        public List<Vector3> transform;

        public UndoableAction() {
            //this.impactedNodes = new GameObject[0];
            this.transform = new List<Vector3>();
        }

        public UndoableAction(GameObject[] impactedNodes, List<Vector3> transform)
        {
            this.impactedNodes = impactedNodes;
            this.transform = transform;
        }

        public void Clear()
        {
            //this.impactedNodes = new GameObject[0];
            this.transform.Clear();
        }
    }*/

    public List<GameObject> SelectedNodes = new List<GameObject>();
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
    private Button btn_Undo;
    [SerializeField]
    private Button btn_Redo;

    [SerializeField]
    DropArea centerZone;

    private bool selectionEnCours;

    private Vector3 mpos;
    private bool neverCalculated;
    List<Vector3> DistanceList = new List<Vector3>();

    /*List<UndoableAction> undoableActions = new List<UndoableAction>();
    UndoableAction currentUndoableAction = new UndoableAction();*/

    int id = 0;
    int idmax = 0;
    string cast_name;
    public bool saved = false;
    bool hasBeenAdded = false;

    #region PROPERTIES
    ToolType CurrentTool
    {
        get
        {
            return currentTool;
        }
        set
        {
            currentTool = value;
        }
    }
    #endregion

    private void Start()
    {
        btn_Selection.onClick.AddListener(() => { CurrentTool = ToolType.Selection; CancelInventory(); SelectionSquare.instance.enabled = true; });
        btn_Main.onClick.AddListener(() => { CurrentTool = ToolType.Hand; CancelInventory(); neverCalculated = true; });
        btn_Undo.onClick.AddListener(() => { CurrentTool = ToolType.Undo; CancelInventory(); ToolUndo(); });
        btn_Redo.onClick.AddListener(() => { CurrentTool = ToolType.Redo; CancelInventory(); ToolRedo(); });
        

    }

    private void Update()
    {
        //Mouse Button Press Down
        if (Input.GetMouseButtonDown(0) && !selectionEnCours)
        {
            RaycastHit hitInfo;
            getTarget = ReturnClickedObject(out hitInfo);
            if (getTarget.name == "pin(Clone)" || getTarget.name == "pinOut(Clone)" || getTarget.name == "transition(Clone)")
            {
                SelectionSquare.instance.enabled = false;
                isMouseDragging = false;
            }
            if (centerZone.CanDrop)
            {
                //Hit background
                if (getTarget.name == "STUBS_backgroundCollider")
                {
                    inventory = false;
                    //current tool activated
                    if (CurrentTool == ToolType.Selection)
                    {
                        selectionEnCours = true;
                        SelectionSquare.instance.MultipleSelection = true;

                        SelectionSquare.instance.enabled = true;

                        SelectedNodes = SelectionSquare.instance.selectedUnits;
                    }
                    if (CurrentTool == ToolType.Hand)
                    {
                        SelectionSquare.instance.enabled = false;
                        isMouseDragging = false;
                    }
                }

                if (getTarget.name != "STUBS_backgroundCollider")
                {
                    //hit something selectable (node, state, action...)
                    if (getTarget.tag == "Selectable")
                    {
                        inventory = false;
                        //current tool activated
                        if (CurrentTool == ToolType.Selection)
                        {

                            SelectionSquare.instance.MultipleSelection = false;
                            SelectionSquare.instance.enabled = true;
                            SelectedNodes = SelectionSquare.instance.selectedUnits;

                        }
                        if (CurrentTool == ToolType.Hand)
                        {
                            SelectionSquare.instance.enabled = false;
                            isMouseDragging = true;

                            //currentUndoableAction.Clear();

                            if (neverCalculated)
                            {
                                DistanceList.Clear();
                                mpos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -GameObject.Find("Camera").GetComponent<Camera>().transform.position.z);
                                mpos = GameObject.Find("Camera").GetComponent<Camera>().ScreenToWorldPoint(mpos);
                                neverCalculated = false;

                                //currentUndoableAction.impactedNodes = new GameObject[SelectedNodes.Count];
                                //SelectedNodes.CopyTo(currentUndoableAction.impactedNodes);

                                foreach (GameObject b in SelectedNodes)
                                {
                                    DistanceList.Add(b.transform.position - mpos);
                                    //currentUndoableAction.transform.Add(b.transform.position);
                                    /*currentUndoableAction.transform = new List<Vector3>
                                    {
                                        b.transform.position
                                    };*/
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                // Disable square when inventory is selected
                SelectionSquare.instance.enabled = false;
                SelectionSquare.instance.MultipleSelection = false;
                isMouseDragging = false;
                selectionEnCours = false;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isMouseDragging = false;
            selectionEnCours = false;
            if (saved)
                hasBeenAdded = false;
            saved = false;
        }

        if (CurrentTool == ToolType.Hand )
        {
            if (isMouseDragging)
            {
                ToolMain();
                if (!hasBeenAdded)
                {
                    id++;
                    if (idmax <= id)
                    {
                         idmax = id;
                    }
                    else
                    {
                        string sourceFilePath;
                        for (int id_delete = id; id_delete <= idmax; id_delete++)
                        {
                            sourceFilePath = Application.dataPath + @"\TemporaryBackup\" + cast_name + "_" + id_delete.ToString() + ".csv";
                            File.Delete(sourceFilePath);
                        }
                        idmax = id;
                    }
                    MCEditorManager.instance.Temporary_Save_MC_Position(cast_name, id.ToString());
                    saved = true;
                    Debug.Log("+1");
                    hasBeenAdded = true;
                }

            }
            else
            {
                ToolMain_ConsolidateNodes();
            }
        }

        // Delete selected nodes when DELETE is pressed
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            DeleteNodes();
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

    #region TOOL : HAND
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
                        if (DistanceList[i].x < 0 || DistanceList[i].y < 0)
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

    /// <summary>
    /// After dropping the nodes, adjust the selected nodes to the grid
    /// </summary>
    private void ToolMain_ConsolidateNodes()
    {
        if (SelectedNodes != null && SelectedNodes.Count == 0)
        {
            MCEditorManager.positioningProxy(getTarget.GetComponent<MCEditor_Proxy>());
        }
        else
        {
            if (SelectedNodes != null)
            {
                foreach (GameObject b in SelectedNodes)
                {
                    MCEditorManager.positioningProxy(b.GetComponent<MCEditor_Proxy>());
                }
            }
        }
    }
    #endregion

    #region DELETE
    /// <summary>
    /// Deletes the selected nodes
    /// </summary>
    void DeleteNodes()
    {
        if (SelectedNodes != null && SelectedNodes.Count == 0)
        {
            MCEditorManager.instance.deleteSelectedProxies(new List<MCEditor_Proxy>() { getTarget.GetComponent<MCEditor_Proxy>() });
            getTarget = null;
        }
        else
        {
            if (SelectedNodes != null)
            {
                List<MCEditor_Proxy> selectedProxies = new List<MCEditor_Proxy>();
                foreach (GameObject b in SelectedNodes)
                {
                    selectedProxies.Add(b.GetComponent<MCEditor_Proxy>());
                }
                MCEditorManager.instance.deleteSelectedProxies(selectedProxies);
                SelectedNodes.Clear();
            }
        }
    }
    #endregion

    #region UNDO
    private void ToolUndo()
    {
        /*//List<GameObject> currentImpactedNodes = undoableActions.Peek().impactedNodes;
        int i = 0;
        foreach(GameObject b in undoableActions[undoableActions.Count -1].impactedNodes)
        {
            b.transform.position = undoableActions[undoableActions.Count - 1].transform[i];
            i++;
        }
        undoableActions[undoableActions.Count - 1].Clear();
        //currentImpactedNodes.Clear();*/
        if (id == idmax)
        {
            idmax++;
            MCEditorManager.instance.Temporary_Save_MC_Position(cast_name, idmax.ToString());

        }

        string destinationFolderPath = AppContextManager.instance.ActiveSpecieFolderPath;
        string sourceFilePath = Application.dataPath + @"\TemporaryBackup\" + cast_name + "_" + id.ToString() + ".csv";

        if (id > 0)
        {
            File.Delete(destinationFolderPath + cast_name + ".csv");
            Debug.Log(sourceFilePath);
            File.Delete(destinationFolderPath + cast_name + ".csv.meta");
            File.Copy(sourceFilePath, destinationFolderPath + cast_name + ".csv");
            MCEditorManager.instance.LoadMC_Position();

            id--;
            //File.Delete(sourceFilePath);
        }


    }
    #endregion

    #region REDO
    private void ToolRedo()
    {
        /*//List<GameObject> currentImpactedNodes = undoableActions.Peek().impactedNodes;
        int i = 0;
        foreach(GameObject b in undoableActions[undoableActions.Count -1].impactedNodes)
        {
            b.transform.position = undoableActions[undoableActions.Count - 1].transform[i];
            i++;
        }
        undoableActions[undoableActions.Count - 1].Clear();
        //currentImpactedNodes.Clear();*/
        string destinationFolderPath = AppContextManager.instance.ActiveSpecieFolderPath;
        

        if (id <  idmax )
        {
            id++;
            string sourceFilePath = Application.dataPath + @"\TemporaryBackup\" + cast_name + "_" + id.ToString() + ".csv";
            File.Delete(destinationFolderPath + cast_name + ".csv");
            File.Delete(destinationFolderPath + cast_name + ".csv.meta");
            Debug.Log(sourceFilePath);
            File.Copy(sourceFilePath, destinationFolderPath + cast_name + ".csv");
            MCEditorManager.instance.LoadMC_Position();

            //File.Delete(sourceFilePath);
        }


    }
    #endregion


    public void Inventory()
    {
        inventory = true;
    }
    public void CancelInventory()
    {
        inventory = false;
    }
}

