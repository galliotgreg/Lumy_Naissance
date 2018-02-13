using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class OpenVoletVertical : MonoBehaviour {
	[SerializeField]
	private float headerHeight = 30;
	[SerializeField]
	private float panelHeight = 990;
    GameObject panelNodeHeaderClick;
    GameObject panelActionHeaderClick;
    GameObject panelParamsHeaderClick;
    GameObject panelOpeHeaderClick;
    GameObject panelNode;
    GameObject panelAction;
    GameObject panelParams;
    GameObject panelOpe;
	RectTransform rtNode;
	RectTransform rtActions;
	RectTransform rtParams;
	RectTransform rtOpeHeader;
	RectTransform rtNodeHeader;
	RectTransform rtActionsHeader;
	RectTransform rtParamsHeader;
	RectTransform rtOpe;
	//booléen etat des onglets
	bool[] voletsIsOpen = new bool[5];
	// Use this for initialization
	void Start () {

		//initialisation de l'état des volet au démarage
		// ---> Au moins 1 ouvert
		voletsIsOpen [0] = true;
		voletsIsOpen [1] = false;
		voletsIsOpen [2] = false;
		voletsIsOpen [3] = false;

		//Recuperation du panel node et de son header
		panelNode = GameObject.Find("PanelNode");
		panelNodeHeaderClick = GameObject.Find("PanelHeaderNode");
		//Recuperation du panel Action et de son header
		panelAction = GameObject.Find("PanelActions");
		panelActionHeaderClick = GameObject.Find ("PanelHeaderActions");
		//Recuperation du panel Parametres et de son header
		panelParams = GameObject.Find("PanelParametres");
		panelParamsHeaderClick = GameObject.Find ("PanelHeaderParameters");
		//Recuperation du panel Operateur et de son header
		panelOpe = GameObject.Find("PanelOperateur");
		panelOpeHeaderClick = GameObject.Find ("PanelHeaderOperateur");

		//get rectransform Panel parent "panelX"
		rtNode = (RectTransform)panelNode.transform;
		rtActions = (RectTransform) panelAction.transform;
		rtParams = (RectTransform)panelParams.transform;
		rtOpe = (RectTransform)panelOpe.transform;

		//getrectTransform panel Header "rtPanelHeader"
		rtNodeHeader = (RectTransform) panelNodeHeaderClick.transform;
		rtActionsHeader = (RectTransform)panelActionHeaderClick.transform;
		rtParamsHeader = (RectTransform)panelParamsHeaderClick.transform;
		rtOpeHeader = (RectTransform)panelOpeHeaderClick.transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void OpenCloseFirstPanel(){
		float heightPanel = rtActions.rect.height;
		float heightHeaderPanel = rtActionsHeader.rect.height;
		bool isOpen = voletsIsOpen [0];
		float pos;
		//Fermeture
		if (isOpen == true) 
		{
			heightPanel = rtNode.rect.width / 36;
			rtNode.sizeDelta = new Vector2 (rtNode.rect.width, heightPanel);
			pos = rtNode.rect.position.y;
			Debug.Log ("Position Y node: " + pos);
			Debug.Log ("Position X node: " + rtNode.rect.position.x);
			pos = rtNode.rect.height - (heightPanel+heightHeaderPanel*2)/100;
			rtNode.SetPositionAndRotation (new Vector3(rtNode.rect.position.x,pos,0), Quaternion.Euler(0,0,0));
			Debug.Log ("Position Y node2: " + pos);
			Debug.Log ("Position X node2: " + rtNode.rect.position.x);
		} 
		//Ouverture
		else 
		{
			heightPanel = rtActions.rect.width * 36;
			rtNode.sizeDelta = new Vector2 (rtActions.rect.width, heightPanel);
		}


	}
	public void OpenCloseSecondPanel(){
		
	}
	public void OpenCloseThirdPanel(){
		
	}
	public void OpenCloseFourthPanel(){
		
	}

}

