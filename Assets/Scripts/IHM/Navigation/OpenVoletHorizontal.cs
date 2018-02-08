using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenVoletHorizontal : MonoBehaviour {

	private float rightWidth;
    private float leftWidth;
	private float middWidth;
	private bool isOpenRight;
	private bool isOpenLeft;
	GameObject leftButton;
	GameObject rightButton;
	GameObject leftPanel;
	GameObject middPanel;
	GameObject rightPanel;
	RectTransform rtLeft;
	RectTransform rtRight;
	RectTransform rtMidd;

    // Use this for initialization
    void Start () {
		isOpenLeft = true;
		isOpenRight = true;
		leftButton = GameObject.Find ("btn_VoletGauche");
		rightButton = GameObject.Find ("btn_VoletDroit");
		leftButton.GetComponent<Button>().onClick.AddListener(MoveLeftPanel);
		rightButton.GetComponent<Button>().onClick.AddListener(MoveRightPanel);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	void MoveLeftPanel() {
		
		leftPanel = GameObject.FindWithTag ("Left");
		rtLeft = (RectTransform)leftPanel.transform;
		leftWidth = rtLeft.rect.width;

		middPanel = GameObject.FindWithTag ("Midd");
		rtMidd = (RectTransform)middPanel.transform;
		middWidth = rtMidd.rect.width;
		Debug.Log (isOpenLeft);
		if(isOpenLeft == true)
		{
			Debug.Log ("Left: isOpen: "+ isOpenLeft);	
			middWidth = middWidth + leftWidth;

		}else{
			Debug.Log ("Left: isOpen: "+ isOpenLeft);	
		}
		isOpenLeft = !isOpenLeft;
    }

	void MoveRightPanel(){
		rightPanel = GameObject.FindWithTag ("Right");
		rtRight = (RectTransform)rightPanel.transform;
		rightWidth = rtRight.rect.width;

		middPanel = GameObject.FindWithTag ("Midd");
		rtMidd = (RectTransform)middPanel.transform;
		middWidth = rtMidd.rect.width;

		if (isOpenRight == true) {
			Debug.Log ("Right: isOpen " + isOpenRight);

		} else {
			Debug.Log ("Right: isOpen " + isOpenRight);
		}
		isOpenRight = !isOpenRight;
	}
}
