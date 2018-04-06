using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SelectHelpScript : MonoBehaviour {
    [SerializeField]
    private string helpName;

    // Use this for initialization
    void Start()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(SelectHelp);
        helpName = GetComponentInChildren<Text>().text;
    }

    void SelectHelp()
    {
        //HelpManager.instance.SelectHelp(helpName);
        PanelManager.instance.SelectHelp(helpName);
    }
}
