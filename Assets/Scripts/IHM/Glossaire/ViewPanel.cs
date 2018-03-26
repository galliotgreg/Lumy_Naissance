using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewPanel : MonoBehaviour
{

    public GameObject subMenu;
    [SerializeField]
    public GameObject prefabButton;
    HelpDatabase help = new HelpDatabase();
    [SerializeField]
    public string JSON_file ="";

    // Use this for initialization
    void Start()
    {
        help.LoadDatabase(JSON_file);
        
        //listHelp.
        this.gameObject.GetComponent<Button>().onClick.AddListener(SwitchMenu);
        //subMenu.GetComponent<Text>().text = help.FetchHelpByID(ID).Content;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SwitchMenu()
    {
        //subMenu.GetComponent<Text>().text = help.FetchHelpByID(ID).Content;
        // subMenu.SetActive(!subMenu.activeSelf);
        //InstantiateButton();

        GameObject.Find("SidePanel").GetComponent<SidePanel>().SetFile(JSON_file);
    }

    void InstantiateButton()
    {
        Vector3 instantiatePosition = new Vector3(0, 0, 0);
        for (int i = 0; i < help.GetLength(); i++)
        {
            Instantiate(prefabButton, instantiatePosition, Quaternion.Euler(0, 0, 0));
            instantiatePosition.Set(0, 20 * i, 0);
        }
    }
}