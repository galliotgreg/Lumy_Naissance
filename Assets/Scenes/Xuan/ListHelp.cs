using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListHelp : MonoBehaviour {

    HelpDatabase helpdatabase = new HelpDatabase(); 
    //public List<Help> helps = new List<Help>();
    
	// Use this for initialization
	void Start () {
        helpdatabase.LoadDatabase();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public Help FetchHelpByID(int id)
    {
        return helpdatabase.FetchHelpByID(id);
    }
    public void LoadDatabase()
    {
        helpdatabase.LoadDatabase();
    }
}
