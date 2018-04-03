using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewPanel : MonoBehaviour
{

    public GameObject subMenu;
    [SerializeField]
    public string JSON_file = "";

    // Use this for initialization
    void Start()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(SwitchMenu);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SwitchMenu()
    {
        //InstantiateButton();

        HelpManager.instance.UpdatePanel(JSON_file);
        HelpManager.instance.RefreshHelpScroll();

    }

}
