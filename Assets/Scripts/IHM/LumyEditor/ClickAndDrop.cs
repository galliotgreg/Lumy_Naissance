using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickAndDrop : MonoBehaviour {

    public GameObject emptyComponentPrefab;

    GameObject heldObject;
    bool holding = false;

    [SerializeField]
    private float dropDist = 0.5f; 

	// Use this for initialization
	void Start () {
        this.gameObject.GetComponent<Button>().onClick.AddListener(ActivateHoldingMode);
    }
	
	// Update is called once per frame
	void Update () {
		if (holding)
        {
            SetPositionToMouse();
        }
        if (holding && Input.GetMouseButton(0))
        {
            //heldObject.transform.SetParent(GameObject.Find("WorldPanel").transform);
            holding = false;
            this.GetComponent<Image>().color = new Color(255f, 255f, 255f);
            if (ValidateHeadDrop())
            {
                int id = heldObject.GetComponent<AgentComponent>().Id;
                LumyEditorManager.instance.PushHead(id);
            } else if (ValidateTailDrop())
            {
                int id = heldObject.GetComponent<AgentComponent>().Id;
                LumyEditorManager.instance.PushTail(id);
            }
            Destroy(heldObject);
        }
    }

    private bool ValidateHeadDrop()
    {
        GameObject activeLumy = LumyEditorManager.instance.EditedLumy;
        GameObject head = activeLumy.transform.Find("Head").gameObject;
        GameObject headCompo = head.transform.GetChild(0).gameObject;

        float dist = Vector2.Distance(headCompo.transform.position, heldObject.transform.position);
        if (dist < dropDist)
        {
            return true;
        }
        return false;
    }

    private bool ValidateTailDrop()
    {
        GameObject activeLumy = LumyEditorManager.instance.EditedLumy;
        GameObject tail = activeLumy.transform.Find("Tail").gameObject;
        GameObject tailCompo = tail.transform.GetChild(tail.transform.childCount - 1).gameObject;

        float dist = Vector2.Distance(tailCompo.transform.position, heldObject.transform.position);
        if (dist < dropDist)
        {
            return true;
        }

        return false;
    }

    void ActivateHoldingMode()
    {
        /*GameObject objectToHold = new GameObject("New Object");
        objectToHold.AddComponent<CanvasRenderer>();
        objectToHold.AddComponent<Image>();
        objectToHold.GetComponent<Image>().sprite = this.gameObject.GetComponent<Image>().sprite;*/

        heldObject = Instantiate(emptyComponentPrefab);
        heldObject.transform.rotation = Quaternion.AngleAxis(90f, Vector3.right);
        heldObject.transform.position = new Vector3(
            heldObject.transform.position.x, 
            heldObject.transform.position.y,
            0f);
        PhyJoin phyJoin = heldObject.GetComponent<PhyJoin>();
        phyJoin.enabled = false;
        LibraryCompoData compoData = gameObject.GetComponent<LibraryCompoData>();
        AgentComponent agentComponent = heldObject.GetComponent<AgentComponent>();
        UnitTemplateInitializer.CopyComponentValues(compoData.ComponentInfo, agentComponent);

        SetPositionToMouse();
        //heldObject.GetComponent<RectTransform>().localScale = GameObject.Find("WorldCanvas").GetComponent<RectTransform>().localScale;
        //heldObject.transform.SetParent(GameObject.Find("WorldPanel").transform);

        this.GetComponent<Image>().color = new Color(0, 255f, 255f);

        holding = true;
    }

    void SetPositionToMouse()
    {
        //Vector3 pos = (Input.mousePosition.x, Input.mousePosition.y,  GameObject.Find("WorldCanvas").GetComponent<RectTransform>().z);
        Vector3 pos = Input.mousePosition;
        pos.z = 10f;
        heldObject.transform.position = GameObject.Find("Main Camera").GetComponent<Camera>().ScreenToWorldPoint(pos);
        heldObject.transform.position = new Vector3(heldObject.transform.position.x, heldObject.transform.position.y, -1f);
    }
}
