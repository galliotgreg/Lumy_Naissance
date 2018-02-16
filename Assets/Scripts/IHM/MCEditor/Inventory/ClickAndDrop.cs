using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickAndDrop : MonoBehaviour {

    public GameObject TEST;

    GameObject heldObject;
    bool holding = false;

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
        }
    }

    void ActivateHoldingMode()
    {
        /*GameObject objectToHold = new GameObject("New Object");
        objectToHold.AddComponent<CanvasRenderer>();
        objectToHold.AddComponent<Image>();
        objectToHold.GetComponent<Image>().sprite = this.gameObject.GetComponent<Image>().sprite;*/

        GameObject objectToHold = TEST;

        heldObject = Instantiate(objectToHold);
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
        pos.z = 10;
        heldObject.transform.position = GameObject.Find("Main Camera").GetComponent<Camera>().ScreenToWorldPoint(pos);
    }
}
