using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class HelpManager : MonoBehaviour {
    /// <summary>
    /// The static instance of the Singleton for external access
    /// </summary>
    public static HelpManager instance = null;
    public HelpDatabase help;

    [SerializeField]
    string JSON_name = "";

    /// <summary>
    /// Enforce Singleton properties
    /// </summary>
    void Awake()
    {
        //Check if instance already exists and set it to this if not
        if (instance == null)
        {
            instance = this;
            help = new HelpDatabase();
            help.LoadDatabase(JSON_name);
        }

        //Enforce the unicity of the Singleton
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    public string JSON_Name
    {
        get
        {
            return JSON_name;
        }

        set
        {
            JSON_name = value;
        }
    }

    public void UpdateDatabase(string JSON_name = "definition")
    {
        this.JSON_name = JSON_name;
        help.LoadDatabase(JSON_name);
    }
    
    public string[] GetListHelpTitle()

    {
        string[] listTitle = new string[help.GetLength()];
        for(int i = 0; i< help.GetLength();i++)
        {
            listTitle[i] = help.FetchHelpByID(i).Title;
        }
        return listTitle;
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
