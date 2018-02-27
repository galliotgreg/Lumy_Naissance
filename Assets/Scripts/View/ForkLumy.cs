using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ForkLumy : MonoBehaviour {

    public GameObject nuee;
    public GameObject nueeprefab;
    // Use this for initialization
    void Start()
    {
        Init();
    }

    public void Init()
    {
        nuee = transform.parent.parent.parent.gameObject;
        this.gameObject.GetComponent<Button>().onClick.AddListener(Fork);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Fork()
    {
        UIFork();

        //Invoquer le Forw via le Manager
        AppContextManager.instance.ForkCast();
        CastesUIController.instance.CreateTree();
    }

    public void UIFork()
    {
        //Bloquer le lumy d'origine, qui n'est plus modifiable
        //if (transform.parent.parent.parent.name == "Nuee(Clone)" || transform.parent.parent.parent.name == "nuee feuille")
        //{
        //    foreach (Transform child in transform.parent.parent.parent.Find("PanelLumy"))
        //    {
        //        Debug.Log(child.name);
        //    }
        //transform.parent.parent.parent.Find("PanelLumy").Find("btn_Lumy").GetComponent<Button>().interactable = false;
        //}
        transform.parent.gameObject.SetActive(false);
        //Activation du bouton Merge du nouveau parent

        //transform.parent.parent.Find("btn_Merge").gameObject.SetActive(true);

        //Desactivé la possibilité de Merge depuis un ancien parent
        //if (transform.parent.parent.parent.parent.name != "PanelNuéeSelectionnée")
        //{
        //    transform.parent.parent.parent.parent.Find("PanelLumy").Find("btn_Merge").gameObject.SetActive(false);
        //}

        for (int i = 0; i < 2; i++)
        {
            var clone = Instantiate(nueeprefab, nuee.transform);
            clone.name = "nuee feuille";
            clone.transform.localScale = Vector3.one;
            clone.transform.GetChild(0).Find("btn_Lumy").GetComponent<Button>().interactable = true;
            clone.transform.Find("PanelLumy").Find("PanelAction").gameObject.SetActive(false);
            clone.transform.Find("PanelLumy").Find("btn_Merge").gameObject.SetActive(false);
            clone.transform.localPosition = new Vector3(2f - 4 * i, -2f, 0);
            //if (clone.transform.childCount > 1) Destroy(clone.transform.GetChild(1).gameObject);
        }

    }
}
