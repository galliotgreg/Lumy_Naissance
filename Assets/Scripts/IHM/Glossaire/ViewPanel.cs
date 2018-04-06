using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewPanel : MonoBehaviour
{

    public GameObject subMenu;
    [SerializeField]
    public string JSON_file = "parametres";

    // Use this for initialization
    void Start()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(SwitchMenu);
        HelpManager.instance.UpdateDatabase(JSON_file);
        PanelManager.instance.RefreshHelpScroll();
        /*
        HelpManager.instance.UpdatePanel(JSON_file);
        HelpManager.instance.RefreshHelpScroll();*/
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SwitchMenu()
    {
        /*HelpManager.instance.UpdateDatabase(JSON_file);
        HelpManager.instance.RefreshHelpScroll();*/

        HelpManager.instance.UpdateDatabase(JSON_file);
        PanelManager.instance.RefreshHelpScroll();

    }

}
