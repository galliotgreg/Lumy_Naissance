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
	GameObject buttonMenuPanel;
	RectTransform rtLeft;
	RectTransform rtRight;
	RectTransform rtMidd;
	RectTransform rtMenuBarre;

    // Use this for initialization
    void Start () {
		isOpenLeft = true;
		isOpenRight = true;
		leftButton = GameObject.Find ("btn_VoletGauche");
		rightButton = GameObject.Find ("btn_VoletDroit");

		//get Rightpanel width
		leftPanel = GameObject.FindWithTag ("Left");
		rtLeft = (RectTransform)leftPanel.transform;
		leftWidth = rtLeft.rect.width;
		//get Rightpanel width
		rightPanel = GameObject.FindWithTag ("Right");
		rtRight = (RectTransform)rightPanel.transform;
		rightWidth = rtRight.rect.width;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	public void MoveLeftPanel() {		
		float recenterPanel=0;

		middPanel = GameObject.FindWithTag ("Midd");
		rtMidd = (RectTransform)middPanel.transform;
		middWidth = rtMidd.rect.width;

		//buttonMenuPanel = GameObject.FindWithTag ("ButtonMenuBarre");
		//rtMenuBarre = (RectTransform)buttonMenuPanel.transform;

		if(isOpenLeft == true)
		{
			middWidth = middWidth + leftWidth;
			recenterPanel = leftWidth/2 ;
			leftPanel.SetActive (false);
			float pos = rtMidd.position.x;
			pos= pos - (recenterPanel)/100;
			rtMidd.sizeDelta = new Vector2(middWidth,0);
			rtMidd.SetPositionAndRotation (new Vector3 (pos,0,0), Quaternion.Euler(0,0,0));


		}else{
			middWidth = middWidth-leftWidth;
			recenterPanel = leftWidth/2 ;
			leftPanel.SetActive (true);
			float pos = rtMidd.position.x;
			pos= pos +(recenterPanel/100);
			rtMidd.sizeDelta = new Vector2(middWidth,0);
			rtMidd.SetPositionAndRotation (new Vector3 (pos,0,0), Quaternion.Euler(0,0,0));

		}
		isOpenLeft = !isOpenLeft;
	}

	//Recenter Right panel
	public void MoveRightPanel(){
		float recenterPanel=0;

		middPanel = GameObject.FindWithTag ("Midd");
		rtMidd = (RectTransform)middPanel.transform;
		middWidth = rtMidd.rect.width;

		if(isOpenRight == true)
		{
			middWidth = middWidth + rightWidth;
			recenterPanel = rightWidth/2 ;
			rightPanel.SetActive (false);
			float pos = rtMidd.position.x;
			pos= pos + (recenterPanel)/100;
			rtMidd.sizeDelta = new Vector2(middWidth,0);
			rtMidd.SetPositionAndRotation (new Vector3 (pos,0,0), Quaternion.Euler(0,0,0));
		}else{
			middWidth = middWidth-rightWidth;
			recenterPanel = rightWidth/2 ;
			rightPanel.SetActive (true);
			float pos = rtMidd.position.x;
			pos= pos - (recenterPanel/100);
			rtMidd.sizeDelta = new Vector2(middWidth,0);
			rtMidd.SetPositionAndRotation (new Vector3 (pos,0,0), Quaternion.Euler(0,0,0));
		}
		isOpenRight = !isOpenRight;
	}
}
