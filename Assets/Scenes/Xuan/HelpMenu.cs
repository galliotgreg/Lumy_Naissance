using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class HelpMenu : MonoBehaviour {
    HelpDatabase help = new HelpDatabase();
    // Use this for initialization
    void Start () {
        help.LoadDatabase();
        this.gameObject.GetComponent<Button>().onClick.AddListener(OpenMenu);

    }

    // Update is called once per frame
    void Update () {
		
	}

    void OpenMenu()
    {
        Debug.Log(help.FetchHelpByID(1).Content);
    }

}
