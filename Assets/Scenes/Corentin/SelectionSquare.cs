using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class SelectionSquare : MonoBehaviour
{
    //Add all units in the scene to this array
    public GameObject[] allUnits;

    [SerializeField]
    public RectTransform selectionSquareImage;

    Vector3 squareStartPos;
    Vector3 squareEndPos;

    float clickTime = 0f;
    public float clickDelay = 0.3f;

    bool hasCreatedSquare;

    //The materials
    public Material normalMaterial;
    public Material highlightMaterial;
    public Material selectedMaterial;

    [System.NonSerialized]
    public List<GameObject> selectedUnits = new List<GameObject>();

    //The selection squares 4 corner positions
    Vector3 HG, HD, BG, BD;
   
    void Start()
    {
        //Desactivate the square selection image
        selectionSquareImage.gameObject.SetActive(false);
        
    }

    void Update()
    {
        SelectUnits();
        allUnits = GameObject.FindGameObjectsWithTag("Selectable");
    }

    void SelectUnits()
    {
        //Are we clicking with left mouse or holding down left mouse
        bool isClicking = false;
        bool isHoldingDown = false;

        //Click the mouse button
        if (Input.GetMouseButtonDown(0))
        {
            clickTime = Time.time;

            //We dont yet know if we are drawing a square, but we need the first coordinate in case we do draw a square
            RaycastHit hit;
            //Fire ray from camera
            if (Physics.Raycast(GameObject.Find("Camera").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))  //Mathf.Infinity)) //Mathf.Infinity)) //200f, 1 << 8))
            {
                //The corner position of the square
                squareStartPos = hit.point;
                //Debug.Log("squarestartpos = " + squareStartPos);
            }
        }
        //Release the mouse button
        if (Input.GetMouseButtonUp(0))
        {
           // selectionSquareImage.gameObject.SetActive(false);
            if (Time.time - clickTime <= clickDelay)
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

                //Select the units
                for (int i = 0; i < allUnits.Length; i++)
                {
                    GameObject currentUnit = allUnits[i];

                    //Is this unit within the square
                    if (IsWithinPolygon(currentUnit.transform.position))
                    {
                        Debug.Log("is within polygon");
                        if (currentUnit.GetComponent<MeshRenderer>() != null)
                            currentUnit.GetComponent<MeshRenderer>().material.color /= 0.95f;
	                    //Debug.Log("is within polygon");
                        currentUnit.GetComponent<MeshRenderer>().material = selectedMaterial;

                        selectedUnits.Add(currentUnit);
                    }
                    //Otherwise deselect the unit if it's not in the square
                    /*else
                    {
                        if (currentUnit.GetComponent<MeshRenderer>() != null)
                            currentUnit.GetComponent<MeshRenderer>().material = normalMaterial;
                    }*/
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
                selectedUnits[i].GetComponent<MeshRenderer>().material = normalMaterial;
            }

            //Clear the list with selected units
            selectedUnits.Clear();

            //Try to select a new unit
            RaycastHit hit;
            //Fire ray from camera
            if (Physics.Raycast(GameObject.Find("Camera").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity)) // 200f))
            {
             
                    GameObject activeUnit = hit.collider.gameObject;
                //Set this unit to selected
                if (activeUnit.GetComponent<MeshRenderer>() != null)
                    activeUnit.GetComponent<MeshRenderer>().material.color /= 0.95f;
                    //Add it to the list of selected units, which is now just 1 unit
                    selectedUnits.Add(activeUnit);
                
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
                            currentUnit.GetComponent<MeshRenderer>().material.color *= 0.95f;

                        //Debug.Log("on change en vert");
                        currentUnit.GetComponent<MeshRenderer>().material = highlightMaterial;
                    }
                    //Otherwise desactivate
                    /*else
                    {
                        if (currentUnit.GetComponent<MeshRenderer>() != null)
                            currentUnit.GetComponent<MeshRenderer>().material = normalMaterial;
                    }*/
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

        Debug.Log("unitPOS : " + unitPos);
        if (IsWithinTriangle(unitPos, HG, BG, HD))
        {
            return true;
        }

        //Triangle 2: TR - BL - BR
        if (IsWithinTriangle(unitPos, HD, BG, BD))
        {
            return true;
        }
        
        //Debug.Log("iswithinPolygon = " + isWithinPolygon);
        return isWithinPolygon;
    }

    float Sign(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        return (p1.x - p3.x) * (p2.y - p3.y) - (p2.x - p3.x) * (p1.y - p3.y);
    }

    //Is a point within a triangle
    bool IsWithinTriangle(Vector3 p, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        /* bool isWithinTriangle = false;
         Debug.Log("unitPOS : " + p);
         Debug.Log("HG : " + p1);
         Debug.Log("BG : " + p2);
         Debug.Log("HD : " + p3);

         float denominator = ((p2.y - p3.y) * (p1.x - p3.x) + (p3.x - p2.x) * (p1.y - p3.y));

         float a = ((p2.y - p3.y) * (p.x - p3.x) + (p3.x - p2.x) * (p.y - p3.y)) / denominator;
         float b = ((p3.y - p1.y) * (p.x - p3.x) + (p1.x - p3.x) * (p.y - p3.y)) / denominator;
         float c = 1 - a - b;
         //Debug.Log(a + " " + b + " " + c);

         //The point is within the triangle if 0 <= a <= 1 and 0 <= b <= 1 and 0 <= c <= 1
         //if (a >= 0f && a <= 1f && b >= 0f && b <= 1f && c >= 0f && c <= 1f)
         if (a >= -1f && a <= 1f && b >= 0-1f && b <= 1f && c >= -1f && c <= 1f)
         {
             Debug.Log("is withintriangle");
             isWithinTriangle = true;
         }

         return isWithinTriangle;*/
        /*Vector2 v0 = p2 - p3;
        Vector2 v1 = p1 - p3;
        Vector2 v2 = p - p3;
        float dot00 = Vector2.Dot(v0, v0);
        float dot01 = Vector2.Dot(v0, v1);
        float dot02 = Vector2.Dot(v0, v2);
        float dot11 = Vector2.Dot(v1, v1);
        float dot12 = Vector2.Dot(v1, v2);
        float invDenom = 1.0f / (dot00 * dot11 - dot01 * dot01);
        float u = (dot11 * dot02 - dot01 * dot12) * invDenom;
        float v = (dot00 * dot12 - dot01 * dot02) * invDenom;
        return ((u > 0.0f) && (v > 0.0f) && (u + v < 1.0f));*/
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

        Debug.Log("avant HG , HD , BG , BD = " + HG +" "+ HD +" "+ BG +" "+ BD);
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
            Debug.DrawRay(rayHG.origin, rayHG.direction * 10, Color.yellow);
            HG = hit.point;
            //HG.x = ((HG.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f));
            //HG.y = ((HG.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f));
            i++;
            Debug.Log("HG = " + HG);
        }
        if (Physics.Raycast(rayHD, out hit, Mathf.Infinity))//Mathf.Infinity))// 200f, 1 << 8))
        {
            Debug.DrawRay(rayHD.origin, rayHD.direction * 10, Color.yellow);
            HD = hit.point;
            //HD.x = ((HD.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f));
            //HD.y = ((HD.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f));
            i++;
            Debug.Log("HD = " + HD);
        }
        if (Physics.Raycast(rayBG, out hit, Mathf.Infinity))// Mathf.Infinity))// 200f, 1 << 8))
        {
            Debug.DrawRay(rayBG.origin, rayBG.direction * 10, Color.yellow);
            BG = hit.point;
            //BG.x = ((BG.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f));
            //BG.y = ((BG.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f));
            i++;
            Debug.Log("BG = " + BG);
        }
        if (Physics.Raycast(rayBD, out hit, Mathf.Infinity))// Mathf.Infinity))// 200f, 1 << 8))
        {
            Debug.DrawRay(rayBD.origin, rayBD.direction * 10, Color.yellow);
            BD = hit.point;
           // BD.x = ((BD.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f));
            //BD.y = ((BD.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f));
            i++;
            Debug.Log("BD = " + BD);
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