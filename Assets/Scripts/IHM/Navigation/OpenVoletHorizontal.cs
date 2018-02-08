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



    }
	
	// Update is called once per frame
	void Update () {
		leftButton.GetComponent<Button>().onClick.AddListener(MoveLeftPanel);
		rightButton.GetComponent<Button>().onClick.AddListener(MoveRightPanel);
	}

	void MoveLeftPanel() {
		isOpenLeft = !isOpenLeft;
		leftPanel = GameObject.FindWithTag ("Left");
		rightPanel = GameObject.FindWithTag ("Right");
		middPanel = GameObject.FindWithTag ("Midd");
		rtLeft = (RectTransform)leftPanel.transform;
		rtRight = (RectTransform)rightPanel.transform;
		rtMidd = (RectTransform)middPanel.transform;

		rightWidth = rtLeft.rect.width;
		leftWidth = rtRight.rect.width;
		middWidth = rtMidd.rect.width;


		if(isOpenLeft == true)
		{
			Debug.Log ("Left: isOpen: "+ isOpenLeft);	
		}else{
			Debug.Log ("Left: isOpen: "+ isOpenLeft);	
		}
    }
	void MoveRightPanel(){
		isOpenRight = !isOpenRight;

		if (isOpenRight == true) {
			Debug.Log ("Right: isOpen " + isOpenRight);
		} else {
			Debug.Log ("Right: isOpen " + isOpenRight);
		}
	}
}
