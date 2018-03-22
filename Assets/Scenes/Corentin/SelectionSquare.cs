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
    public List<GameObject> selectedUnits;

    //The selection squares 4 corner positions
    Vector3 HG, HD, BG, BD;

    public bool MultipleSelection;

    void Start()
    {
        //Desactivate the square selection image
        //selectionSquareImage.gameObject.SetActive(false);
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
        if (Input.GetMouseButtonDown(0)  )// && !EventSystem.current.IsPointerOverGameObject()) 
        {
            //Debug.Log("zizi");
            clickTime = Time.time;

            //We dont yet know if we are drawing a square, but we need the first coordinate in case we do draw a square
            RaycastHit hit;
            //Fire ray from camera
            if (Physics.Raycast(GameObject.Find("Camera").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity)) //200f, 1 << 8))
            {
                //The corner position of the square
                squareStartPos = hit.point;
               //Debug.Log("squarestartpos = " + squareStartPos);
            }

        }
        //Release the mouse button
        if (Input.GetMouseButtonUp(0) )//&& !EventSystem.current.IsPointerOverGameObject())
        {
           // if (!MultipleSelection)
           // {
            //    isClicking = true;
          //  }
            if (Time.time - clickTime <= clickDelay)//&& !EventSystem.current.IsPointerOverGameObject())
            {
                isClicking = true;
            }

            //Select all units within the square if we have created a square
            if (hasCreatedSquare)
            {
                //Debug.Log("square created");
                hasCreatedSquare = false;

                //Deactivate the square selection image
                selectionSquareImage.gameObject.SetActive(false);

                //Clear the list with selected unit
                selectedUnits.Clear();
                //Debug.Log(selectedUnits);

                //Select the units
                for (int i = 0; i < allUnits.Length; i++)
                {
                    GameObject currentUnit = allUnits[i];

                    //Is this unit within the square
                    if (IsWithinPolygon(currentUnit.transform.position))
                    {
                        //Debug.Log("is within polygon");
                        if (currentUnit.GetComponent<MeshRenderer>() != null) {
                            //Shader caca = Shader.(currentUnit.GetComponent<MeshRenderer>().material.shader , Shader.Find("Outlined/Silhouetted Bumped Diffuse") );
                            //currentUnit.GetComponent<MeshRenderer>().material.shader.= Shader.Find("Outlined/Silhouetted Bumped Diffuse");
                            currentUnit.GetComponent<MeshRenderer>().material.shader = Shader.Find("Outlined/Silhouetted Bumped Diffuse");
                        }
                        selectedUnits.Add(currentUnit);
                    }
                    //Otherwise deselect the unit if it's not in the square
                    else
                    {
                        if (currentUnit.GetComponent<MeshRenderer>() != null)
                        {
                            if (currentUnit.GetComponent<ProxyABOperator>())//if(currentUnit.GetType().ToString().Contains("Macro"))
                            {
                                if (currentUnit.GetComponent<ProxyABOperator>().isMacroComposant)
                                {
                                    currentUnit.GetComponent<Renderer>().material.shader = Shader.Find("Specular");
                                    currentUnit.GetComponent<Renderer>().material.SetColor("_SpecColor", Color.red);
                                }
                                else currentUnit.GetComponent<MeshRenderer>().material.shader = Shader.Find("Standard");
                            }
                            else currentUnit.GetComponent<MeshRenderer>().material.shader = Shader.Find("Standard");
                        }
                    }
                }
            }

        }
        //Holding down the mouse button
        if (Input.GetMouseButton(0))
        {
            //isHoldingDown = true;
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
                if (selectedUnits[i].GetComponent<MeshRenderer>() != null)
                {
                    if (selectedUnits[i].GetComponent<ProxyABOperator>()) //if (selectedUnits[i].GetType().ToString().Contains("Macro"))
                    {
                        if (selectedUnits[i].GetComponent<ProxyABOperator>().isMacroComposant)
                        {
                            selectedUnits[i].GetComponent<Renderer>().material.shader = Shader.Find("Specular");
                            selectedUnits[i].GetComponent<Renderer>().material.SetColor("_SpecColor", Color.red);
                        }
                        else selectedUnits[i].GetComponent<MeshRenderer>().material.shader = Shader.Find("Standard");
                    }
                    else selectedUnits[i].GetComponent<MeshRenderer>().material.shader = Shader.Find("Standard");
                }
            }

            //Clear the list with selected units
            selectedUnits.Clear();
            //Debug.Log("la liste est clear");
            
            //Try to select a new unit
            RaycastHit hit;
            //Fire ray from camera
            if (Physics.Raycast(GameObject.Find("Camera").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity)) // 200f))
            {
                if (hit.collider.gameObject.tag == "Selectable")
                {
                    GameObject activeUnit = hit.collider.gameObject;
                    //Set this unit to selected
                    if (activeUnit.GetComponent<MeshRenderer>() != null)
                        activeUnit.GetComponent<MeshRenderer>().material.shader = Shader.Find("Outlined/Silhouetted Bumped Diffuse");
                    //Add it to the list of selected units, which is now just 1 unit
                    selectedUnits.Add(activeUnit);
                }
            }
        }

        //Drag the mouse to select all units within the square
        if (isHoldingDown)
        {
            //Debug.Log("is holding down = " + isHoldingDown);
            //Activate the square selection image
            if (!selectionSquareImage.gameObject.activeInHierarchy)
            {
                selectionSquareImage.gameObject.SetActive(true);
            }

            //Get the latest coordinate of the square
            squareEndPos = Input.mousePosition;
            //squareEndPos = GameObject.Find("Camera").GetComponent<Camera>().WorldToScreenPoint(squareEndPos);
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
                        if (currentUnit.GetComponent<MeshRenderer>() != null)
                            currentUnit.GetComponent<MeshRenderer>().material.shader = Shader.Find("Outlined/Silhouetted Bumped Diffuse");

                    }
                    //Otherwise desactivate
                    else
                    {
                        if (currentUnit.GetComponent<MeshRenderer>() != null)
                        {
                            if (currentUnit.GetComponent<ProxyABOperator>())
                            {
                                if (currentUnit.GetComponent<ProxyABOperator>().isMacroComposant) //if (currentUnit.GetType().ToString().Contains("Macro"))
                                {
                                    currentUnit.GetComponent<Renderer>().material.shader = Shader.Find("Specular");
                                    currentUnit.GetComponent<Renderer>().material.SetColor("_SpecColor", Color.red);
                                }
                                else currentUnit.GetComponent<MeshRenderer>().material.shader = Shader.Find("Standard");
                            }
                            else currentUnit.GetComponent<MeshRenderer>().material.shader = Shader.Find("Standard");
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

        //Debug.Log("unitPOS : " + unitPos);
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
        //Vector3 squareStartScreen = squareStartPos;
        //squareStartScreen.x = squareStartScreen.x - GameObject.Find("Camera").GetComponent<Camera>().orthographicSize;
        //squareStartScreen.y = squareStartScreen.y - GameObject.Find("Camera").GetComponent<Camera>().orthographicSize;

        RectTransform CanvasRect = GameObject.Find("EditeurMCSceneCanvas").GetComponent<RectTransform>();
        squareStartScreen.x = ((squareStartScreen.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f));
        squareStartScreen.y = ((squareStartScreen.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f));
        squareStartScreen.z = 0.0f;

        //squareStartScreen.z = GameObject.Find("Camera").GetComponent<Camera>().transform.position.z;
        //Debug.Log("squareStartScreen : " + squareStartScreen);

        /*Vector3 squareEndScreen = GameObject.Find("Camera").GetComponent<Camera>().WorldToScreenPoint(squareEndPos);
        squareEndScreen.x = ((squareEndScreen.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f));
        squareEndScreen.y = ((squareEndScreen.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f));
        squareEndScreen.z = 0.0f;
        Debug.Log("squareEndScreen : " + squareEndScreen);*/

        squareEndPos = GameObject.Find("Camera").GetComponent<Camera>().ScreenToViewportPoint(squareEndPos);
        squareEndPos.x = ((squareEndPos.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f));
        squareEndPos.y = ((squareEndPos.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f));
        squareEndPos.z = 0.0f;
        //squareEndPos = GameObject.Find("Camera").GetComponent<Camera>().WorldToScreenPoint(squareEndPos);
        //Debug.Log("squareEndPos : " + squareEndPos);

        //Get the middle position of the square
        Vector3 middle = (squareStartScreen + squareEndPos) / 2.0f;
        //Debug.Log("middle : " + middle);
        //Set the middle position of the GUI square
        //selectionSquareImage.position = middle;
        //middle = GameObject.Find("Camera").GetComponent<Camera>().ScreenToWorldPoint(middle);
        //selectionSquareImage.anchoredPosition = middle;
        selectionSquareImage.SetParent(GameObject.Find("EditeurMCSceneCanvas").transform);
        //selectionSquareImage.SetPositionAndRotation(GameObject.Find("EditeurMCSceneCanvas").transform.position, GameObject.Find("EditeurMCSceneCanvas").transform.rotation);
        //middle.x = middle.x / 68;
        //middle.y = middle.y / 24;
        //middle = middle / GameObject.Find("Camera").GetComponent<Camera>().orthographicSize;
        selectionSquareImage.localPosition = middle;
        //selectionSquareImage.position = middle;

        //Change the size of the square
        float sizeX = Mathf.Abs(squareStartScreen.x - squareEndPos.x);
        float sizeY = Mathf.Abs(squareStartScreen.y - squareEndPos.y);

        //Set the size of the square
        selectionSquareImage.sizeDelta = new Vector2(sizeX, sizeY);

        //The problem is that the corners in the 2d square is not the same as in 3d space
        //To get corners, we have to fire a ray from the screen
        //We have 2 of the corner positions, but we don't know which,  
        //so we can figure it out or fire 4 raycasts
        HG = new Vector3(middle.x - sizeX / 2.0f, middle.y + sizeY / 2.0f, 0.0f);
        HD = new Vector3(middle.x + sizeX / 2.0f, middle.y + sizeY / 2.0f, 0.0f);
        BG = new Vector3(middle.x - sizeX / 2.0f, middle.y - sizeY / 2.0f, 0.0f);
        BD = new Vector3(middle.x + sizeX / 2.0f, middle.y - sizeY / 2.0f, 0.0f);
        /*HG.x = ((HG.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f));
        HG.y = ((HG.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f));
        HD.x = ((HD.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f));
        HD.y = ((HD.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f));
        BG.x = ((BG.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f));
        BG.y = ((BG.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f));
        BD.x = ((BD.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f));
        BD.y = ((BD.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f));*/

        HG = HG + new Vector3(GameObject.Find("Main Camera").GetComponent<Camera>().pixelWidth, GameObject.Find("Main Camera").GetComponent<Camera>().pixelHeight, 0.0f) / 2f;
        HD = HD + new Vector3(GameObject.Find("Main Camera").GetComponent<Camera>().pixelWidth, GameObject.Find("Main Camera").GetComponent<Camera>().pixelHeight, 0.0f) / 2f;
        BG = BG + new Vector3(GameObject.Find("Main Camera").GetComponent<Camera>().pixelWidth, GameObject.Find("Main Camera").GetComponent<Camera>().pixelHeight, 0.0f) / 2f;
        BD = BD + new Vector3(GameObject.Find("Main Camera").GetComponent<Camera>().pixelWidth, GameObject.Find("Main Camera").GetComponent<Camera>().pixelHeight, 0.0f) / 2f;

        //Debug.Log("avant HG , HD , BG , BD = " + HG +" "+ HD +" "+ BG +" "+ BD);
        //From screen to world
        RaycastHit hit;
        int i = 0;
        //Fire ray from camera
        /* HG = GameObject.Find("Camera").GetComponent<Camera>().WorldToScreenPoint(HG);
         HD = GameObject.Find("Camera").GetComponent<Camera>().WorldToScreenPoint(HD);
         BG = GameObject.Find("Camera").GetComponent<Camera>().WorldToScreenPoint(BG);
         BD = GameObject.Find("Camera").GetComponent<Camera>().WorldToScreenPoint(BD);
         Debug.Log("apres HG , HD , BG , BD = " + HG + " " + HD + " " + BG + " " + BD);*/

        Ray rayHG = GameObject.Find("Camera").GetComponent<Camera>().ScreenPointToRay(HG);
        Ray rayHD = GameObject.Find("Camera").GetComponent<Camera>().ScreenPointToRay(HD);
        Ray rayBG = GameObject.Find("Camera").GetComponent<Camera>().ScreenPointToRay(BG);
        Ray rayBD = GameObject.Find("Camera").GetComponent<Camera>().ScreenPointToRay(BD);
        if (Physics.Raycast(rayHG, out hit, Mathf.Infinity))// Mathf.Infinity))// 200f, 1 << 8))
        {
            //Debug.DrawRay(rayHG.origin, rayHG.direction * 10, Color.yellow);
            HG = hit.point;
            //HG.x = ((HG.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f));
            //HG.y = ((HG.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f));
            i++;
            //Debug.Log("HG = " + HG);
        }
        if (Physics.Raycast(rayHD, out hit, Mathf.Infinity))//Mathf.Infinity))// 200f, 1 << 8))
        {
            //Debug.DrawRay(rayHD.origin, rayHD.direction * 10, Color.yellow);
            HD = hit.point;
            //HD.x = ((HD.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f));
            //HD.y = ((HD.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f));
            i++;
            //Debug.Log("HD = " + HD);
        }
        if (Physics.Raycast(rayBG, out hit, Mathf.Infinity))// Mathf.Infinity))// 200f, 1 << 8))
        {
            //Debug.DrawRay(rayBG.origin, rayBG.direction * 10, Color.yellow);
            BG = hit.point;
            //BG.x = ((BG.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f));
            //BG.y = ((BG.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f));
            i++;
            //Debug.Log("BG = " + BG);
        }
        if (Physics.Raycast(rayBD, out hit, Mathf.Infinity))// Mathf.Infinity))// 200f, 1 << 8))
        {
            //Debug.DrawRay(rayBD.origin, rayBD.direction * 10, Color.yellow);
            BD = hit.point;
            // BD.x = ((BD.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f));
            //BD.y = ((BD.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f));
            i++;
            //Debug.Log("BD = " + BD);
        }

        //Could we create a square?
        hasCreatedSquare = false;

        //We could find 4 points
        if (i == 4)
        {
            //Display the corners for debug
            //sphere1.position = TL;
            //sphere2.position = TR;
            //sphere3.position = BL;
            //sphere4.position = BR;

            hasCreatedSquare = true;
            //Debug.Log("has created square = " + hasCreatedSquare);
        }
    }
}