using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class OpenVoletVertical : MonoBehaviour {
	[SerializeField]
	private float headerHeight = 30;
	[SerializeField]
	private float panelHeight = 990;
	private float windowHeight = 1080;
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
	bool isOpen;
	// Use this for initialization
	void Start () {
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
		isOpen = true;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log ("5:" +  isOpen);
	}
	public void OpenFirstPanel(){
		Debug.Log ("0:" +  isOpen);
		GameObject btnCreer;
		float heightPanel = rtActions.rect.height;
		float heightHeaderPanel = rtActionsHeader.rect.height;
		float recenterPos;

		//Fermeture
		heightPanel = headerHeight;
		rtNode.sizeDelta = new Vector2 (rtNode.rect.width, heightPanel);
		btnCreer = GameObject.Find("btn_Creer");

		//Button et filtre de Node
		Button creer = btnCreer.GetComponent<Button>();
		GameObject panelFilter = GameObject.Find ("PanelFilter");
		creer.enabled = false;
		if (isOpen == false) {
			Debug.Log ("1:" + panelFilter.activeSelf);
			panelFilter.SetActive (true);
			Debug.Log ("2:" + panelFilter.activeSelf);
			isOpen = true;
		}

		//Deplacement du volet du dessus en haut de l'écran
		float anchorPosition = ((3*headerHeight)+(panelHeight-headerHeight)+(headerHeight/2))-windowHeight;
		float anchorX = rtNode.anchoredPosition.x;
		float anchorY = rtNode.anchoredPosition.y;
		//Ouverture du panel cliqué
		anchorX = rtActions.anchoredPosition.x;
		anchorY = rtActions.anchoredPosition.y;
		anchorPosition =-(panelHeight/2);
		rtNode.anchoredPosition = new Vector2 (rtActions.anchoredPosition.x, anchorPosition);
		rtNode.sizeDelta = new Vector2(rtActions.rect.width, panelHeight);

		//Fermeture des volet du dessous
		//1er volet en dessous
		anchorX =rtActions.anchoredPosition.x;
		anchorY = rtActions.anchoredPosition.y;
		anchorPosition = -(panelHeight + (headerHeight / 2));
		rtActions.anchoredPosition = new Vector2 (rtParams.anchoredPosition.x, anchorPosition);
		rtActions.sizeDelta =new Vector2(rtParams.rect.width, headerHeight);
		//2er volmet en dessou
		anchorX = rtParams.anchoredPosition.x;
		anchorY = rtParams.anchoredPosition.y;
		anchorPosition = -(panelHeight + (headerHeight / 2) +headerHeight);
		rtParams.anchoredPosition = new Vector2 (rtParams.anchoredPosition.x, anchorPosition);
		rtParams.sizeDelta =new Vector2(rtParams.rect.width, headerHeight);
		//3eme volet en dessous
		anchorX = rtOpe.anchoredPosition.x;
		anchorY = rtOpe.anchoredPosition.y;
		anchorPosition = -(panelHeight + (headerHeight / 2) +(2*headerHeight));
		rtOpe.anchoredPosition = new Vector2 (rtParams.anchoredPosition.x, anchorPosition);
		rtOpe.sizeDelta =new Vector2(rtParams.rect.width, headerHeight);

	}
	public void OpenSecondPanel(){
		GameObject btnCreer;
		float heightPanel = rtActions.rect.height;
		float heightHeaderPanel = rtActionsHeader.rect.height;
		float recenterPos;
		//Fermeture
		heightPanel = 30;//rtNode.rect.height / 36;
		rtNode.sizeDelta = new Vector2 (rtNode.rect.width, heightPanel);
		btnCreer = GameObject.Find("btn_Creer");

		//Button et filtre de Node
		Button creer = btnCreer.GetComponent<Button>();
		GameObject panelFilter = GameObject.Find ("PanelFilter");
		creer.enabled = false;
		if (isOpen == true) {
			panelFilter.SetActive (false);
			isOpen = false;
			//Debug.Log ("4:" +  isOpen);
		}

		//Deplacement du volet du dessus en haut de l'écran
		recenterPos = rtNode.rect.position.y;
		float anchorX = rtNode.anchoredPosition.x;
		float anchorY = rtNode.anchoredPosition.y;
		float anchorPosition = ((3*headerHeight)+(panelHeight-headerHeight)+(headerHeight/2))-windowHeight;

		rtNode.anchoredPosition= new Vector2 (rtNode.anchoredPosition.x, anchorPosition);
		anchorX = rtNode.anchoredPosition.x;
		anchorY = rtNode.anchoredPosition.y;

		//Ouverture du panel cliqué
		anchorX = rtActions.anchoredPosition.x;
		anchorY = rtActions.anchoredPosition.y;
		anchorPosition = ((2*headerHeight)+((panelHeight-(2*headerHeight))/2)+(headerHeight))-windowHeight;
		rtActions.anchoredPosition = new Vector2 (rtActions.anchoredPosition.x, anchorPosition);
		rtActions.sizeDelta = new Vector2(rtActions.rect.width, panelHeight);

		//Fermeture des volet du dessous
		//1er volet en dessou
		anchorX = rtParams.anchoredPosition.x;
		anchorY = rtParams.anchoredPosition.y;
		anchorPosition = -(panelHeight + headerHeight) - headerHeight / 2;
		rtParams.anchoredPosition = new Vector2 (rtParams.anchoredPosition.x, anchorPosition);
		rtParams.sizeDelta =new Vector2(rtParams.rect.width, headerHeight);
		//2eme volet en dessous
		anchorX = rtOpe.anchoredPosition.x;
		anchorY = rtOpe.anchoredPosition.y;
		anchorPosition = -(panelHeight + (2*headerHeight)) - headerHeight / 2;
		rtOpe.anchoredPosition = new Vector2 (rtParams.anchoredPosition.x, anchorPosition);
		rtOpe.sizeDelta =new Vector2(rtParams.rect.width, headerHeight);
	}
	public void OpenThirdPanel(){
		GameObject btnCreer;
		float heightPanel = rtActions.rect.height;
		float heightHeaderPanel = rtActionsHeader.rect.height;
		float recenterPos;
		//Fermeture
		heightPanel = 30;//rtNode.rect.height / 36;
		rtNode.sizeDelta = new Vector2 (rtNode.rect.width, heightPanel);
		btnCreer = GameObject.Find("btn_Creer");

		//Button et filtre de Node
		Button creer = btnCreer.GetComponent<Button>();
		GameObject panelFilter = GameObject.Find ("PanelFilter");
		creer.enabled = false;
		if (isOpen == true) {
			panelFilter.SetActive (false);
			isOpen = false;
		}

		//Deplacement des volet du dessus en haut de l'écran
		recenterPos = rtNode.rect.position.y;
		float anchorX = rtNode.anchoredPosition.x;
		float anchorY = rtNode.anchoredPosition.y;

		//1er volet du dessus
		float anchorPosition = ((3*headerHeight)+(panelHeight-headerHeight)+(headerHeight/2))-windowHeight;
		rtNode.anchoredPosition= new Vector2 (rtNode.anchoredPosition.x, anchorPosition);
		anchorX = rtNode.anchoredPosition.x;
		anchorY = rtNode.anchoredPosition.y;

		//2eme volet en dessu
		anchorX = rtActions.anchoredPosition.x;
		anchorY = rtActions.anchoredPosition.y;
		anchorPosition = -((headerHeight/2) + headerHeight);
		rtActions.anchoredPosition = new Vector2 (rtActions.anchoredPosition.x, anchorPosition);
		rtActions.sizeDelta =new Vector2(rtActions.rect.width, headerHeight);

		//Ouverture du panel cliqué
		anchorX = rtParams.anchoredPosition.x;
		anchorY = rtParams.anchoredPosition.y;
		anchorPosition = -((panelHeight / 2) + 2*headerHeight);
		rtParams.anchoredPosition = new Vector2 (rtParams.anchoredPosition.x, anchorPosition);
		rtParams.sizeDelta = new Vector2(rtParams.rect.width, panelHeight);

		//Fermeture du volet du dessous
		anchorX = rtOpe.anchoredPosition.x;
		anchorY = rtOpe.anchoredPosition.y;
		anchorPosition = -(panelHeight + (2*headerHeight)) - headerHeight / 2;
		rtOpe.anchoredPosition = new Vector2 (rtParams.anchoredPosition.x, anchorPosition);
		rtOpe.sizeDelta =new Vector2(rtParams.rect.width, headerHeight);
	}
	public void OpenFourthPanel(){
		GameObject btnCreer;
		float heightPanel = rtActions.rect.height;
		float heightHeaderPanel = rtActionsHeader.rect.height;

		float recenterPos;
		//Fermeture
		heightPanel = 30;//rtNode.rect.height / 36;
		rtNode.sizeDelta = new Vector2 (rtNode.rect.width, heightPanel);
		btnCreer = GameObject.Find("btn_Creer");

		//Button et filtre de Node
		Button creer = btnCreer.GetComponent<Button>();
		GameObject panelFilter = GameObject.Find ("PanelFilter");
		creer.enabled = false;
		if (isOpen == true) {
			panelFilter.SetActive (false);
			isOpen = false;
		}

		//Deplacement des volet du dessus en haut de l'écran
		recenterPos = rtNode.rect.position.y;
		float anchorX = rtNode.anchoredPosition.x;
		float anchorY = rtNode.anchoredPosition.y;

		//1er volet du dessus
		float anchorPosition = ((3*headerHeight)+(panelHeight-headerHeight)+(headerHeight/2))-windowHeight;
		rtNode.anchoredPosition= new Vector2 (rtNode.anchoredPosition.x, anchorPosition);
		anchorX = rtNode.anchoredPosition.x;
		anchorY = rtNode.anchoredPosition.y;

		//2eme volet en dessu
		anchorX = rtActions.anchoredPosition.x;
		anchorY = rtActions.anchoredPosition.y;
		anchorPosition = -((headerHeight/2) + headerHeight);
		rtActions.anchoredPosition = new Vector2 (rtActions.anchoredPosition.x, anchorPosition);
		rtActions.sizeDelta =new Vector2(rtActions.rect.width, headerHeight);

		//3eme volet au dessu
		anchorX = rtParams.anchoredPosition.x;
		anchorY = rtParams.anchoredPosition.y;
		anchorPosition = -((headerHeight / 2) + headerHeight*2);
		rtParams.anchoredPosition = new Vector2 (rtParams.anchoredPosition.x, anchorPosition);
		rtParams.sizeDelta = new Vector2(rtParams.rect.width, headerHeight);

		//Ouverture du panel cliqué
		anchorX = rtOpe.anchoredPosition.x;
		anchorY = rtOpe.anchoredPosition.y;
		anchorPosition = -(3*headerHeight) - panelHeight / 2;
		rtOpe.anchoredPosition = new Vector2 (rtParams.anchoredPosition.x, anchorPosition);
		rtOpe.sizeDelta =new Vector2(rtParams.rect.width, panelHeight);
	}
		

}