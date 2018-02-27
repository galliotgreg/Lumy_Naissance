using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MergeLumy : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(UIMerge);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void UIMerge()
    {
        AppContextManager.instance.UnforkCast();
        CastesUIController.instance.CreateTree();

        //this.gameObject.SetActive(false);
        //transform.parent.Find("PanelAction").gameObject.SetActive(true);

        //réactive le bouton Merge du parent
        //int feuille = 0;
        //Suppression des feuilles fille
        //foreach (Transform child in transform.parent.parent)
        //{
        //    /* if (child.name == "nuee feuille" && child.Find("PanelLumy").Find("PanelAction").gameObject.activeSelf)
        //     {
        //         Destroy(child.gameObject);
        //     }*/
        //}
        //condition de réactivation : les deux fils (nuée feuilles) posséde leur panelAction actif
        //foreach (Transform child in transform.parent.parent.parent)
        //{
        //    if (child.name == "nuee feuille" && child.Find("PanelLumy").Find("PanelAction").gameObject.activeSelf)
        //    {
        //        feuille++;
        //    }
        //}
        //if (feuille == 2)
        //{
        //    transform.parent.parent.parent.Find("PanelLumy").Find("btn_Merge").gameObject.SetActive(true);
        //}
    }
}
