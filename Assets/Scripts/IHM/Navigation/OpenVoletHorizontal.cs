using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenVoletHorizontal : MonoBehaviour {
    //Largeur des volets
	private float rightWidth;
    private float leftWidth;
	private float middWidth;
    //Booléen état des volet
	private bool isOpenRight;
	private bool isOpenLeft;
    //Button
	GameObject leftButton;
	GameObject rightButton;
    GameObject buttonMenuPanel;
    //Panel
    GameObject leftPanel;
	GameObject middPanel;
	GameObject rightPanel;
    //RectTransform de chaque Panel
	RectTransform rtLeft;
	RectTransform rtRight;
	RectTransform rtMidd;
	RectTransform rtMenuBarre;

    // Use this for initialization
    void Start () {
        //initialisation booléen de l'état des volets
		isOpenLeft = true;
		isOpenRight = true;
        //Detection des bouton liés aux volets
		leftButton = GameObject.Find ("btn_VoletGauche");
		rightButton = GameObject.Find ("btn_VoletDroit");
		//get Largeur Panel gauche
		leftPanel = GameObject.FindWithTag ("Left");
		rtLeft = (RectTransform)leftPanel.transform;
		leftWidth = rtLeft.rect.width;
		//get Largeur panel droit
		rightPanel = GameObject.FindWithTag ("Right");
		rtRight = (RectTransform)rightPanel.transform;
		rightWidth = rtRight.rect.width;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //Recentrer panel gauche
	public void MoveLeftPanel() {
        //Valeur pour recentrer le panel central
        float recenterPanel =0;
        //Recupération de la largeur du panel central
        //Via la rectTransform du panel tagger "Midd" 
        middPanel = GameObject.FindWithTag ("Midd");
		rtMidd = (RectTransform)middPanel.transform;
		middWidth = rtMidd.rect.width;
        //Détection ouverture - Fermeture volet
        if (isOpenLeft == true)//Fermeture
        {
            //Calcul de la valeur de recentrage du panel central
            middWidth = middWidth + leftWidth;
			recenterPanel = leftWidth/2 ;
            //Desactivation du panel gauche
            leftPanel.SetActive (false);
            //Repositionnement du panel central sur la gauche
            float pos = rtMidd.position.x;
			pos= pos - (recenterPanel)/100;
			rtMidd.sizeDelta = new Vector2(middWidth,rtMidd.rect.height);
			rtMidd.SetPositionAndRotation (new Vector3 (pos,rtMidd.position.y,0), Quaternion.Euler(0,0,0));
			string text = leftButton.GetComponentInChildren<Text>().text = ">>";
			leftButton.GetComponentInChildren<Text> ().text = text;

		}else{//Ouverture
              //Calcul de la valeur de recentrage du panel central
            middWidth = middWidth-leftWidth;
			recenterPanel = leftWidth/2 ;
            //Desactivation du panel gauche
            leftPanel.SetActive (true);
            //Repositionnement du panel central sur la gauche
            float pos = rtMidd.position.x;
			pos= pos +(recenterPanel/100);
			rtMidd.sizeDelta = new Vector2(middWidth,rtMidd.rect.height);
			rtMidd.SetPositionAndRotation (new Vector3 (pos,rtMidd.position.y,0), Quaternion.Euler(0,0,0));
			string text = leftButton.GetComponentInChildren<Text>().text = "<<";
			leftButton.GetComponentInChildren<Text> ().text = text;
		}
		isOpenLeft = !isOpenLeft;
	}
 	//Recenter panel droit
	public void MoveRightPanel(){
        //Valeur pour recentrer le panel central
		float recenterPanel=0;
        //Recupération de la largeur du panel central
        //Via la rectTransform du panel tagger "Midd" 
		middPanel = GameObject.FindWithTag ("Midd");
		rtMidd = (RectTransform)middPanel.transform;
		middWidth = rtMidd.rect.width;

        //Détection ouverture - Fermeture volet
        
		if(isOpenRight == true)//Fermeture
        {
            //Calcul de la valeur de recentrage du panel central
			middWidth = middWidth + rightWidth;
			recenterPanel = rightWidth/2 ;
            //Desactivation du panel droit
			rightPanel.SetActive (false);
            //Repositionnement du panel central sur la droite
			float pos = rtMidd.position.x;
			pos= pos + (recenterPanel)/100;
			rtMidd.sizeDelta = new Vector2(middWidth,rtMidd.rect.height);
			rtMidd.SetPositionAndRotation (new Vector3 (pos,rtMidd.position.y,0), Quaternion.Euler(0,0,0));
			string text = rightButton.GetComponentInChildren<Text>().text = "<<";
			rightButton.GetComponentInChildren<Text> ().text = text;

		}else{//Ouverture
            //Calcul de la valeur de recentrage du panel central
            middWidth = middWidth-rightWidth;
			recenterPanel = rightWidth/2 ;
            //Desactivation du panel droit
            rightPanel.SetActive (true);
            //Repositionnement du panel central sur la droite
            float pos = rtMidd.position.x;
			pos= pos - (recenterPanel/100);
			rtMidd.sizeDelta = new Vector2(middWidth,rtMidd.rect.height);
			rtMidd.SetPositionAndRotation (new Vector3 (pos,rtMidd.position.y,0), Quaternion.Euler(0,0,0));
			string text = rightButton.GetComponentInChildren<Text>().text = ">>";
			rightButton.GetComponentInChildren<Text> ().text = text;
		}
		isOpenRight = !isOpenRight;
	}
}
