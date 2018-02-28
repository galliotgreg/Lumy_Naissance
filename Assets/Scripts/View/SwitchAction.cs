using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchAction : MonoBehaviour {

    Transform origine;
	// Use this for initialization
	void Start () {
        this.gameObject.GetComponent<Button>().onClick.AddListener(ShowAction);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void ShowAction()
    {
        //Ouverture du Lumy, avec un second lumy d'ouvert
        if (!transform.parent.Find("PanelAction").gameObject.activeSelf && !transform.parent.Find("btn_Merge").gameObject.activeSelf)
        {
            //Premiere etape, fermé tout les boutons / panel action des lumys
            //Parcours de l'arbre des lumy
            //Placement de l'origine de la nuée
            origine = transform.parent;
            while (origine.name != "Nuee(Clone)")
            {
                origine = origine.parent;
            }
            //Desactivation des boutons du lumy
            CloseAction(origine);

            //Montrer le bon panel (PanelAction OU btn_Merge)
            //Afficher le PanelAction
            if (IsFeuille(transform.parent.parent))
            {
                transform.parent.Find("PanelAction").gameObject.SetActive(true);
            }
            //Afficher le btn_Merge (Action Unfork)
            int childFeuille = 0;
            //Verification que les deux enfants sont des feuilles
            foreach (Transform child in transform.parent.parent)
            {
                if (child.name == "nuee feuille")
                {
                    if (IsFeuille(child))
                    {
                        childFeuille++;
                    }
                }
            }
            if (childFeuille == 2)
            {
                transform.parent.Find("btn_Merge").gameObject.SetActive(true);
            }

            Text txt = transform.Find("Text").gameObject.GetComponent<Text>();
            AppContextManager.instance.SwitchActiveCast(txt.text);
            CastesUIController.instance.LoadEditedLumy();
        }
        else //Fermeture du lumy ouvert (reselectionné pour simplement le fermer
        {
            if (transform.parent.Find("PanelAction").gameObject.activeSelf)
            {
                transform.parent.Find("PanelAction").gameObject.SetActive(false);
            }
            if (transform.parent.Find("btn_Merge").gameObject.activeSelf)
            {
                transform.parent.Find("btn_Merge").gameObject.SetActive(false);
            }
        }
    }

    void CloseAction(Transform nuee)
    {
        nuee.Find("PanelLumy").Find("PanelAction").gameObject.SetActive(false);
        nuee.Find("PanelLumy").Find("btn_Merge").gameObject.SetActive(false);
        //Parcours des autres lumy
        foreach (Transform child in nuee)
        {
            if (child.name == "nuee feuille")
            {
                CloseAction(child);
            }
        }
    }

    bool IsFeuille(Transform nuee)
    {
        int nbFeuille = 0;
        foreach (Transform child in nuee)
        {
            if (child.name == "nuee feuille")
            {
                nbFeuille++;
            }
        }
        return (nbFeuille == 0);
    }
}
