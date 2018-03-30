using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewExplanation : MonoBehaviour
{
    [SerializeField]
    public GameObject explanationText;

    [SerializeField]
    int ID;

    // Use this for initialization
    void Start()
    {
        //help.LoadDatabase(GameObject.Find("SidePanel").GetComponent<SidePanel>().GetFile());
        this.gameObject.GetComponent<Button>().onClick.AddListener(SwitchMenu);
        this.gameObject.GetComponentInChildren<Text>().text = HelpManager.instance.help.FetchHelpByID(ID).Title;

    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.GetComponentInChildren<Text>().text = HelpManager.instance.help.FetchHelpByID(ID).Title;
    }

    void SwitchMenu()
    {
        explanationText.GetComponent<Text>().text = HelpManager.instance.help.FetchHelpByID(ID).Content;
    }
}