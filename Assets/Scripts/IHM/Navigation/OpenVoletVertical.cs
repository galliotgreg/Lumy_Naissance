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

		//getrectTransform panel Header ""
		rtNodeHeader = (RectTransform) panelNodeHeaderClick.transform;
		rtActionsHeader = (RectTransform)panelActionHeaderClick.transform;
		rtParamsHeader = (RectTransform)panelParamsHeaderClick.transform;
		rtOpeHeader = (RectTransform)panelOpeHeaderClick.transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	//detecte si ont doit fermer ou ouvrir le volet
	public void OpenCloseVerticalPanel(){
		
		if (voletsIsOpen [0] == false) {

		} else if (voletsIsOpen [1] == false) {

		} else if (voletsIsOpen [2] == false) {

		} else if (voletsIsOpen [3] == false) {

		}else if (voletsIsOpen [0] == true) {

		} else if (voletsIsOpen [1] == true) {

		} else if (voletsIsOpen [2] == true) {

		} else if (voletsIsOpen [3] == true) {

		}
	}
	//Ouverture d'un volet si il est fermé
	public void OpenVerticalPanel(int cas) {
		
	}
	//Fermeture d'un volet si il est ouvert
	public void CloseVerticalPanel(int cas){
		
	}
}

