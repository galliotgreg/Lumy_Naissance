using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectionSquare : MonoBehaviour
{
    #region SINGLETON
    /// <summary>
    /// The static instance of the Singleton for external access
    /// </summary>
    public static SelectionSquare instance = null;

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

        selectedUnits = new List<GameObject>();
        selectionSquareImage.gameObject.SetActive(false);
    }
    #endregion
    public GameObject[] allUnits;

    [SerializeField]
    public RectTransform selectionSquareImage;

    Vector3 squareStartPos;
    Vector3 squareEndPos;

    float clickTime = 0f;
    float clickDelay = 0.3f;

    bool hasCreatedSquare;
    bool isClicking;
    bool isHoldingDown;

    [System.NonSerialized]
    public List<GameObject> selectedUnits = null;

    //The selection squares 4 corner positions
    Vector3 HG, HD, BG, BD;

    public bool MultipleSelection;

    Color regularState = new Color(255.0f, 255.0f, 255.0f, 255.0f) / 255.0f; //blanc
    Color regularAction = new Color(133.0f,255.0f, 134.0f, 255.0f) / 255.0f; //vert
    Color regularParam = new Color(251.0f, 255.0f, 0.0f, 255.0f) / 255.0f; //jaune
    Color regularOper = new Color(85.0f, 219.0f, 255.0f, 255.0f) / 255.0f; //bleu 

    void Start()
    {
    }

    void Update()
    {
        allUnits = GameObject.FindGameObjectsWithTag("Selectable");
        SelectUnits();
    }

    void SelectUnits()
    {
        //Are we clicking with left mouse or holding down left mouse

        isClicking = false;
        isHoldingDown = false;
        //Click the mouse button
        if (Input.GetMouseButtonDown(0)  )
        {
            clickTime = Time.time;

            RaycastHit hit;
            //Fire ray from camera
            if (Physics.Raycast(GameObject.Find("Camera").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                //The corner position of the square
                squareStartPos = hit.point;
            }

        }
        //Release the mouse button
        if (Input.GetMouseButtonUp(0) )
        {

            if (Time.time - clickTime <= clickDelay)
            {
                isClicking = true;
            }

            //Select all units within the square if we have created a square
            if (hasCreatedSquare)
            {
                hasCreatedSquare = false;

                //Deactivate the square selection image
                selectionSquareImage.gameObject.SetActive(false);

                //Clear the list with selected unit
                selectedUnits.Clear();

                //Select the units
                for (int i = 0; i < allUnits.Length; i++)
                {
                    GameObject currentUnit = allUnits[i];

                    //Is this unit within the square
                    if (IsWithinPolygon(currentUnit.transform.position))
                    {
                        currentUnit.transform.Find("InternSphere").GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
                        currentUnit.transform.Find("ExternSphere").GetComponent<MeshRenderer>().material.SetColor("_node_4083", Color.red);

                        selectedUnits.Add(currentUnit);
                    }
                    //Otherwise deselect the unit if it's not in the square
                    else
                    {
                        if (currentUnit.GetComponent<ProxyABAction>())
                        {
                            currentUnit.transform.Find("InternSphere").GetComponent<MeshRenderer>().material.SetColor("_Color", regularAction);
                            currentUnit.transform.Find("ExternSphere").GetComponent<MeshRenderer>().material.SetColor("_node_4083", regularAction);
                        }
                        if (currentUnit.GetComponent<ProxyABState>())
                        {
                            currentUnit.transform.Find("InternSphere").GetComponent<MeshRenderer>().material.SetColor("_Color", regularState);
                            currentUnit.transform.Find("ExternSphere").GetComponent<MeshRenderer>().material.SetColor("_node_4083", regularState);
                        }
                        if (currentUnit.GetComponent<ProxyABParam>())
                        {
                            currentUnit.transform.Find("InternSphere").GetComponent<MeshRenderer>().material.SetColor("_Color", regularParam);
                            currentUnit.transform.Find("ExternSphere").GetComponent<MeshRenderer>().material.SetColor("_node_4083", regularParam);
                        }
                        if (currentUnit.GetComponent<ProxyABOperator>())
                        {
                            currentUnit.transform.Find("InternSphere").GetComponent<MeshRenderer>().material.SetColor("_Color", regularOper);
                            currentUnit.transform.Find("ExternSphere").GetComponent<MeshRenderer>().material.SetColor("_node_4083", regularOper);
                        }
                    }
                }
            }
        }
        //Holding down the mouse button
        if (Input.GetMouseButton(0))
        {
            if (Time.time - clickTime > clickDelay)
            {
                isHoldingDown = true;
            }
        }

        //Select one unit with left mouse and deselect all units with left mouse by clicking on what's not a unit
        if (isClicking)
        {
            //Deselect all units
            for (int i = 0; i < selectedUnits.Count; i++)
            {
                if (selectedUnits[i].GetComponent<ProxyABAction>())
                {
                    selectedUnits[i].transform.Find("InternSphere").GetComponent<MeshRenderer>().material.SetColor("_Color", regularAction);
                    selectedUnits[i].transform.Find("ExternSphere").GetComponent<MeshRenderer>().material.SetColor("_node_4083", regularAction);
                }
                if (selectedUnits[i].GetComponent<ProxyABState>())
                {
                    selectedUnits[i].transform.Find("InternSphere").GetComponent<MeshRenderer>().material.SetColor("_Color", regularState);
                    selectedUnits[i].transform.Find("ExternSphere").GetComponent<MeshRenderer>().material.SetColor("_node_4083", regularState);
                }
                if (selectedUnits[i].GetComponent<ProxyABParam>())
                {
                    selectedUnits[i].transform.Find("InternSphere").GetComponent<MeshRenderer>().material.SetColor("_Color", regularParam);
                    selectedUnits[i].transform.Find("ExternSphere").GetComponent<MeshRenderer>().material.SetColor("_node_4083", regularParam);
                }

                if (selectedUnits[i].GetComponent<ProxyABOperator>())
                {
                    selectedUnits[i].transform.Find("InternSphere").GetComponent<MeshRenderer>().material.SetColor("_Color", regularOper);
                    selectedUnits[i].transform.Find("ExternSphere").GetComponent<MeshRenderer>().material.SetColor("_node_4083", regularOper);
                }
            }

            //Clear the list with selected units
            selectedUnits.Clear();
            
            //Try to select a new unit
            RaycastHit hit;
            //Fire ray from camera
            if (Physics.Raycast(GameObject.Find("Camera").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject.tag == "Selectable")
                {
                    GameObject activeUnit = hit.collider.gameObject;

                    activeUnit.transform.Find("InternSphere").GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
                    activeUnit.transform.Find("ExternSphere").GetComponent<MeshRenderer>().material.SetColor("_node_4083", Color.red);

                    selectedUnits.Add(activeUnit);
                }
            }
        }

        //Drag the mouse to select all units within the square
        if (isHoldingDown)
        {
            selectedUnits.Clear();

            //Activate the square selection image
            if (!selectionSquareImage.gameObject.activeInHierarchy)
            {
                selectionSquareImage.gameObject.SetActive(true);
            }

            //Get the latest coordinate of the square
            squareEndPos = Input.mousePosition;

            //Display the selection with a GUI image
            DisplaySquare();

            //Highlight the units within the selection square, but don't select the units
            if (hasCreatedSquare)
            {
                for (int i = 0; i < allUnits.Length; i++)
                {
                    GameObject currentUnit = allUnits[i];
                    
                    //Is this unit within the square
                    if (IsWithinPolygon(currentUnit.transform.position))
                    {
                        currentUnit.transform.Find("InternSphere").GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
                        currentUnit.transform.Find("ExternSphere").GetComponent<MeshRenderer>().material.SetColor("_node_4083", Color.red);
                    }
                    //Otherwise desactivate
                    else
                    {
                        if (currentUnit.GetComponent<ProxyABAction>())
                        {
                            currentUnit.transform.Find("InternSphere").GetComponent<MeshRenderer>().material.SetColor("_Color", regularAction);
                            currentUnit.transform.Find("ExternSphere").GetComponent<MeshRenderer>().material.SetColor("_node_4083", regularAction);
                        }
                        if (currentUnit.GetComponent<ProxyABState>())
                        {
                            currentUnit.transform.Find("InternSphere").GetComponent<MeshRenderer>().material.SetColor("_Color", regularState);
                            currentUnit.transform.Find("ExternSphere").GetComponent<MeshRenderer>().material.SetColor("_node_4083", regularState);
                        }
                        if (currentUnit.GetComponent<ProxyABParam>())
                        {
                            currentUnit.transform.Find("InternSphere").GetComponent<MeshRenderer>().material.SetColor("_Color", regularParam);
                            currentUnit.transform.Find("ExternSphere").GetComponent<MeshRenderer>().material.SetColor("_node_4083", regularParam);
                        }
                        if (currentUnit.GetComponent<ProxyABOperator>())
                        {
                            currentUnit.transform.Find("InternSphere").GetComponent<MeshRenderer>().material.SetColor("_Color", regularOper);
                            currentUnit.transform.Find("ExternSphere").GetComponent<MeshRenderer>().material.SetColor("_node_4083", regularOper);
                        }
                    }
                }
            }
        }
    }

    //Is a unit within a polygon determined by 4 corners
    bool IsWithinPolygon(Vector3 unitPos)
    {
        bool isWithinPolygon = false;

        //The polygon forms 2 triangles, so we need to check if a point is within any of the triangles
        //Triangle 1: TL - BL - TR
        if (IsWithinTriangle(unitPos, HG, BG, HD))
        {
            return true;
        }

        //Triangle 2: TR - BL - BR
        if (IsWithinTriangle(unitPos, HD, BG, BD))
        {
            return true;
        }
        return isWithinPolygon;
    }

    float Sign(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        return (p1.x - p3.x) * (p2.y - p3.y) - (p2.x - p3.x) * (p1.y - p3.y);
    }

    //Is a point within a triangle
    bool IsWithinTriangle(Vector3 p, Vector3 p1, Vector3 p2, Vector3 p3)
    {        
        bool b1, b2, b3;

        b1 = Sign(p, p1, p2) < 0.0f;
        b2 = Sign(p, p2, p3) < 0.0f;
        b3 = Sign(p, p3, p1) < 0.0f;

        return ((b1 == b2) && (b2 == b3));
    }

    void DisplaySquare()
    {
        Vector3 squareStartScreen = GameObject.Find("Camera").GetComponent<Camera>().WorldToViewportPoint(squareStartPos);

        RectTransform CanvasRect = GameObject.Find("EditeurMCSceneCanvas").GetComponent<RectTransform>();
        squareStartScreen.x = ((squareStartScreen.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f));
        squareStartScreen.y = ((squareStartScreen.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f));
        squareStartScreen.z = 0.0f;

        squareEndPos = GameObject.Find("Camera").GetComponent<Camera>().ScreenToViewportPoint(squareEndPos);
        squareEndPos.x = ((squareEndPos.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f));
        squareEndPos.y = ((squareEndPos.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f));
        squareEndPos.z = 0.0f;

        Vector3 middle = (squareStartScreen + squareEndPos) / 2.0f;
        selectionSquareImage.SetParent(GameObject.Find("EditeurMCSceneCanvas").transform);
        selectionSquareImage.localPosition = middle;

        //Change the size of the square
        float sizeX = Mathf.Abs(squareStartScreen.x - squareEndPos.x);
        float sizeY = Mathf.Abs(squareStartScreen.y - squareEndPos.y);

        //Set the size of the square
        selectionSquareImage.sizeDelta = new Vector2(sizeX, sizeY);


        HG = new Vector3(middle.x - sizeX / 2.0f, middle.y + sizeY / 2.0f, 0.0f);
        HD = new Vector3(middle.x + sizeX / 2.0f, middle.y + sizeY / 2.0f, 0.0f);
        BG = new Vector3(middle.x - sizeX / 2.0f, middle.y - sizeY / 2.0f, 0.0f);
        BD = new Vector3(middle.x + sizeX / 2.0f, middle.y - sizeY / 2.0f, 0.0f);


        HG = HG + new Vector3(GameObject.Find("Main Camera").GetComponent<Camera>().pixelWidth, GameObject.Find("Main Camera").GetComponent<Camera>().pixelHeight, 0.0f) / 2f;
        HD = HD + new Vector3(GameObject.Find("Main Camera").GetComponent<Camera>().pixelWidth, GameObject.Find("Main Camera").GetComponent<Camera>().pixelHeight, 0.0f) / 2f;
        BG = BG + new Vector3(GameObject.Find("Main Camera").GetComponent<Camera>().pixelWidth, GameObject.Find("Main Camera").GetComponent<Camera>().pixelHeight, 0.0f) / 2f;
        BD = BD + new Vector3(GameObject.Find("Main Camera").GetComponent<Camera>().pixelWidth, GameObject.Find("Main Camera").GetComponent<Camera>().pixelHeight, 0.0f) / 2f;

        RaycastHit hit;
        int i = 0;
        //Fire ray from camera

        Ray rayHG = GameObject.Find("Camera").GetComponent<Camera>().ScreenPointToRay(HG);
        Ray rayHD = GameObject.Find("Camera").GetComponent<Camera>().ScreenPointToRay(HD);
        Ray rayBG = GameObject.Find("Camera").GetComponent<Camera>().ScreenPointToRay(BG);
        Ray rayBD = GameObject.Find("Camera").GetComponent<Camera>().ScreenPointToRay(BD);
        if (Physics.Raycast(rayHG, out hit, Mathf.Infinity))
        {
            HG = hit.point;
            i++;
        }
        if (Physics.Raycast(rayHD, out hit, Mathf.Infinity))
        {
            HD = hit.point;
            i++;
        }
        if (Physics.Raycast(rayBG, out hit, Mathf.Infinity))
        {
            BG = hit.point;
            i++;
        }
        if (Physics.Raycast(rayBD, out hit, Mathf.Infinity))
        {
            BD = hit.point;
            i++;
        }

        //Could we create a square?
        hasCreatedSquare = false;

        //We could find 4 points
        if (i == 4)
        {
            hasCreatedSquare = true;
        }
    }
}