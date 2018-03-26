using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewExplanation : MonoBehaviour
{

    public GameObject subMenu;
    public GameObject tabMenu;
    public ListHelp listHelp;
    HelpDatabase help = new HelpDatabase();
    [SerializeField]
    int ID;

    // Use this for initialization
    void Start()
    {

        help.LoadDatabase(tabMenu.GetComponent<ViewPanel>().JSON_file);
        help.LoadDatabase(GameObject.Find("SidePanel").GetComponent<SidePanel>().GetFile());
        this.gameObject.GetComponent<Button>().onClick.AddListener(SwitchMenu);
        this.gameObject.GetComponentInChildren<Text>().text = help.FetchHelpByID(ID).Title;

       // subMenu.GetComponent<Text>().text =  tabMenu.GetComponent<HelpDatabase>().FetchHelpByID(ID).Content;

    }

    // Update is called once per frame
    void Update()
    {

    }

    void SwitchMenu()
    {
        subMenu.GetComponent<Text>().text = help.FetchHelpByID(ID).Content;
        //subMenu.GetComponent<Text>().text = tabMenu.GetComponent<HelpDatabase>().FetchHelpByID(ID).Content;
        // subMenu.SetActive(!subMenu.activeSelf);

    }
}